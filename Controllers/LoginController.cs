using InventorySystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly InventoryContext _context;

        public LoginController(InventoryContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            var data = _context.Accounts.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();
            if (data != null)
            {
                HttpContext.Session.SetString("UserID", data.ID.ToString());
                HttpContext.Session.SetString("RoleID", data.RoleID.ToString());
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.error = "Invalid username or Password";
                return View("Login");
            }

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
       
    }
}
