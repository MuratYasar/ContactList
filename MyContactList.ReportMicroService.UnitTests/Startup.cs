using Contracts;
using Entities;
using Entities.Repository;
using Entities.UnitOfWork;
using Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Report.DAL.Abstract;
using Report.DAL.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyContactList.ReportMicroService.UnitTests
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
                opt.UseNpgsql("User ID=postgres;Password=muratyasar;Server=localhost;Port=5432;Database=ContactListDB;Integrated Security=true;Pooling=true;")
            );

            services.AddControllers().AddApplicationPart(Assembly.Load("Report.API")).AddControllersAsServices();

            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<ILoggerManager, LoggerManager>();
            services.TryAddScoped<DbContext, ContactListContext>();
            services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.TryAddScoped<IUnitOfWork, UnitOfWork>();
            services.TryAddScoped<IReportOperation, ReportOperation>();
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
