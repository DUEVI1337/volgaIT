using VolgaIT.Data.Repository.Interface;
using VolgaIT.Models;
using VolgaIT.Models.ViewModels;
using VolgaIT.Services.Interface;

namespace VolgaIT.Services
{
    public class AppService : IAppService
    {
        private readonly IAppRepository _repoApp;
        public AppService(IAppRepository repoApp)
        {
            _repoApp = repoApp;
        }

        public async Task<List<App>> GetAllAppsAsync()
        {
            return await _repoApp.GetAllAppAsync();
        }

        public async Task<List<App>> GetAllUserApp(List<string> appsId)
        {
            var userApps = await _repoApp.GetAllUserApp(appsId);
            return userApps;
        }

        public async Task AddAppAsync(AddAppViewModel model)
        {
            var app = new App() {Id = model.AppId, Name = model.AppName };
            await _repoApp.AddAppAsync(app);
            await _repoApp.SaveAsync();
        }
    }
}
