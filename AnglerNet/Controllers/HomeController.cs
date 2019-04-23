using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnglerNet.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace AnglerNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _env;
        public HomeController(IHostingEnvironment env)
        {
            _env = env;
        }

        private AnglerNetContext _context = new AnglerNetContext();
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public IActionResult Messages()
        {
            ViewData["Message"] = "Your Messages page.";
            Profile currentProfile = new Profile();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            currentProfile = _context.Profile.Where(o => o.UserId == userId).FirstOrDefault();
            List<Feed> userFeed = _context.Feed.Include(o => o.Sender).Where(o => o.UserId == userId).OrderBy(o => o.DateAdded).ToList();
            ViewBag.UserFeed = userFeed;
            ViewBag.Avatar = currentProfile.PictureUrl;
            return View(currentProfile);
        }


        [HttpGet]
        [Authorize(Roles = "User")]
        public JsonResult ReturnAllUsers()
        {
            List<Profile> listOfProfiles = _context.Profile.ToList();
            return Json(listOfProfiles);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public string GetCurrentUserId()
        {
            Profile currentProfile = new Profile();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public string GetUserById(int id)
        {
            if (id != 0)
            {
                Profile currentProfile = _context.Profile.Where(o => o.Id == id).FirstOrDefault();
                return currentProfile.UserId;
            }
            return "";
        }

        [HttpPost]
        public IActionResult PostFeed(string user, string feed)
        {
            Profile currentProfile = _context.Profile.Where(o => o.Id == Int32.Parse(user)).FirstOrDefault();
            var senderID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Feed newFeed = new Feed();
            newFeed.SenderId = senderID;
            newFeed.UserId = currentProfile.UserId;
            newFeed.Content = feed;
            newFeed.DateAdded = DateTime.Now;
            _context.Feed.Add(newFeed);
            _context.SaveChanges();

            currentProfile = _context.Profile.Where(o => o.UserId == newFeed.UserId).FirstOrDefault();
            List<Feed> userFeed = _context.Feed.Include(o=>o.Sender).Where(o => o.UserId == newFeed.UserId).OrderBy(o => o.DateAdded).ToList();
            ViewBag.UserFeed = userFeed;
            return PartialView("_PartialFeed", currentProfile);
        }

        [Authorize(Roles = "User")]
        public IActionResult Profile()
        {
            ViewData["Message"] = "Your profile page.";
            Profile currentProfile = new Profile();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            currentProfile = _context.Profile.Where(o=>o.UserId == userId).FirstOrDefault();
            List<Feed> userFeed = _context.Feed.Include(o => o.Sender).Where(o => o.UserId == userId).OrderBy(o => o.DateAdded).ToList();
            ViewBag.UserFeed = userFeed;
            ViewBag.Avatar = currentProfile.PictureUrl;
            ViewBag.Same = true;
            ViewBag.Friends = false;
            return View(currentProfile);
        }

        public IActionResult FriendRequest(string id)
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Profile currProfile = _context.Profile.Where(o => o.UserId == id).FirstOrDefault();
            Relationship newFriend = new Relationship();
            newFriend.UserId = currentUserId;
            newFriend.FriendId = currProfile.UserId;
            newFriend.Active = true;
            newFriend.Date = DateTime.Now;
            _context.Relationship.Add(newFriend);
            _context.SaveChanges();

            List<Feed> userFeed = _context.Feed.Include(o => o.Sender).Where(o => o.UserId == currProfile.UserId).OrderBy(o => o.DateAdded).ToList();
            ViewBag.UserFeed = userFeed;
            ViewBag.Avatar = currProfile.PictureUrl;
            ViewBag.Same = false;
            ViewBag.Friends = true;

            return Redirect("/Home/Profile/" + currProfile.Id);
        }

        public IActionResult RemoveFriend(string id)
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Profile currProfile = _context.Profile.Where(o => o.UserId == id).FirstOrDefault();
            Relationship currRelation = _context.Relationship.Where(o => o.UserId == currentUserId && o.FriendId == currProfile.UserId).FirstOrDefault();
            _context.Relationship.Remove(currRelation);
            _context.SaveChanges();

            List<Feed> userFeed = _context.Feed.Include(o => o.Sender).Where(o => o.UserId == currProfile.UserId).OrderBy(o => o.DateAdded).ToList();
            ViewBag.UserFeed = userFeed;
            ViewBag.Avatar = currProfile.PictureUrl;
            ViewBag.Same = false;
            ViewBag.Friends = false;

            return Redirect("/Home/Profile/" + currProfile.Id);
        }

        [Authorize(Roles = "User")]
        [Route("Home/Profile/{id}")]
        public IActionResult Profile(int id)
        {
            ViewData["Message"] = "Your profile page.";
            Profile currentProfile = _context.Profile.Where(o => o.Id == id).FirstOrDefault();
            List<Feed> userFeed = _context.Feed.Include(o => o.Sender).Where(o => o.UserId == currentProfile.UserId).OrderBy(o=>o.DateAdded).ToList();
            ViewBag.UserFeed = userFeed;
            ViewBag.Avatar = currentProfile.PictureUrl;

            ViewBag.Same = false;
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Profile logedUser = _context.Profile.Where(o => o.UserId == userId).FirstOrDefault();
            if (logedUser.UserId == currentProfile.UserId)
            {
                ViewBag.Same = true;
            }

            ViewBag.Friends = false;
            Relationship friends = _context.Relationship.Where(o => o.UserId == logedUser.UserId && o.FriendId == currentProfile.UserId).FirstOrDefault();
            if (friends != null)
            {
                ViewBag.Friends = true;
            }
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

        [HttpPost]
        public IActionResult UploadImage(IFormFile picture)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Profile currentProfile = _context.Profile.Where(o => o.UserId == userId).FirstOrDefault();

            if (picture != null)
            {
                DirectoryInfo di = Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "images", "avatars", currentProfile.Id.ToString()));
                foreach (FileInfo file in di.GetFiles())
                {
                    try{
                      file.Delete();
                    }
                    catch (IOException e)
                    {
                        //Decide what to do with the exception?
                    }

                }
                var filename = Path.Combine(_env.WebRootPath, "images", "avatars", currentProfile.Id.ToString(), Path.GetFileNameWithoutExtension(picture.FileName) + DateTime.Now.ToString("yyyyddMHHmmss")+Path.GetExtension(picture.FileName));
                picture.CopyTo(new FileStream(filename, FileMode.Create));
                currentProfile.PictureUrl = "/images/avatars/"+ currentProfile.Id.ToString()+ "/" + Path.GetFileName(filename);
                _context.SaveChanges();
            }

            return RedirectToAction("Profile", "Home");
        }

    }
}
