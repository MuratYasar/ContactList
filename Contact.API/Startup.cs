using Contact.DAL.Abstract;
using Contact.DAL.Concrete;
using Contracts;
using Entities;
using Entities.Repository;
using Entities.UnitOfWork;
using Logging;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Contact.API
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
            services.AddDbContext<ContactListContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ContactListContext).Assembly.FullName))
            );

            services.AddControllers();

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //        builder => builder
            //        .WithOrigins("https://localhost:5001", "https://localhost:5000")
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .SetIsOriginAllowed((host) => true)
            //        .AllowCredentials());
            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Contact.API", Version = "v1" });
            });            

            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<ILoggerManager, LoggerManager>();
            services.TryAddScoped<DbContext, ContactListContext>();
            services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.TryAddScoped<IUnitOfWork, UnitOfWork>();
            services.TryAddScoped<IContactOperation, ContactOperation>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroService Contact.API v1"));
            }

            //app.UseCors("CorsPolicy");

            app.UseRouting();            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
