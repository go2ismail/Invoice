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


    [Authorize(Roles = "MyCompany")]
    public class MyCompanyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyCompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MyCompany
        public async Task<IActionResult> Index()
        {
                    return View(await _context.MyCompany.ToListAsync());
        }        

    // GET: MyCompany/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var myCompany = await _context.MyCompany
                    .SingleOrDefaultAsync(m => m.myCompanyId == id);
        if (myCompany == null)
        {
            return NotFound();
        }

        return View(myCompany);
    }


    // GET: MyCompany/Create
    public IActionResult Create()
    {
    return View();
    }




    // POST: MyCompany/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("myCompanyId,companyName,contactName,address,phone,fax,email,website,taxRegisteredNumber,additionalInformation,createdAt")] MyCompany myCompany)
    {
        if (ModelState.IsValid)
        {
            _context.Add(myCompany);
            await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }
        return View(myCompany);
    }

    // GET: MyCompany/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var myCompany = await _context.MyCompany.SingleOrDefaultAsync(m => m.myCompanyId == id);
        if (myCompany == null)
        {
            return NotFound();
        }
        return View(myCompany);
    }

    // POST: MyCompany/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("myCompanyId,companyName,contactName,address,phone,fax,email,website,taxRegisteredNumber,additionalInformation,createdAt")] MyCompany myCompany)
    {
        if (id != myCompany.myCompanyId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(myCompany);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyCompanyExists(myCompany.myCompanyId))
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
        return View(myCompany);
    }

    // GET: MyCompany/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var myCompany = await _context.MyCompany
                .SingleOrDefaultAsync(m => m.myCompanyId == id);
        if (myCompany == null)
        {
            return NotFound();
        }

        return View(myCompany);
    }




    // POST: MyCompany/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var myCompany = await _context.MyCompany.SingleOrDefaultAsync(m => m.myCompanyId == id);
            _context.MyCompany.Remove(myCompany);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MyCompanyExists(string id)
    {
        return _context.MyCompany.Any(e => e.myCompanyId == id);
    }

  }
}





namespace netcore.MVC
{
  public static partial class Pages
  {
      public static class MyCompany
      {
          public const string Controller = "MyCompany";
          public const string Action = "Index";
          public const string Role = "MyCompany";
          public const string Url = "/MyCompany/Index";
          public const string Name = "MyCompany";
      }
  }
}
namespace netcore.Models
{
  public partial class ApplicationUser
  {
      [Display(Name = "MyCompany")]
      public bool MyCompanyRole { get; set; } = false;
  }
}



