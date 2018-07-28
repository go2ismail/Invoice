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


    [Authorize(Roles = "CustomerInvoiceLine")]
    public class CustomerInvoiceLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerInvoiceLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomerInvoiceLine
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CustomerInvoiceLine.Include(c => c.customerInvoice).Include(c => c.item);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CustomerInvoiceLine/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerInvoiceLine = await _context.CustomerInvoiceLine
                    .Include(c => c.customerInvoice)
                    .Include(c => c.item)
                        .SingleOrDefaultAsync(m => m.customerInvoiceLineId == id);
            if (customerInvoiceLine == null)
            {
                return NotFound();
            }

            return View(customerInvoiceLine);
        }


        // GET: CustomerInvoiceLine/Create
        public IActionResult Create(string masterid, string id)
        {
            var check = _context.CustomerInvoiceLine.SingleOrDefault(m => m.customerInvoiceLineId == id);
            var selected = _context.CustomerInvoice.SingleOrDefault(m => m.customerInvoiceId == masterid);
            ViewData["customerInvoiceId"] = new SelectList(_context.CustomerInvoice, "customerInvoiceId", "invoiceNumber");
            ViewData["itemId"] = new SelectList(_context.Item, "itemId", "itemName");
            if (check == null)
            {
                CustomerInvoiceLine objline = new CustomerInvoiceLine();
                objline.customerInvoice = selected;
                objline.customerInvoiceId = masterid;
                return View(objline);
            }
            else
            {
                return View(check);
            }
        }




        // POST: CustomerInvoiceLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("customerInvoiceLineId,itemId,description,quantity,price,taxId,taxAmount,amount,totalAmount,customerInvoiceId,createdAt")] CustomerInvoiceLine customerInvoiceLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerInvoiceLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["customerInvoiceId"] = new SelectList(_context.CustomerInvoice, "customerInvoiceId", "invoiceNumber", customerInvoiceLine.customerInvoiceId);
            ViewData["itemId"] = new SelectList(_context.Item, "itemId", "itemName", customerInvoiceLine.itemId);
            return View(customerInvoiceLine);
        }

        // GET: CustomerInvoiceLine/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerInvoiceLine = await _context.CustomerInvoiceLine.SingleOrDefaultAsync(m => m.customerInvoiceLineId == id);
            if (customerInvoiceLine == null)
            {
                return NotFound();
            }
            ViewData["customerInvoiceId"] = new SelectList(_context.CustomerInvoice, "customerInvoiceId", "invoiceNumber", customerInvoiceLine.customerInvoiceId);
            ViewData["itemId"] = new SelectList(_context.Item, "itemId", "itemName", customerInvoiceLine.itemId);
            return View(customerInvoiceLine);
        }

        // POST: CustomerInvoiceLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("customerInvoiceLineId,itemId,description,quantity,price,taxId,taxAmount,amount,totalAmount,customerInvoiceId,createdAt")] CustomerInvoiceLine customerInvoiceLine)
        {
            if (id != customerInvoiceLine.customerInvoiceLineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerInvoiceLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerInvoiceLineExists(customerInvoiceLine.customerInvoiceLineId))
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
            ViewData["customerInvoiceId"] = new SelectList(_context.CustomerInvoice, "customerInvoiceId", "invoiceNumber", customerInvoiceLine.customerInvoiceId);
            ViewData["itemId"] = new SelectList(_context.Item, "itemId", "itemName", customerInvoiceLine.itemId);
            return View(customerInvoiceLine);
        }

        // GET: CustomerInvoiceLine/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerInvoiceLine = await _context.CustomerInvoiceLine
                    .Include(c => c.customerInvoice)
                    .Include(c => c.item)
                    .SingleOrDefaultAsync(m => m.customerInvoiceLineId == id);
            if (customerInvoiceLine == null)
            {
                return NotFound();
            }

            return View(customerInvoiceLine);
        }




        // POST: CustomerInvoiceLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customerInvoiceLine = await _context.CustomerInvoiceLine.SingleOrDefaultAsync(m => m.customerInvoiceLineId == id);
            _context.CustomerInvoiceLine.Remove(customerInvoiceLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerInvoiceLineExists(string id)
        {
            return _context.CustomerInvoiceLine.Any(e => e.customerInvoiceLineId == id);
        }

    }
}





namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class CustomerInvoiceLine
        {
            public const string Controller = "CustomerInvoiceLine";
            public const string Action = "Index";
            public const string Role = "CustomerInvoiceLine";
            public const string Url = "/CustomerInvoiceLine/Index";
            public const string Name = "CustomerInvoiceLine";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "CustomerInvoiceLine")]
        public bool CustomerInvoiceLineRole { get; set; } = false;
    }
}



