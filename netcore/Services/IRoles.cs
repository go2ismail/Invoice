using netcore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Services
{
    public interface IRoles
    {
        Task UpdateRoles(ApplicationUser appUser, ApplicationUser currentUserLogin);
    }
}
