using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
// using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CRM {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            // var appSettingsSection = Configuration.GetSection("AppSettings");
            // services.Configure<AppSettings>(appSettingsSection);

            services.AddDistributedMemoryCache();

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
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

            // services.AddControllers();

            services.AddHttpContextAccessor();

            services.AddScoped<ICrmSession, CrmSession>();
            services.AddScoped<Apps.ICrmApp, Apps.CrmApp>();
            services.AddScoped<Domain.ICrmContext, Domain.CrmContext>();
            services.AddScoped<DAL.ICrmRepo, DAL.CrmRepo>();

            // services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.EnvironmentName == "Development")
                app.UseDeveloperExceptionPage();
            else
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseSession();
            // app.UseHttpContextItemsMiddleware();
            app.UseMvc();
        }
    }
}