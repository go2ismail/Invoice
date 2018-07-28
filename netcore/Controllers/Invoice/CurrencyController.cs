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


    [Authorize(Roles = "Currency")]
    public class CurrencyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CurrencyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Currency
        public async Task<IActionResult> Index()
        {
                    return View(await _context.Currency.ToListAsync());
        }        

    // GET: Currency/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var currency = await _context.Currency
                    .SingleOrDefaultAsync(m => m.currencyId == id);
        if (currency == null)
        {
            return NotFound();
        }

        return View(currency);
    }


    // GET: Currency/Create
    public IActionResult Create()
    {
    return View();
    }




    // POST: Currency/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("currencyId,currencyCode,currencyName,createdAt")] Currency currency)
    {
        if (ModelState.IsValid)
        {
            _context.Add(currency);
            await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }
        return View(currency);
    }

    // GET: Currency/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var currency = await _context.Currency.SingleOrDefaultAsync(m => m.currencyId == id);
        if (currency == null)
        {
            return NotFound();
        }
        return View(currency);
    }

    // POST: Currency/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("currencyId,currencyCode,currencyName,createdAt")] Currency currency)
    {
        if (id != currency.currencyId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(currency);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyExists(currency.currencyId))
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
        return View(currency);
    }

    // GET: Currency/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var currency = await _context.Currency
                .SingleOrDefaultAsync(m => m.currencyId == id);
        if (currency == null)
        {
            return NotFound();
        }

        return View(currency);
    }




    // POST: Currency/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var currency = await _context.Currency.SingleOrDefaultAsync(m => m.currencyId == id);
            _context.Currency.Remove(currency);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CurrencyExists(string id)
    {
        return _context.Currency.Any(e => e.currencyId == id);
    }

  }
}





namespace netcore.MVC
{
  public static partial class Pages
  {
      public static class Currency
      {
          public const string Controller = "Currency";
          public const string Action = "Index";
          public const string Role = "Currency";
          public const string Url = "/Currency/Index";
          public const string Name = "Currency";
      }
  }
}
namespace netcore.Models
{
  public partial class ApplicationUser
  {
      [Display(Name = "Currency")]
      public bool CurrencyRole { get; set; } = false;
  }
}



