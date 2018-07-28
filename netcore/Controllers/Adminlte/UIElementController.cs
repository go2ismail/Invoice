using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace lte.Controllers
{
    public partial class AdminlteController : Controller
    {
        public IActionResult General()
        {
            return View();
        }

        public IActionResult Icon()
        {
            return View();
        }

        public IActionResult Button()
        {
            return View();
        }

        public IActionResult Slider()
        {
            return View();
        }

        public IActionResult Timeline()
        {
            return View();
        }

        public IActionResult Modal()
        {
            return View();
        }
    }
}