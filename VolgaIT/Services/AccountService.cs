using Microsoft.AspNetCore.Identity;
using VolgaIT.Models;
using VolgaIT.Services.Interface;

namespace VolgaIT.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> RegisterUserAsync(RegisterViewModel model)
        {
            User user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                return false;
            }
            user = new User { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return false;
            }
            await _userManager.AddToRoleAsync(user, "user");
            await _signInManager.SignInAsync(user, false);
            return true;
        }

        public async Task<bool> SignInAsync(SignInViewModel model)
        {
            User user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return false;
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
