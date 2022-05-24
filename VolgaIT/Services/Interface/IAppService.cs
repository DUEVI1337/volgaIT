using VolgaIT.Models;
using VolgaIT.Models.ViewModels;

namespace VolgaIT.Services.Interface
{
    public interface IAppService
    {
        Task<List<App>> GetAllAppsAsync();
        Task AddAppAsync(AddAppViewModel model);
    }
}
