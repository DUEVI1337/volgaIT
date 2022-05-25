using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolgaIT.Models;
using VolgaIT.Services.Interface;

namespace VolgaIT.Controllers
{
    [AllowAnonymous]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = 0)]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("MainPageUser", "UserActions");
        }

        [HttpPost]
        [Route("RegisterAsync")]
        public async Task<IActionResult> Register(RegisterViewModel registerUser)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountService.RegisterUserAsync(registerUser);
                if(result)
                {
                    return RedirectToAction("MainPageUser", "UserActions");
                }
                else
                {
                    ModelState.AddModelError("", "Ошибка регистрации, возможно данная почта уже используется");
                }
            }
            return View("Register");
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            if(!User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("MainPageUser", "UserActions");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountService.SignInAsync(model);
                if(result)
                {
                    return RedirectToAction("MainPageUser", "UserActions", new {  Email = model.Email });
                }
                ModelState.AddModelError("", "Неверная почта и/или пароль");
            }
            return View("SignIn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}