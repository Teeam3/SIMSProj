    using democode.Models;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    namespace democode.Controllers
    {
        public class UserController : Controller
        {
            private readonly IUserService _userService;
        private static int _studentCount = 0;

        public UserController(IUserService userService)
            {
                _userService = userService;
            }

            [HttpGet]
            public IActionResult Index()
            {
                return View();
            }

            [HttpGet]
            public IActionResult Register()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Register(User model)
            {
                if (model.Email == null || _userService.IsEmailTaken(model.Email))
                {
                    ModelState.AddModelError("Email", "That email is already in use");
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                UserRole role;
                if (model.Id == "a")
                {
                    role = UserRole.Admin;
                }
                else if (model.Id == "t")
                {
                    role = UserRole.Teacher;
                }
                else
                {
                    role = UserRole.Student;
                model.Id = (++_studentCount).ToString();
            }

                var user = _userService.Register(model.Id ?? string.Empty, model.Name ?? string.Empty, model.Email ?? string.Empty, model.Password ?? string.Empty, role);

                if (user == null)
                {
                    ModelState.AddModelError("", "There was an error registering the user");
                    return View(model);
                }
            //Save user details ub session
            HttpContext.Session.SetString("UserId", user.Id);
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));

            return RedirectToAction("Index", "Dashboard"); // Changed from "Index" to "Index", "Dashboard"
            }

            [HttpPost]
            public IActionResult Login(User model)
            {
                if (!ModelState.IsValid)
                {
                    return View("Index", model);
                }

                var user = _userService.GetUser(model.Email, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("Email", "Invalid email or password.");
                    return View("Index", model);
                }

            // Save user details in session
            HttpContext.Session.SetString("UserId", user.Id);
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));

            return RedirectToAction("Index", "Dashboard");
            }
            public IActionResult Logout()
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "User");
            }
            [HttpGet]
            public IActionResult Update(string id)
            {
                var users = _userService.GetById(id);
                return View(users);
            }
            // Other actions...
            [HttpPost]
            public IActionResult Update(string id, string name, string email)
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                if(!email.Contains("@"))
                {
                    ModelState.AddModelError("Email", "That email is already in use");
                }
                var newUser = _userService.Update(id, name,email);
                return View(newUser);
            }
            [HttpGet]
            public IActionResult GetById()
            {
                return View();
            }
            [HttpGet]
            public IActionResult Delete(string id)
            {
                _userService.DeleteUser(id);
                return Redirect("/Dashboard");
            }
        }
    }
