using VolgaIT.Models;

namespace VolgaIT.Services.Interface
{
    public interface IUserAppsService
    {
        Task AddUserAppAsync(string appId, string userId);
    }
}
