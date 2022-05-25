using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolgaIT.Models;
using VolgaIT.Services.Interface;

namespace VolgaIT.Controllers
{
    [AllowAnonymous]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = 0)]
    public class PasswordController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IPasswordService _passwordService;

        public PasswordController(IPasswordService passwordService, IEmailService emailService)
        {
            _passwordService = passwordService;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _passwordService.GenerateTokenPasswordResetAsync(model);
                if(result == null)
                {
                    return RedirectToAction("SignIn", "Account");
                }
                var callbackUrl = Url.Action("NewPassword", "Password", new { Email = model.Email, Token = result, lifeTime = DateTime.Now.AddMinutes(20) }, protocol: HttpContext.Request.Scheme);
                await _emailService.SendEmailAsync(model.Email, "Восстановление Пароля", $"Для того чтобы создать новый пароль перейдите по <a href='{callbackUrl}'>ссылке</a>");
                return RedirectToAction("SignIn", "Account");
            }
            return View();
        }

        [HttpGet]
        public IActionResult NewPassword(ResetPasswordViewModel model, DateTime lifeTime)
        {
            if (lifeTime >= DateTime.Now)
            {
                return View(model);
            }
            return RedirectToAction("SignIn", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _passwordService.ResetPasswordAsync(model);
                return RedirectToAction("SignIn", "Account");

            }
            return View("NewPassword", model);
        }
    }
}
