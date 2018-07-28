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


    [Authorize(Roles = "VendorInvoiceLine")]
    public class VendorInvoiceLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorInvoiceLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VendorInvoiceLine
        public async Task<IActionResult> Index()
        {
                    var applicationDbContext = _context.VendorInvoiceLine.Include(v => v.item).Include(v => v.vendorInvoice);
                    return View(await applicationDbContext.ToListAsync());
        }        

    // GET: VendorInvoiceLine/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vendorInvoiceLine = await _context.VendorInvoiceLine
                .Include(v => v.item)
                .Include(v => v.vendorInvoice)
                    .SingleOrDefaultAsync(m => m.vendorInvoiceLineId == id);
        if (vendorInvoiceLine == null)
        {
            return NotFound();
        }

        return View(vendorInvoiceLine);
    }


    // GET: VendorInvoiceLine/Create
    public IActionResult Create(string masterid, string id)
    {
        var check = _context.VendorInvoiceLine.SingleOrDefault(m => m.vendorInvoiceLineId == id);
        var selected = _context.VendorInvoice.SingleOrDefault(m => m.vendorInvoiceId == masterid);
            ViewData["itemId"] = new SelectList(_context.Item, "itemId", "itemName");
            ViewData["vendorInvoiceId"] = new SelectList(_context.VendorInvoice, "vendorInvoiceId", "invoiceNumber");
        if (check == null)
        {
            VendorInvoiceLine objline = new VendorInvoiceLine();
            objline.vendorInvoice = selected;
            objline.vendorInvoiceId = masterid;
            return View(objline);
        }
        else
        {
            return View(check);
        }
    }




    // POST: VendorInvoiceLine/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("vendorInvoiceLineId,itemId,description,quantity,price,taxId,taxAmount,amount,totalAmount,vendorInvoiceId,createdAt")] VendorInvoiceLine vendorInvoiceLine)
    {
        if (ModelState.IsValid)
        {
            _context.Add(vendorInvoiceLine);
            await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }
                ViewData["itemId"] = new SelectList(_context.Item, "itemId", "itemName", vendorInvoiceLine.itemId);
                ViewData["vendorInvoiceId"] = new SelectList(_context.VendorInvoice, "vendorInvoiceId", "invoiceNumber", vendorInvoiceLine.vendorInvoiceId);
        return View(vendorInvoiceLine);
    }

    // GET: VendorInvoiceLine/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vendorInvoiceLine = await _context.VendorInvoiceLine.SingleOrDefaultAsync(m => m.vendorInvoiceLineId == id);
        if (vendorInvoiceLine == null)
        {
            return NotFound();
        }
                ViewData["itemId"] = new SelectList(_context.Item, "itemId", "itemName", vendorInvoiceLine.itemId);
                ViewData["vendorInvoiceId"] = new SelectList(_context.VendorInvoice, "vendorInvoiceId", "invoiceNumber", vendorInvoiceLine.vendorInvoiceId);
        return View(vendorInvoiceLine);
    }

    // POST: VendorInvoiceLine/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("vendorInvoiceLineId,itemId,description,quantity,price,taxId,taxAmount,amount,totalAmount,vendorInvoiceId,createdAt")] VendorInvoiceLine vendorInvoiceLine)
    {
        if (id != vendorInvoiceLine.vendorInvoiceLineId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(vendorInvoiceLine);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorInvoiceLineExists(vendorInvoiceLine.vendorInvoiceLineId))
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
                ViewData["itemId"] = new SelectList(_context.Item, "itemId", "itemName", vendorInvoiceLine.itemId);
                ViewData["vendorInvoiceId"] = new SelectList(_context.VendorInvoice, "vendorInvoiceId", "invoiceNumber", vendorInvoiceLine.vendorInvoiceId);
        return View(vendorInvoiceLine);
    }

    // GET: VendorInvoiceLine/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vendorInvoiceLine = await _context.VendorInvoiceLine
                .Include(v => v.item)
                .Include(v => v.vendorInvoice)
                .SingleOrDefaultAsync(m => m.vendorInvoiceLineId == id);
        if (vendorInvoiceLine == null)
        {
            return NotFound();
        }

        return View(vendorInvoiceLine);
    }




    // POST: VendorInvoiceLine/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var vendorInvoiceLine = await _context.VendorInvoiceLine.SingleOrDefaultAsync(m => m.vendorInvoiceLineId == id);
            _context.VendorInvoiceLine.Remove(vendorInvoiceLine);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool VendorInvoiceLineExists(string id)
    {
        return _context.VendorInvoiceLine.Any(e => e.vendorInvoiceLineId == id);
    }

  }
}





namespace netcore.MVC
{
  public static partial class Pages
  {
      public static class VendorInvoiceLine
      {
          public const string Controller = "VendorInvoiceLine";
          public const string Action = "Index";
          public const string Role = "VendorInvoiceLine";
          public const string Url = "/VendorInvoiceLine/Index";
          public const string Name = "VendorInvoiceLine";
      }
  }
}
namespace netcore.Models
{
  public partial class ApplicationUser
  {
      [Display(Name = "VendorInvoiceLine")]
      public bool VendorInvoiceLineRole { get; set; } = false;
  }
}



