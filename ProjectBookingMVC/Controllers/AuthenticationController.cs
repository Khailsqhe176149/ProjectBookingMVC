using Microsoft.AspNetCore.Mvc;
using ProjectBookingMVC.Repository.RepUser;
using ProjectBookingMVC.Models;

namespace ProjectBookingMVC.Controllers
{
    public class AuthenticationController : Controller
    {
        IUserRepository userRepository = null;
        public AuthenticationController()=>userRepository = new UserRepository();
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user, string ConfirmPassword)
        {
            if(user.Password != ConfirmPassword)
            {
                ViewBag.Msg = "Password is not match";
                return View();
            }
            else if(user.Password.Length < 8)
            {
                ViewBag.Msg = "Password must > 8 character";
                return View();
            }
            ViewBag.Msg = "Register successfully";
            userRepository.Register(user);
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user)
        {
            Console.WriteLine(user.Username);
            Console.WriteLine(user.Password);
            User userLogined = userRepository.Login(user);

            if (userLogined != null)
            {
                if(userLogined.Role == "ADMIN")
                {
                    HttpContext.Session.SetString("Logined", userLogined.Id.ToString());
                    HttpContext.Session.SetString("Roll", userLogined.Role);
                    return RedirectToAction("Index", "Admin");
                }
                HttpContext.Session.SetString("Logined", userLogined.Id.ToString());
                HttpContext.Session.SetString("Roll", userLogined.Role);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Msg = "Login failed";
                return View();
            }
        }
    }
}
