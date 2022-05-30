using VolgaIT.Models;

namespace VolgaIT.Services.Interface
{
    public interface IAccountService
    {
        Task<bool> RegisterUserAsync(RegisterViewModel model);
        Task<bool> SignInAsync(SignInViewModel model);
        Task LogoutAsync();
    }
}
