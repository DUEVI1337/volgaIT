using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VolgaIT.Models;
using VolgaIT.Models.ViewModels;

namespace VolgaIT.Services.Interface
{
    public interface IUserService
    {
        Task<User> GetUserAsync();
        Task<bool> EditProfileAsync(UpdateProfileViewModel model);
    }
}
