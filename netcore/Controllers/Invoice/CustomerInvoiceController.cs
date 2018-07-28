using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

using netcore.Data;
using netcore.Models.Invoice;

namespace netcore.Controllers.Invoice
{


    [Authorize(Roles = "CustomerInvoice")]
    public class CustomerInvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerInvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomerInvoice
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CustomerInvoice.Include(c => c.customer).Include(c => c.myCompany);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CustomerInvoice/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerInvoice = await _context.CustomerInvoice
                    .Include(c => c.customer)
                    .Include(c => c.myCompany)
                    .Include(x => x.customerInvoiceLine)
                    .SingleOrDefaultAsync(m => m.customerInvoiceId == id);
            if (customerInvoice == null)
            {
                return NotFound();
            }

            return View(customerInvoice);
        }


        // GET: CustomerInvoice/Create
        public IActionResult Create()
        {
            ViewData["customerId"] = new SelectList(_context.Customer, "customerId", "customerName");
            ViewData["myCompanyId"] = new SelectList(_context.Set<MyCompany>(), "myCompanyId", "companyName");
            CustomerInvoice obj = new CustomerInvoice();
            obj.invoiceDate = DateTime.UtcNow.Date;
            obj.dueDate = obj.invoiceDate.AddMonths(1).Date;
            return View(obj);
        }




        // POST: CustomerInvoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("customerInvoiceId,invoiceNumber,invoiceDate,invoiceReference,dueDate,myCompanyId,customerId,noteToRecipient,termsAndCondition,subTotal,taxAmount,discount,shipping,grandTotal,isPaid,HasChild,createdAt")] CustomerInvoice customerInvoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerInvoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["customerId"] = new SelectList(_context.Customer, "customerId", "customerId", customerInvoice.customerId);
            ViewData["myCompanyId"] = new SelectList(_context.Set<MyCompany>(), "myCompanyId", "myCompanyId", customerInvoice.myCompanyId);
            return View(customerInvoice);
        }

        // GET: CustomerInvoice/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerInvoice = await _context.CustomerInvoice.SingleOrDefaultAsync(m => m.customerInvoiceId == id);
            if (customerInvoice == null)
            {
                return NotFound();
            }
            ViewData["customerId"] = new SelectList(_context.Customer, "customerId", "customerId", customerInvoice.customerId);
            ViewData["myCompanyId"] = new SelectList(_context.Set<MyCompany>(), "myCompanyId", "myCompanyId", customerInvoice.myCompanyId);
            return View(customerInvoice);
        }

        // POST: CustomerInvoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("customerInvoiceId,invoiceNumber,invoiceDate,invoiceReference,dueDate,myCompanyId,customerId,noteToRecipient,termsAndCondition,subTotal,taxAmount,discount,shipping,grandTotal,isPaid,HasChild,createdAt")] CustomerInvoice customerInvoice)
        {
            if (id != customerInvoice.customerInvoiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    customerInvoice.grandTotal = customerInvoice.subTotal + customerInvoice.taxAmount - customerInvoice.discount + customerInvoice.shipping;
                    _context.Update(customerInvoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerInvoiceExists(customerInvoice.customerInvoiceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["customerId"] = new SelectList(_context.Customer, "customerId", "customerId", customerInvoice.customerId);
            ViewData["myCompanyId"] = new SelectList(_context.Set<MyCompany>(), "myCompanyId", "myCompanyId", customerInvoice.myCompanyId);
            return View(customerInvoice);
        }

        // GET: CustomerInvoice/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerInvoice = await _context.CustomerInvoice
                    .Include(c => c.customer)
                    .Include(c => c.myCompany)
                    .Include(x => x.customerInvoiceLine)
                    .SingleOrDefaultAsync(m => m.customerInvoiceId == id);
            if (customerInvoice == null)
            {
                return NotFound();
            }

            return View(customerInvoice);
        }




        // POST: CustomerInvoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customerInvoice = await _context.CustomerInvoice
                .Include(x => x.customerInvoiceLine)
                .SingleOrDefaultAsync(m => m.customerInvoiceId == id);
            _context.CustomerInvoice.Remove(customerInvoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerInvoiceExists(string id)
        {
            return _context.CustomerInvoice.Any(e => e.customerInvoiceId == id);
        }

        public async Task<IActionResult> ShowInvoice(string id)
        {
            CustomerInvoice obj = await _context.CustomerInvoice
                .Where(x => x.customerInvoiceId.Equals(id))
                    .Include(x => x.customerInvoiceLine)
                        .ThenInclude(c => c.item)
                .Include(x => x.customer)
                .Include(x => x.myCompany).FirstOrDefaultAsync();

            return View(obj);
        }

        public async Task<IActionResult> PrintInvoice(string id)
        {
            CustomerInvoice obj = await _context.CustomerInvoice
                .Where(x => x.customerInvoiceId.Equals(id))
                    .Include(x => x.customerInvoiceLine)
                        .ThenInclude(c => c.item)
                .Include(x => x.customer)
                .Include(x => x.myCompany).FirstOrDefaultAsync();

            return View(obj);
        }

    }
}





namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class CustomerInvoice
        {
            public const string Controller = "CustomerInvoice";
            public const string Action = "Index";
            public const string Role = "CustomerInvoice";
            public const string Url = "/CustomerInvoice/Index";
            public const string Name = "CustomerInvoice";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "CustomerInvoice")]
        public bool CustomerInvoiceRole { get; set; } = false;
    }
}



