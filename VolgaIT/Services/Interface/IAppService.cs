using VolgaIT.Models;
using VolgaIT.Models.ViewModels;

namespace VolgaIT.Services.Interface
{
    public interface IAppService
    {
        Task<List<App>> GetAllAppsAsync();
        Task<List<App>> GetAppRangeAsync(List<string> appsId);
        Task<App> GetAppByIdAsync(string idApp);
        Task UpdateAppAsync(UpdateAppViewModel model);
        StatisticsAppViewModel TimeFilterRequestApp(string timeFilter, StatisticsAppViewModel model);
        Task AddAppAsync(AddAppViewModel model);
        Task RemoveAppByIdAsync(string idApp);
    }
}
