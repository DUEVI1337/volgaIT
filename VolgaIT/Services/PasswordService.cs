using Microsoft.AspNetCore.Identity;
using VolgaIT.Models;
using VolgaIT.Services.Interface;

namespace VolgaIT.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly UserManager<User> _userManager;

        public PasswordService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GenerateTokenPasswordResetAsync(ForgotPasswordViewModel model)
        {
            User user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                return code;
            }
            return null;
        }

        public async Task ResetPasswordAsync(ResetPasswordViewModel model)
        {
            User user = await _userManager.FindByEmailAsync(model.Email);
            if(user!=null)
            {
                await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            }
        }

    }
}
