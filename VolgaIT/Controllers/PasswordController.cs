using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolgaIT.Models;
using VolgaIT.Services;

namespace VolgaIT.Controllers
{
    [AllowAnonymous]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = 0)]
    public class PasswordController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public PasswordController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model) //отправка письма на почту, для восстановления пароля
        {
            if (ModelState.IsValid) //проверка модели
            {
                IdentityUser user = await _userManager.FindByEmailAsync(model.Email); //ищем пользователя по указанному email 
                if (user != null) 
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user); //генерация токена сброса пароля для найденного пользователя
                    DateTime lifeTime = DateTime.Now.AddMinutes(20); //время жизни ссылки
                    var callbackUrl = Url.Action("NewPassword", "Password", new { Email = user.Email, Token = code, lifeTime = lifeTime }, protocol: HttpContext.Request.Scheme); //генерация ссылки
                    EmailServices emailServices = new EmailServices();
                    await emailServices.SendEmailAsync(user.Email, "Восстановление Пароля", $"Для того чтобы создать новый пароль перейдите по <a href='{callbackUrl}'>ссылке</a>"); //отправка письма на email
                }
                return RedirectToAction("SignIn", "Account");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> NewPassword(ResetPasswordViewModel model, DateTime lifeTime)
        {
            if (lifeTime >= DateTime.Now) //проверяем время жизни ссылки
            {
                return View(model);
            }
            return RedirectToAction("SignIn", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model) //создание нового пользователя
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByEmailAsync(model.Email); //ищем пользователя по email 
                await _userManager.ResetPasswordAsync(user, model.Token, model.Password); //сбрасываеи и задаем новый пароль для пользователя
                return RedirectToAction("SignIn", "Account");

            }
            return View("NewPassword", model);
        }
    }
}
