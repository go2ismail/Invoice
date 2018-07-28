using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace lte.Controllers
{
    public partial class AdminlteController : Controller
    {
        public IActionResult Top()
        {
            return View();
        }

        public IActionResult Boxed()
        {
            return View();
        }

        public IActionResult Fixed()
        {
            return View();
        }

        public IActionResult Collapsed()
        {
            return View();
        }
    }
}