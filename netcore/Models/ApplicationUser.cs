using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace netcore.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public partial class ApplicationUser : IdentityUser
    {
        public string profilePictureUrl { get; set; } = "/images/empty_profile.png";
        public bool isSuperAdmin { get; set; } = false;

      

        [Display(Name = "Home Roles")]
        public bool HomeRole { get; set; } = false;

        [Display(Name = "Roles")]
        public bool ApplicationUserRole { get; set; } = false;
    }
}
