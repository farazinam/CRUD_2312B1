using CRUD_2312B1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_2312B1.Controllers
{
    public class AuthController : Controller
    {
        //LabsevenContext abc = new LabsevenContext();

        private readonly LabsevenContext dbase;

        public AuthController(LabsevenContext _dbase)
        {
            dbase = _dbase;
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            var CheckExistingUser = dbase.Users.FirstOrDefault(t => t.Email == user.Email);
            if (CheckExistingUser != null)
            {
                ViewBag.msg = "User Already Exists";
                return View();
            }

            var hasher = new PasswordHasher<string>();  //Convert password in hash
            user.Password = hasher.HashPassword(user.Email, user.Password);
            dbase.Users.Add(user);
            dbase.SaveChanges();
            return RedirectToAction("SignIn");
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

    }
}
