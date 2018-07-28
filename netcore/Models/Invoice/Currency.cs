using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invoice
{
    public class Currency : INetcoreBasic
    {
        public Currency()
        {
            this.createdAt = DateTime.UtcNow;
        }

        public string currencyId { get; set; }

        [Display(Name = "Currency Code")]
        [Required]
        public string currencyCode { get; set; }

        [Display(Name = "Currency Name")]
        [Required]
        public string currencyName { get; set; }
    }
}
