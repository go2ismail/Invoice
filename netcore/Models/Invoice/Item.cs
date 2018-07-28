using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invoice
{
    public class Item : INetcoreBasic
    {
        public Item()
        {
            this.createdAt = DateTime.UtcNow;
        }

        public string itemId { get; set; }

        [Display(Name = "Item Name")]
        [Required]
        public string itemName { get; set; }

        [Display(Name = "Item Description")]
        [Required]
        public string itemDescription { get; set; }

        [Display(Name = "Price")]
        [Required]
        public decimal price { get; set; }

        [Display(Name = "Tax")]
        [Required]
        public string taxId { get; set; }
        public Tax tax { get; set; }

        [Display(Name = "Currency")]
        [Required]
        public string currencyId { get; set; }
        public Currency currency { get; set; }
    }
}
