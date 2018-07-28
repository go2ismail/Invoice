using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invoice
{
    public class Tax : INetcoreBasic
    {
        public Tax()
        {
            this.createdAt = DateTime.UtcNow;
        }

        public string taxId { get; set; }

        [Display(Name = "Tax Label")]
        [Required]
        public string taxLabel { get; set; }

        [Display(Name = "Tax Rate")]
        [Required]
        public float taxRate { get; set; }
    }
}
