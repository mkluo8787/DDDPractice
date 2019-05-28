using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NHibernate.NetCore;

namespace CRM {
    public class Startup {
        public Startup(
            // ILoggerFactory factory,
            IConfiguration configuration) {

            Configuration = configuration;
            // factory.UseAsHibernateLoggerFactory();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {

            // var appSettingsSection = Configuration.GetSection("AppSettings");
            // services.Configure<AppSettings>(appSettingsSection);

            services.Configure<IISServerOptions>(options => {
                options.AutomaticAuthentication = false;
            });

            services.AddDistributedMemoryCache();

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromSeconds(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.Name = "crm";
                options.Cookie.IsEssential = true;
            });

            var crmControllerAssembly = typeof(Controllers.CrmController).Assembly;
            services.AddMvc(opt => opt.EnableEndpointRouting = false)
                .AddNewtonsoftJson()
                .AddApplicationPart(crmControllerAssembly).AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddHttpContextAccessor();

            var path = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "hibernate.config"
            );
            services.AddHibernate(path);

            services.AddScoped<Apps.ICrmSession, Apps.CrmSession>();
            services.AddScoped<Apps.ICrmApp, Apps.CrmApp>();
            services.AddScoped<Core.ICrmCore, Core.CrmCore>();

            // User Context
            AddScopedReadContext<Domain.User, DAL.USER, DAL.UserMapper>(services);

            // Legacy User Context
            AddScopedReadContext<Domain.LegacyUser, DAL.BWApuser, DAL.LegacyUserMapper>(services);
        }

        // TODO: Move it to another file.
        public void AddScopedReadContext<T, TD, TMapper>(
            IServiceCollection services)
        where T : Domain.Entity
        where TD : class
        where TMapper : class, DAL.IReadMapper<T, TD> {
            services
            .AddScoped<Domain.IReadContext<T>, Domain.ReadContext<T, TD>>()
            .AddScoped<DAL.IReadMapper<T, TD>, TMapper>()
            .AddScoped<DAL.IRepository<TD>, DAL.Repository<TD>>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.EnvironmentName == "Development")
                app.UseDeveloperExceptionPage();
            //else
            //    app.UseHsts();

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseMvc();
        }
    }
}