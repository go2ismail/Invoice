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


    [Authorize(Roles = "Tax")]
    public class TaxController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaxController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tax
        public async Task<IActionResult> Index()
        {
                    return View(await _context.Tax.ToListAsync());
        }        

    // GET: Tax/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tax = await _context.Tax
                    .SingleOrDefaultAsync(m => m.taxId == id);
        if (tax == null)
        {
            return NotFound();
        }

        return View(tax);
    }


    // GET: Tax/Create
    public IActionResult Create()
    {
    return View();
    }




    // POST: Tax/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("taxId,taxLabel,taxRate,createdAt")] Tax tax)
    {
        if (ModelState.IsValid)
        {
            _context.Add(tax);
            await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }
        return View(tax);
    }

    // GET: Tax/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tax = await _context.Tax.SingleOrDefaultAsync(m => m.taxId == id);
        if (tax == null)
        {
            return NotFound();
        }
        return View(tax);
    }

    // POST: Tax/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("taxId,taxLabel,taxRate,createdAt")] Tax tax)
    {
        if (id != tax.taxId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(tax);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaxExists(tax.taxId))
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
        return View(tax);
    }

    // GET: Tax/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tax = await _context.Tax
                .SingleOrDefaultAsync(m => m.taxId == id);
        if (tax == null)
        {
            return NotFound();
        }

        return View(tax);
    }




    // POST: Tax/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var tax = await _context.Tax.SingleOrDefaultAsync(m => m.taxId == id);
            _context.Tax.Remove(tax);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TaxExists(string id)
    {
        return _context.Tax.Any(e => e.taxId == id);
    }

  }
}





namespace netcore.MVC
{
  public static partial class Pages
  {
      public static class Tax
      {
          public const string Controller = "Tax";
          public const string Action = "Index";
          public const string Role = "Tax";
          public const string Url = "/Tax/Index";
          public const string Name = "Tax";
      }
  }
}
namespace netcore.Models
{
  public partial class ApplicationUser
  {
      [Display(Name = "Tax")]
      public bool TaxRole { get; set; } = false;
  }
}



