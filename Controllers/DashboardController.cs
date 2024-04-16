using Microsoft.AspNetCore.Mvc;
using democode.Models; // Assuming IUserService is in this namespace
using Newtonsoft.Json;

namespace democode.Controllers
{
    public class DashboardController : Controller
    {
        private IUserService _userService;

        public DashboardController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var userJson = HttpContext.Session.GetString("User");
            var user = userJson != null ? JsonConvert.DeserializeObject<User>(userJson) : null;

            if (user == null)
            {
                return RedirectToAction("Index", "User");
            }

            ViewBag.User = user;

            return View();
        }

        public IActionResult Course()
        {
            return View();
        }

        public IActionResult Teacher()
        {
            return View();
        }

        public IActionResult Student()
        {
            return View();
        }
        public IActionResult Information(string id)
        {
            var users = _userService.GetById(id);
            return View(users);
        }
        public IActionResult Account()
        {
            return View();
        }

        public IActionResult Login(User user)
        {
            var validUser = _userService.GetUser(user.Email, user.Password);
            if (validUser != null)
            {
                HttpContext.Session.SetString("UserId", validUser.Id.ToString());
                HttpContext.Session.SetString("UserName", validUser.Name);
                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(validUser)); // Add this line
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.Error = "Invalid Account";
                return View("Index");
            }
        }
    }
}
