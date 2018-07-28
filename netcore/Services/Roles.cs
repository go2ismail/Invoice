using Microsoft.AspNetCore.Identity;
using netcore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Services
{
    public class Roles : IRoles
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Roles(UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task UpdateRoles(ApplicationUser appUser,
            ApplicationUser currentLoginUser)
        {
            try
            {
                IList<string> roles = await _userManager.GetRolesAsync(appUser);
                foreach (var item in roles)
                {
                    if (!item.Contains("Line"))
                    {
                        await _userManager.RemoveFromRoleAsync(appUser, item);
                    }
                    
                }

                Type t = appUser.GetType();
                foreach (System.Reflection.PropertyInfo item in t.GetProperties())
                {
                    if (item.Name.Contains("Role"))
                    {
                        bool vlue = (bool)item.GetValue(appUser, null);
                        if (vlue)
                        {
                            string roleName = item.Name.Replace("Role", "");
                            if (!await _roleManager.RoleExistsAsync(roleName))
                                await _roleManager.CreateAsync(new IdentityRole(roleName));
                            await _userManager.AddToRoleAsync(appUser, roleName);
                        }
                    }
                }

                
                

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
