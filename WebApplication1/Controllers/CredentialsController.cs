using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace WebApplication1.Controllers
{
    public class CredentialsController : Controller
    {
        public static string passUser = "";
        public static string userIcon = "";
        private readonly ApplicationDbContext _context;
        public CredentialsController(ApplicationDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            var credentials = _context.Credentials.ToList();
            return View(credentials);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public IActionResult Index(string user, string password)
        {
            var existingUser = _context.Credentials.FirstOrDefault(x => x.UserName == user);

            if (existingUser == null)
            {
                TempData["UserNull"] = "Username Not Found";
                return View();
            }

            if (existingUser.Password != password)
            {
                TempData["PasswordIncorrect"] = "Incorrect Password";
                return View();
            }

            var icon = existingUser.ProfileIcon;
            userIcon = icon.ToString();
            passUser = user;
            TempData["SuccessMessage"] = "Successfully Logged In As: " + user;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(Credentials addRequestCredentials)
        {
            var existingUser = _context.Credentials.FirstOrDefault(x => x.UserName == addRequestCredentials.UserName);

            if (existingUser != null)
            {
                TempData["UserExists"] = "Username Already Exists";
                return View();
            }

            var userWithExistingPassword = _context.Credentials.FirstOrDefault(x => x.Password == addRequestCredentials.Password);
            if (userWithExistingPassword != null)
            {
                TempData["PasswordExists"] = $"Password Already Used By {userWithExistingPassword.UserName}";
                return View();
            }

            if (addRequestCredentials.ProfileIcon == null)
            {
                TempData["NoIcon"] = "Select an Icon!";
                return View();
            }

            SaveData(addRequestCredentials);
            TempData["SuccessMessage"] = "Successfully Registered Please Try Logging In";
            return View("Index");
        }

        private void SaveData(Credentials addRequestCredentials)
        {
            var credentials = new Credentials()
            {
                UserName = addRequestCredentials.UserName,
                Password = addRequestCredentials.Password,
                ProfileIcon = addRequestCredentials.ProfileIcon
            };

            _context.Credentials.Add(credentials);
            _context.SaveChanges();
        }
    }
}
