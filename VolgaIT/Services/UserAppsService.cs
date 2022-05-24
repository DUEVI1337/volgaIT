using VolgaIT.Data.Repository.Interface;
using VolgaIT.Models;
using VolgaIT.Services.Interface;

namespace VolgaIT.Services
{
    public class UserAppsService : IUserAppsService
    {
        private readonly IUserAppsRepository _repoUserApps;

        public UserAppsService(IUserAppsRepository repoUserApps)
        {
            _repoUserApps = repoUserApps;
        }

        public async Task AddUserAppAsync(string appId, string userId)
        {
            var userApp = new UserApp { AppsId = appId, UsersId = userId };
            await _repoUserApps.AddUserAppAsync(userApp);
            await _repoUserApps.SaveAsync();

        }
    }
}
