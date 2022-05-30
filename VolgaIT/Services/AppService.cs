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

        public async Task<App> GetAppByIdAsync(string idApp)
        {
            return await _repoApp.GetAppByIdAsync(idApp);
        }

        public async Task<List<App>> GetAllAppsAsync()
        {
            return await _repoApp.GetAllAppAsync();
        }

        public async Task<List<App>> GetAppRangeAsync(List<string> appsId)
        {
            var userApps = await _repoApp.GetAppRangeAsync(appsId);
            return userApps;
        }

        public async Task AddAppAsync(AddAppViewModel model)
        {
            var app = new App() {Id = model.AppId, Name = model.AppName };
            await _repoApp.AddAppAsync(app);
            await _repoApp.SaveAsync();
        }

        public async Task UpdateAppAsync(UpdateAppViewModel model)
        {
            App appUpdate = await _repoApp.GetAppByIdAsync(model.AppId);
            appUpdate.Name = model.AppName;
            _repoApp.UpdateApp(appUpdate);
            await _repoApp.SaveAsync();
        }

        public async Task RemoveAppByIdAsync(string appId)
        {
            _repoApp.RemoveApp(await _repoApp.GetAppByIdAsync(appId));
            await _repoApp.SaveAsync();
        }

        public StatisticsAppViewModel TimeFilterRequestApp(string timeFilter, StatisticsAppViewModel model)
        {
            switch (timeFilter)
            {
                case "week":
                    DateTime rqstUserWeek = DateTime.Now.AddDays(-7);
                    model.RequestUsers = model.RequestUsers.Where(x => x.CreatedDate > rqstUserWeek).ToList();
                    break;

                case "month":
                    DateTime rqstUserMonth = DateTime.Now.AddDays(-31);
                    model.RequestUsers = model.RequestUsers.Where(x => x.CreatedDate > rqstUserMonth).ToList();
                    break;

                case "year":
                    DateTime rqstUserYear = DateTime.Now.AddDays(-365);
                    model.RequestUsers = model.RequestUsers.Where(x => x.CreatedDate > rqstUserYear).ToList();
                    break;
            }
            return model;
        }
    }
}
