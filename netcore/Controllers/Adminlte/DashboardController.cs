using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace lte.Controllers
{
    public partial class AdminlteController : Controller
    {
        public IActionResult Dashboardv1()
        {
            return View();
        }

        public IActionResult Dashboardv2()
        {
            return View();
        }
    }
}