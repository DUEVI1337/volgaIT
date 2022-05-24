using VolgaIT.Models;
using VolgaIT.Models.ViewModels;

namespace VolgaIT.Services.Interface
{
    public interface IAppService
    {
        Task<List<App>> GetAllAppsAsync();
        Task<List<App>> GetAllUserApp(string userId);
        Task AddAppAsync(AddAppViewModel model);
    }
}
