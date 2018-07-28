using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using netcore.Data;
using netcore.Models;
using netcore.Services;

namespace netcore.Controllers
{
    [Authorize(Roles = netcore.MVC.Pages.ApplicationUser.Role)]
    public class ApplicationUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INetcoreService _netCoreService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public ApplicationUserController(ApplicationDbContext context,
            INetcoreService netCoreService,
            UserManager<ApplicationUser> userManager,
            ILogger<ApplicationUserController> logger)
        {
            _context = context;
            _netCoreService = netCoreService;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: ApplicationUser
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation(LoggingEvents.ListItems, "List ApplicationUser");
            return View(await _context.ApplicationUser.ToListAsync());
        }

        // GET: ApplicationUser/Details/5
        public async Task<IActionResult> Details(string id)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Getting ApplicationUser: {id}", id);

            if (id == null)
            {
                _logger.LogInformation(LoggingEvents.GetItemNotFound, "Getting ApplicationUser: null");

                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                _logger.LogInformation(LoggingEvents.GetItemNotFound, "Getting ApplicationUser: {id}", id);

                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: ApplicationUser/Create
        public IActionResult Create()
        {
            _logger.LogInformation(LoggingEvents.InsertItem, "Get Create ApplicationUser");

            return View();
        }

        // POST: ApplicationUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ApplicationUser applicationUser)
        {
            _logger.LogInformation(LoggingEvents.InsertItem, "Post Create ApplicationUser");

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(applicationUser);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.InsertItem, "Post Create ApplicationUser: {Error}", ex.Message);

                throw;
            }
           
            return View(applicationUser);
        }

        // GET: ApplicationUser/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Getting ApplicationUser: {id}", id);

            if (id == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "Getting ApplicationUser: null");

                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "Getting ApplicationUser: {id}", id);

                return NotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [FromForm] ApplicationUser applicationUser)
        {
            _logger.LogInformation(LoggingEvents.UpdateItem, "Update ApplicationUser: {id}", id);

            if (id != applicationUser.Id)
            {
                _logger.LogWarning(LoggingEvents.UpdateItemNotFound, "Update ApplicationUser: {id}", id);

                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //super admin should always have access to Roles
                    applicationUser.ApplicationUserRole = applicationUser.isSuperAdmin ? true : applicationUser.ApplicationUserRole;

                    _context.Update(applicationUser);
                    await _context.SaveChangesAsync();

                    ApplicationUser currentUserLogin = await _userManager.GetUserAsync(User);

                    await _netCoreService.UpdateRoles(applicationUser, currentUserLogin);
                }
                catch (Exception ex)
                {
                    _logger.LogError(LoggingEvents.UpdateItem, "Update ApplicationUser: {id}. Error: {Error}", id, ex.Message);

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(applicationUser);
        }

        // GET: ApplicationUser/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation(LoggingEvents.DeleteItem, "Get Delete ApplicationUser: {id}", id);

            if (id == null)
            {
                _logger.LogWarning(LoggingEvents.DeleteItemNotFound, "Get Delete ApplicationUser: null");

                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                _logger.LogWarning(LoggingEvents.DeleteItemNotFound, "Get Delete ApplicationUser: {id}", id);

                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: ApplicationUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            _logger.LogInformation(LoggingEvents.DeleteItem, "Post Delete ApplicationUser: {id}", id);

            try
            {
                var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
                _context.ApplicationUser.Remove(applicationUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.DeleteItem, "Post Delete ApplicationUser: {id}. Error: {Error}", id, ex.Message);

                throw;
            }
            
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}
