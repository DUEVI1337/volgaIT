using VolgaIT.Models;

namespace VolgaIT.Data.Repository.Interface
{
    public interface IAppRepository
    {
        Task<List<App>> GetAllAppAsync();
        Task<List<App>> GetAppRangeAsync(List<string> appsId);
        Task<App> GetAppByIdAsync(string appId);
        Task AddAppAsync(App app);
        void UpdateApp(App app);
        void RemoveApp(App app);
        Task SaveAsync();
    }
}
