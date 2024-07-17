using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//..
using GrandTravelPackages.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace GrandTravelPackages.Services
{
    public static class SeedHelper
    {

        public static async Task Seed(IServiceProvider provider)
        {
            var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
            //add profile service

            //DataService<Customer> profileService = new DataService<Customer>();

            using (var scope = scopeFactory.CreateScope())
            {
                UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                //add Customer role
                if (await roleManager.FindByNameAsync("Customer") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("Customer"));
                }
                //add Admin role
                if (await roleManager.FindByNameAsync("Admin") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                //add Manager role
                if (await roleManager.FindByNameAsync("TravelProvider") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("TravelProvider"));
                }

                //add default Admin
                if (await userManager.FindByNameAsync("Admin") == null)
                {
                    IdentityUser admin = new IdentityUser("Admin");
                    await userManager.CreateAsync(admin, "Apple3###");
                    await userManager.AddToRoleAsync(admin, "Admin");
                    //add a default profile for this admin
                    //Customer profile = new Customer
                    //{
                    //    UserId = admin.Id
                    //};
                    //profileService.Create(profile);

                }
            }
        }

    }
}
