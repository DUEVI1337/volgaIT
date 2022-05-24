using Microsoft.AspNetCore.Identity;
using VolgaIT.Data.Repository.Interface;
using VolgaIT.Models;
using VolgaIT.Services.Interface;

namespace VolgaIT.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repoUser;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IUserRepository repoUser, UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContext)
        {
            _repoUser = repoUser;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContext = httpContext;
        }

        public async Task<User> GetUserAsync()
        {
            string emailUser = _httpContext.HttpContext.User.Identity.Name;
            User user = await _repoUser.GetUserByEmailAsync(emailUser);
            return user;
        }

        public async Task<bool> EditProfileAsync(User user)
        {
            User userUpdate = await _userManager.FindByIdAsync(user.Id);
            userUpdate.Email = user.Email;
            userUpdate.UserName = user.Email;
            var result = await _userManager.UpdateAsync(userUpdate);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(userUpdate);
                return true;
            }
            return false;
        }
    }
}

