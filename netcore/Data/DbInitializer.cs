using Microsoft.AspNetCore.Identity;
using netcore.Models;
using netcore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace netcore.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            INetcoreService netcoreService)
        {
            context.Database.EnsureCreated();

            //check for users
            if (context.ApplicationUser.Any())
            {
                return; //if user is not empty, DB has been seed
            }

            //init app with super admin user
            await netcoreService.CreateDefaultSuperAdmin();
            
        }
    }
}
