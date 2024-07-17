using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
//...
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GrandTravelPackages.Services;
using GrandTravelPackages.Models;

namespace GrandTravelPackages
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<IDataService<Customer>, DataService<Customer>>();
            services.AddScoped<IDataService<TravelProvider>, DataService<TravelProvider>>();
            services.AddScoped<IDataService<Package>, DataService<Package>>();
            services.AddScoped<IDataService<Order>, DataService<Order>>();
            services.AddScoped<IDataService<Feedback>, DataService<Feedback>>();

            //add identity services
            services.AddIdentity<IdentityUser, IdentityRole>
                (
                   config =>
                   {
                       config.Password.RequireDigit = true;
                       config.Password.RequiredLength = 8;
                       config.Password.RequiredUniqueChars = 2;
                       config.Password.RequireLowercase = true;
                       config.Password.RequireNonAlphanumeric = true;
                       config.Password.RequireUppercase = true;
                   }
                ).AddEntityFrameworkStores<MyDbContext>();
            services.AddDbContext<MyDbContext>();
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
            //app.UseIdentity();
           
            app.UseMvcWithDefaultRoute(); // Our default rout would be Home/Index

            //SeedHelper.Seed(app.ApplicationServices).Wait();

        }
    }
}
