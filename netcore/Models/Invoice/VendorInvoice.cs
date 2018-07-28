using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invoice
{
    public class VendorInvoice : INetcoreMasterChild
    {
        public VendorInvoice()
        {
            this.createdAt = DateTime.UtcNow;
            this.invoiceNumber = DateTime.UtcNow.Date.Year.ToString() +
                DateTime.UtcNow.Date.Month.ToString() +
                DateTime.UtcNow.Date.Day.ToString() + Guid.NewGuid().ToString().Substring(0, 4).ToUpper() + "VGENINV";
            this.dueDate = DateTime.UtcNow.Date.AddMonths(1);
            this.subTotal = 0;
            this.taxAmount = 0;
            this.discount = 0;
            this.shipping = 0;
            this.grandTotal = 0;
        }

        public string vendorInvoiceId { get; set; }

        [Display(Name = "Invoice Number")]
        [Required]
        public string invoiceNumber { get; set; }

        [Display(Name = "Vendor Original Invoice Number")]
        [Required]
        public string originalInvoiceNumber { get; set; }

        [Display(Name = "Invoice Date")]
        [Required]
        public DateTime invoiceDate { get; set; }

        [Display(Name = "Invoice Reference")]
        public string invoiceReference { get; set; }

        [Display(Name = "Due Date")]
        [Required]
        public DateTime dueDate { get; set; }

        [Display(Name = "My Company")]
        [Required]
        public string myCompanyId { get; set; }
        public MyCompany myCompany { get; set; }

        [Display(Name = "Vendor")]
        [Required]
        public string vendorId { get; set; }
        public Vendor vendor { get; set; }

        [Display(Name = "Note")]
        public string note { get; set; }

        [Display(Name = "Terms And Condition")]
        public string termsAndCondition { get; set; }

        [Display(Name = "Sub Total")]
        public decimal subTotal { get; set; }

        [Display(Name = "Tax Amount")]
        public decimal taxAmount { get; set; }

        [Display(Name = "Discount")]
        public decimal discount { get; set; }

        [Display(Name = "Shipping")]
        public decimal shipping { get; set; }

        [Display(Name = "Grand Total")]
        public decimal grandTotal { get; set; }

        [Display(Name = "Already Paid")]
        public bool isPaid { get; set; }

        public List<VendorInvoiceLine> vendorInvoiceLine { get; set; } = new List<VendorInvoiceLine>();
    }

    public class VendorInvoiceLine : INetcoreBasic
    {
        public VendorInvoiceLine()
        {
            this.createdAt = DateTime.UtcNow;
            this.price = 0;
        }

        public string vendorInvoiceLineId { get; set; }

        [Display(Name = "Item")]
        [Required]
        public string itemId { get; set; }
        public Item item { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Quantity")]
        [Required]
        public float quantity { get; set; }

        [Display(Name = "Price")]
        public decimal price { get; set; }

        [Display(Name = "Tax")]
        [Required]
        public string taxId { get; set; }

        [Display(Name = "Tax Amount")]
        public decimal taxAmount { get; set; }

        [Display(Name = "Amount")]
        public decimal amount { get; set; }

        [Display(Name = "Total Amount")]
        public decimal totalAmount { get; set; }

        [Display(Name = "Vendor Invoice")]
        public string vendorInvoiceId { get; set; }
        [Display(Name = "Vendor Invoice")]
        public VendorInvoice vendorInvoice { get; set; }
    }
}
