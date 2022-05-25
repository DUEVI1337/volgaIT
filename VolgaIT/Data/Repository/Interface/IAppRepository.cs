using VolgaIT.Models;

namespace VolgaIT.Data.Repository.Interface
{
    public interface IAppRepository
    {
        Task<List<App>> GetAllAppAsync();
        Task<List<App>> GetAllUserApp(List<string> appsId);
        Task AddAppAsync(App app);
        Task SaveAsync();
    }
}
