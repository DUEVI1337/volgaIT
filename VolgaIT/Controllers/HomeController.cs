using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VolgaIT.Models;

namespace VolgaIT.Controllers
{
    [AllowAnonymous]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = 0)]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()      
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("MainPageUser", "UserActions");
        }
    }
}
