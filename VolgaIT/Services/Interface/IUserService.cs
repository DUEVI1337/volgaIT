using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VolgaIT.Models;

namespace VolgaIT.Services.Interface
{
    public interface IUserService
    {
        Task<User> GetUserAsync();
        Task<bool> EditProfileAsync(User user);
    }
}
