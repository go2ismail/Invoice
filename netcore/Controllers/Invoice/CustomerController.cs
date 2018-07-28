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


    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {
                    return View(await _context.Customer.ToListAsync());
        }        

    // GET: Customer/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = await _context.Customer
                    .SingleOrDefaultAsync(m => m.customerId == id);
        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }


    // GET: Customer/Create
    public IActionResult Create()
    {
    return View();
    }




    // POST: Customer/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("customerId,customerName,contactName,address,phone,fax,email,website,taxRegisteredNumber,additionalInformation,createdAt")] Customer customer)
    {
        if (ModelState.IsValid)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }
        return View(customer);
    }

    // GET: Customer/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = await _context.Customer.SingleOrDefaultAsync(m => m.customerId == id);
        if (customer == null)
        {
            return NotFound();
        }
        return View(customer);
    }

    // POST: Customer/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("customerId,customerName,contactName,address,phone,fax,email,website,taxRegisteredNumber,additionalInformation,createdAt")] Customer customer)
    {
        if (id != customer.customerId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(customer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.customerId))
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
        return View(customer);
    }

    // GET: Customer/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = await _context.Customer
                .SingleOrDefaultAsync(m => m.customerId == id);
        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }




    // POST: Customer/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var customer = await _context.Customer.SingleOrDefaultAsync(m => m.customerId == id);
            _context.Customer.Remove(customer);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CustomerExists(string id)
    {
        return _context.Customer.Any(e => e.customerId == id);
    }

  }
}





namespace netcore.MVC
{
  public static partial class Pages
  {
      public static class Customer
      {
          public const string Controller = "Customer";
          public const string Action = "Index";
          public const string Role = "Customer";
          public const string Url = "/Customer/Index";
          public const string Name = "Customer";
      }
  }
}
namespace netcore.Models
{
  public partial class ApplicationUser
  {
      [Display(Name = "Customer")]
      public bool CustomerRole { get; set; } = false;
  }
}



