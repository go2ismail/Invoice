using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invoice
{
    public class MyCompany : INetcoreBasic
    {
        public MyCompany()
        {
            this.createdAt = DateTime.UtcNow;
        }

        public string myCompanyId { get; set; }

        [Display(Name = "Company Name")]
        [Required]
        public string companyName { get; set; }

        [Display(Name = "Contact Name")]
        [Required]
        public string contactName { get; set; }

        [Display(Name = "Address")]
        [Required]
        public string address { get; set; }

        [Display(Name = "Phone")]
        [Required]
        public string phone { get; set; }

        [Display(Name = "Fax")]
        public string fax { get; set; }

        [Display(Name = "Email")]
        [Required]
        public string email { get; set; }

        [Display(Name = "Website")]
        public string website { get; set; }

        [Display(Name = "Tax Registered Number")]
        [Required]
        public string taxRegisteredNumber { get; set; }

        [Display(Name = "Additional Information")]
        public string additionalInformation { get; set; }

    }
}
