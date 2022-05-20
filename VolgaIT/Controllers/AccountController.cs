 using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolgaIT.Models;
using VolgaIT.Services;

namespace VolgaIT.Controllers
{
    [AllowAnonymous]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = 0)]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (!User.Identity.IsAuthenticated) //если авторизованный пользователь пытается перейти на страницу регистрации, то его будет переадресовывать на главную страницу для пользователя
            {
                return View();
            }
            return RedirectToAction("MainPageUser", "UserActions");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerUser) //регистрация пользователя
        {
            if(ModelState.IsValid) //проверка валидности данных
            {
                IdentityUser user = await _userManager.FindByEmailAsync(registerUser.Email);
                if(user != null)
                {
                    ModelState.AddModelError("", "Данная почта уже используется");
                    return View();
                }
                user = new IdentityUser { UserName = registerUser.Email, Email = registerUser.Email};
                var result = await _userManager.CreateAsync(user, registerUser.Password); //создаем пользователя
                if (result.Succeeded) //если пользователь создался
                {
                    await _userManager.AddToRoleAsync(user, "user"); //добавляем роль
                    await _signInManager.SignInAsync(user, false); //???
                    return RedirectToAction("MainPageUser", "UserActions"); //переадресация на главную страницу для пользователя
                }
                else
                {
                    ModelState.AddModelError("", "Ошибка регистрации");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            if(!User.Identity.IsAuthenticated) //если авторизованный пользователь пытается перейти на страницу входа в аккаунт, то его будет переадресовывать на главную страницу для пользователя
            {
                return View();
            }
            return RedirectToAction("MainPageUser", "UserActions");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel signIn) //вход пользователя
        {
            if(ModelState.IsValid) //проверка валидности модели
            {
                IdentityUser user = await _userManager.FindByEmailAsync(signIn.Email); //ищем пользователя по email
                if(user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, signIn.Password, true, false); //???
                    if(result.Succeeded) //пользователь ввел верные данные
                    {
                        return RedirectToAction("MainPageUser", "UserActions", new IdentityUser { Id = user.Id, Email = user.Email}); //переадресация пользователя на главную страницу
                    }
                    else //неправильный пароль
                    {
                        ModelState.AddModelError("", "Неверная почта и/или пароль");
                    }
                }
                else //не нашел пользователя по указанному email 
                {
                    ModelState.AddModelError("", "Неверная почта и/или пароль");
                }
            }
            return View();
        }
    }
}