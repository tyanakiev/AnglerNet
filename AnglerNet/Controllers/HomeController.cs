using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnglerNet.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AnglerNet.Controllers
{
    public class HomeController : Controller
    {
        AnglerNetContext _context = new AnglerNetContext();
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public IActionResult Messages()
        {
            ViewData["Message"] = "Your Messages page.";

            return View();
        }

        [Authorize(Roles = "User")]
        public IActionResult Profile()
        {
            ViewData["Message"] = "Your profile page.";
            Profile currentProfile = new Profile();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            currentProfile = _context.Profile.Where(o=>o.UserId == userId).FirstOrDefault();
            return View(currentProfile);
        }

        [Authorize(Roles = "User")]
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
