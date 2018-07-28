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


    [Authorize(Roles = "VendorInvoice")]
    public class VendorInvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorInvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VendorInvoice
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VendorInvoice.Include(v => v.myCompany).Include(v => v.vendor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: VendorInvoice/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorInvoice = await _context.VendorInvoice
                    .Include(v => v.myCompany)
                    .Include(v => v.vendor)
                        .SingleOrDefaultAsync(m => m.vendorInvoiceId == id);
            if (vendorInvoice == null)
            {
                return NotFound();
            }

            return View(vendorInvoice);
        }


        // GET: VendorInvoice/Create
        public IActionResult Create()
        {
            ViewData["myCompanyId"] = new SelectList(_context.MyCompany, "myCompanyId", "companyName");
            ViewData["vendorId"] = new SelectList(_context.Vendor, "vendorId", "vendorName");
            VendorInvoice obj = new VendorInvoice();
            obj.invoiceDate = DateTime.UtcNow.Date;
            obj.dueDate = obj.invoiceDate.AddMonths(1).Date;
            return View(obj);
        }




        // POST: VendorInvoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("vendorInvoiceId,invoiceNumber,originalInvoiceNumber,invoiceDate,invoiceReference,dueDate,myCompanyId,vendorId,note,termsAndCondition,subTotal,taxAmount,discount,shipping,grandTotal,isPaid,HasChild,createdAt")] VendorInvoice vendorInvoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorInvoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["myCompanyId"] = new SelectList(_context.MyCompany, "myCompanyId", "companyName", vendorInvoice.myCompanyId);
            ViewData["vendorId"] = new SelectList(_context.Vendor, "vendorId", "vendorName", vendorInvoice.vendorId);
            return View(vendorInvoice);
        }

        // GET: VendorInvoice/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorInvoice = await _context.VendorInvoice.SingleOrDefaultAsync(m => m.vendorInvoiceId == id);
            if (vendorInvoice == null)
            {
                return NotFound();
            }
            ViewData["myCompanyId"] = new SelectList(_context.MyCompany, "myCompanyId", "companyName", vendorInvoice.myCompanyId);
            ViewData["vendorId"] = new SelectList(_context.Vendor, "vendorId", "vendorName", vendorInvoice.vendorId);
            return View(vendorInvoice);
        }

        // POST: VendorInvoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("vendorInvoiceId,invoiceNumber,originalInvoiceNumber,invoiceDate,invoiceReference,dueDate,myCompanyId,vendorId,note,termsAndCondition,subTotal,taxAmount,discount,shipping,grandTotal,isPaid,HasChild,createdAt")] VendorInvoice vendorInvoice)
        {
            if (id != vendorInvoice.vendorInvoiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorInvoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorInvoiceExists(vendorInvoice.vendorInvoiceId))
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
            ViewData["myCompanyId"] = new SelectList(_context.MyCompany, "myCompanyId", "companyName", vendorInvoice.myCompanyId);
            ViewData["vendorId"] = new SelectList(_context.Vendor, "vendorId", "vendorName", vendorInvoice.vendorId);
            return View(vendorInvoice);
        }

        // GET: VendorInvoice/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorInvoice = await _context.VendorInvoice
                    .Include(v => v.myCompany)
                    .Include(v => v.vendor)
                    .SingleOrDefaultAsync(m => m.vendorInvoiceId == id);
            if (vendorInvoice == null)
            {
                return NotFound();
            }

            return View(vendorInvoice);
        }




        // POST: VendorInvoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vendorInvoice = await _context.VendorInvoice.Include(x => x.vendorInvoiceLine).SingleOrDefaultAsync(m => m.vendorInvoiceId == id);
            _context.VendorInvoice.Remove(vendorInvoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorInvoiceExists(string id)
        {
            return _context.VendorInvoice.Any(e => e.vendorInvoiceId == id);
        }

    }
}





namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class VendorInvoice
        {
            public const string Controller = "VendorInvoice";
            public const string Action = "Index";
            public const string Role = "VendorInvoice";
            public const string Url = "/VendorInvoice/Index";
            public const string Name = "VendorInvoice";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "VendorInvoice")]
        public bool VendorInvoiceRole { get; set; } = false;
    }
}



