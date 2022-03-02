using Contact.DAL.Abstract;
using Contact.DAL.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Entities;
using Contracts;
using Entities.UnitOfWork;
using Entities.Repository;
using Logging;

namespace MyContactList.ContactMicroService.UnitTests
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ContactListContext>(opt =>
                opt.UseNpgsql("User ID=test;Password=test;Server=localhost;Port=5432;Database=ContactListDB;Integrated Security=true;Pooling=true;")
            );

            services.AddControllers().AddApplicationPart(Assembly.Load("Contact.API")).AddControllersAsServices();

            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<ILoggerManager, LoggerManager>();
            services.TryAddScoped<DbContext, ContactListContext>();
            services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.TryAddScoped<IUnitOfWork, UnitOfWork>();
            services.TryAddScoped<IContactOperation, ContactOperation>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
