using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMExpenses.Web.Data;
using AMExpenses.Web.Data.Entities;
using AMExpenses.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AMExpenses.Web
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<StoreUser, IdentityRole>(cfg => { cfg.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<AMExpensesContext>();

            services.AddAuthentication().AddCookie();

            services.AddDbContext<AMExpensesContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("AMExpensesConnectionString"));
            });

            services.AddTransient<IBalanceUpdater, BalanceUpdater>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(cfg =>
            {
                cfg.MapRoute("Default",
                    "{controller}/{action}/{id?}",
                    new {Controller = "App", Action = "Balance"});
            });
        }
    }
}