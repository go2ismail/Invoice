using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace lte.Controllers
{
    public partial class AdminlteController : Controller
    {
        public IActionResult Invoice()
        {
            return View();
        }

        public IActionResult InvoicePrint()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Lockscreen()
        {
            return View();
        }

        public IActionResult Error404()
        {
            return View();
        }

        public IActionResult Error500()
        {
            return View();
        }

        public IActionResult BlankPage()
        {
            return View();
        }

        public IActionResult PacePage()
        {
            return View();
        }
    }
}