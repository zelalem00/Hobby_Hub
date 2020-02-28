using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hobby_Hub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;


namespace Hobby_Hub.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
         //register (post)
        [HttpPost("Register")]
        public IActionResult Register(User newuser)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.users.Any(u => u.UserName == newuser.UserName))
                {
                    ModelState.AddModelError("UserName", "UserName alredy in use!");
                return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newuser.Password = Hasher.HashPassword(newuser, newuser.Password);
                dbContext.Add(newuser);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("Loginuser", newuser.UserId);
                return RedirectToAction("Dashboard");
            }
            return View("Index");
        }
        //login(post)

        [HttpPost("Login")]
        public IActionResult Login(Luser Loginuser)
        {
            if(ModelState.IsValid)
            {
                User userInDb = dbContext.users.FirstOrDefault(u => u.Email == Loginuser.LEmail);
          
                 if(userInDb == null)
                {
                ModelState.AddModelError("Email", "Invalid Email/Password");
                return View("Index");

                }
            var hasher = new PasswordHasher<Luser>();
            var result = hasher.VerifyHashedPassword(Loginuser, userInDb.Password, Loginuser.LPassword);
            
            if(result == 0)
            {
                ModelState.AddModelError("LEmail", "invalid email/password");
                return View("Index");
            }
            HttpContext.Session.SetInt32("Loginuser", userInDb.UserId);
            return RedirectToAction("Dashboard");
        }
        return View("Index");
        }
        
        //Dashboard(get)
        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            int? session = HttpContext.Session.GetInt32("Loginuser");
            if(session == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.LoggedIn = dbContext.users.FirstOrDefault(a => a.UserId == (int)session);
            List<Hobby> hobbies = dbContext.Hobbies.Include(b => b.HobbyPoster).Include(s => s.Guests).OrderByDescending(l=> l.Guests.Count).ToList();
            return View(hobbies);
        }
        [HttpGet("Hobby")]
        public IActionResult Hobby()
        {
            int? session = HttpContext.Session.GetInt32("Loginuser");
            if(session == null)
            {
                return RedirectToAction("Index");
            }
            
            return View();
        }
    //    HobbyPoster
        //Create Activity(post)
        [HttpPost("CreateHobby")]
        public IActionResult CreateHobby(Hobby newhobby)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Hobbies.Any(u => u.Title == newhobby.Title))
                {
                    ModelState.AddModelError("Title", "Title alredy in use!");
                return View("Hobby");
                } 
            }
            int? session = HttpContext.Session.GetInt32("Loginuser");
            if(ModelState.IsValid)
        {   newhobby.UserId = (int)session;
            dbContext.Add(newhobby);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        return View("Hobby");
        }

        [HttpGet("detail/{HobbyId}")]
        public IActionResult detail(int HobbyId)
        {
        ;
        int? session = HttpContext.Session.GetInt32("Loginuser");
        if(session == null)
        {
            return RedirectToAction("Index");
        }
        ViewBag.LoggedIn = dbContext.users.FirstOrDefault(a => a.UserId == (int)session);
        Hobby one = dbContext.Hobbies.Include(b => b.HobbyPoster).Include(s => s.Guests).ThenInclude(v => v.Creator).FirstOrDefault(b => b.HobbyId == HobbyId);
        return View(one);

        }
            //logout
        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        [HttpGet("join/{HobbyId}")]
        public IActionResult Join(int HobbyId)
        {
            int? session = HttpContext.Session.GetInt32("Loginuser");
            Eventss newEvent = new Eventss();
            newEvent.UserId = (int)session;
            newEvent.HobbyId = HobbyId;
            dbContext.Add(newEvent);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet("unjoin/{HobbyId}")]
        public IActionResult Unjoin(int HobbyId)
        {
            int? session = HttpContext.Session.GetInt32("Loginuser");
            Eventss oneEvent = dbContext.Eventsses.Where(d => d.HobbyId == HobbyId).SingleOrDefault(f => f.UserId == (int)session);
            dbContext.Remove(oneEvent);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet("edit/{HobbyId}")]
        public IActionResult edit(int HobbyId)
        {
            Hobby RetrieveHobby = dbContext.Hobbies.FirstOrDefault(x=> x.HobbyId == HobbyId);
            return View(RetrieveHobby);
        }

        [HttpPost("update/{HobbyId}")]
        public IActionResult update(Hobby updateHobby, int HobbyId)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Hobbies.Any(u => u.Title == updateHobby.Title))
                {
                    ModelState.AddModelError("Title", "Title alredy in use!");
                return View("Hobby");
                } 
                Hobby hobby = dbContext.Hobbies.FirstOrDefault(x=> x.HobbyId == HobbyId);
                hobby.Title = updateHobby.Title;
                hobby.Description = updateHobby.Description;
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View("Hobby");
            
        }


        [HttpGet("delete/{HobbyId}")]
        public IActionResult Delete(int HobbyId)
        {
            Hobby oneAct = dbContext.Hobbies.SingleOrDefault(a => a.HobbyId == HobbyId);
            dbContext.Remove(oneAct);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        

      
    }
}
