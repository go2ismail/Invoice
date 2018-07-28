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


    [Authorize(Roles = "Item")]
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Item
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Item.Include(i => i.currency).Include(i => i.tax);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Item/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                    .Include(i => i.currency)
                    .Include(i => i.tax)
                        .SingleOrDefaultAsync(m => m.itemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }


        // GET: Item/Create
        public IActionResult Create()
        {
            ViewData["currencyId"] = new SelectList(_context.Currency, "currencyId", "currencyCode");
            ViewData["taxId"] = new SelectList(_context.Set<Tax>(), "taxId", "taxLabel");
            return View();
        }




        // POST: Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("itemId,itemName,itemDescription,price,taxId,currencyId,createdAt")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["currencyId"] = new SelectList(_context.Currency, "currencyId", "currencyCode", item.currencyId);
            ViewData["taxId"] = new SelectList(_context.Set<Tax>(), "taxId", "taxLabel", item.taxId);
            return View(item);
        }

        // GET: Item/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.SingleOrDefaultAsync(m => m.itemId == id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["currencyId"] = new SelectList(_context.Currency, "currencyId", "currencyCode", item.currencyId);
            ViewData["taxId"] = new SelectList(_context.Set<Tax>(), "taxId", "taxLabel", item.taxId);
            return View(item);
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("itemId,itemName,itemDescription,price,taxId,currencyId,createdAt")] Item item)
        {
            if (id != item.itemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.itemId))
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
            ViewData["currencyId"] = new SelectList(_context.Currency, "currencyId", "currencyCode", item.currencyId);
            ViewData["taxId"] = new SelectList(_context.Set<Tax>(), "taxId", "taxLabel", item.taxId);
            return View(item);
        }

        // GET: Item/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                    .Include(i => i.currency)
                    .Include(i => i.tax)
                    .SingleOrDefaultAsync(m => m.itemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }




        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var item = await _context.Item.SingleOrDefaultAsync(m => m.itemId == id);
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(string id)
        {
            return _context.Item.Any(e => e.itemId == id);
        }

    }
}





namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class Item
        {
            public const string Controller = "Item";
            public const string Action = "Index";
            public const string Role = "Item";
            public const string Url = "/Item/Index";
            public const string Name = "Item";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "Item")]
        public bool ItemRole { get; set; } = false;
    }
}



