using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FM2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace FM2.Controllers
{
    public class HomeController : Controller
    {
        public User LoggedInUser()
        {
            int? LoggedID = HttpContext.Session.GetInt32("UserId");
            User logged = _context.Users.FirstOrDefault(u => u.UserId == LoggedID);
            return logged;
        }
        public int UserID()
        {
            int UserID = LoggedInUser().UserId;
            return UserID;
        }
        private MyContext _context;

        public HomeController(MyContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Register");
        }
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("Register")]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(user => user.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email is already in use!");
                    return View("Register");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                Console.WriteLine(newUser.Password);
                _context.Add(newUser);
                _context.SaveChanges();
                Console.WriteLine(newUser.UserId);
                HttpContext.Session.SetInt32("UserId", newUser.UserId);
                return RedirectToAction("Success");
            }
            return View("Register");
        }
        [HttpGet("success")]
        public IActionResult Success()
        {
            if (LoggedInUser() == null)
            {
                return RedirectToAction("Register");
            }
            ViewBag.loggedUser = LoggedInUser();
            return View();
        }
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost("login")]
        public IActionResult LoginUser(LoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                var dbuser = _context.Users.FirstOrDefault(user => user.Email == loginUser.LoginEmail);
                if (dbuser == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid email");
                    return View("Login");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(loginUser, dbuser.Password, loginUser.LoginPassword);
                if (result == 0)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid email");
                    return View("Login");
                }
                HttpContext.Session.SetInt32("UserId", dbuser.UserId);
                return RedirectToAction("Success");
            }
            return View("Login");
        }
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
