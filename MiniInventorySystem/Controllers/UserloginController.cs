using Microsoft.AspNetCore.Mvc;
using MiniInventorySystem.Models;

namespace MiniInventorySystem.Controllers
{
    public class UserloginController : Controller
    {
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Login(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

           
            if (model.Username == "admin" && model.Password == "1234")
            {
                TempData["SuccessMessage"] = "Login successful!";
                return RedirectToAction("Index", "Product");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return RedirectToAction("Login", "Userlogin");
        }

        
        public IActionResult Index()
        {
            return View();
        }
    }
}
