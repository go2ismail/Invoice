using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using netcore.Models;
using netcore.Models.Invoice;

namespace netcore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<netcore.Models.ApplicationUser> ApplicationUser { get; set; }

        public DbSet<netcore.Models.Invoice.Currency> Currency { get; set; }

        public DbSet<netcore.Models.Invoice.Customer> Customer { get; set; }

        public DbSet<netcore.Models.Invoice.CustomerInvoice> CustomerInvoice { get; set; }

        public DbSet<netcore.Models.Invoice.Item> Item { get; set; }

        public DbSet<netcore.Models.Invoice.MyCompany> MyCompany { get; set; }

        public DbSet<netcore.Models.Invoice.Tax> Tax { get; set; }

        public DbSet<netcore.Models.Invoice.Vendor> Vendor { get; set; }

        public DbSet<netcore.Models.Invoice.VendorInvoice> VendorInvoice { get; set; }

        public DbSet<netcore.Models.Invoice.CustomerInvoiceLine> CustomerInvoiceLine { get; set; }

        public DbSet<netcore.Models.Invoice.VendorInvoiceLine> VendorInvoiceLine { get; set; }
        
    }
}
