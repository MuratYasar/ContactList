using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ocelot.LoadBalancer.LoadBalancers;
using Ocelot.Responses;
using Ocelot.Values;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Contracts;
using Logging;

namespace Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddControllers();

            services.AddDbContext<ContactListContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.TryAddSingleton<ILoggerManager, LoggerManager>();

            services.AddOcelot()
                .AddDelegatingHandler<RequestLogger>()
                .AddCacheManager(settings => settings.WithDictionaryHandle())
                .AddCustomLoadBalancer((serviceProvider, downstreamRoute, serviceDiscoveryProvider) => {
                    return new CustomLoadBalancer(serviceDiscoveryProvider.Get);
                });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gateway", Version = "v1" });
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePathBase("/gateway");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway v1"));
            }

            app.UseAuthentication();

            app.UseRouting();
            
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            await app.UseOcelot();
        }
    }

    public class RequestLogger : DelegatingHandler
    {
        private readonly ILogger<RequestLogger> _logger;

        public RequestLogger(ILogger<RequestLogger> logger)
        {
            _logger = logger;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Request [{request.Method}] - {request.RequestUri?.PathAndQuery} : {request.RequestUri?.Port}");
            return base.SendAsync(request, cancellationToken);
        }
    }

    public class CustomLoadBalancer : ILoadBalancer
    {
        private readonly Func<Task<List<Service>>> _services;
        private readonly object _lock = new();
        private int _last;

        public CustomLoadBalancer(Func<Task<List<Service>>> services)
        {
            _services = services;
        }

        public async Task<Response<ServiceHostAndPort>> Lease(HttpContext httpContext)
        {
            var services = await _services();
            lock (_lock)
            {
                if (_last >= services.Count)
                {
                    _last = 0;
                }

                var next = services[_last];
                _last++;
                return new OkResponse<ServiceHostAndPort>(next.HostAndPort);
            }
        }

        public void Release(ServiceHostAndPort hostAndPort) { }        
    }
}
