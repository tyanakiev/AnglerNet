using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnglerNet.Models;

namespace AnglerNet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Messages()
        {
            ViewData["Message"] = "Your Messages page.";

            return View();
        }

        public IActionResult Profile()
        {
            ViewData["Message"] = "Your profile page.";

            return View();
        }

        public IActionResult Maps()
        {
            ViewData["Message"] = "Your Maps page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
