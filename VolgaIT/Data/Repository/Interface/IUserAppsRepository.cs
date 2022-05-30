using VolgaIT.Models;

namespace VolgaIT.Data.Repository.Interface
{
    public interface IUserAppsRepository
    {
        //Task<List<UserApps>> GetAllUserApps(string idUser);
        Task AddUserAppAsync(UserApp userApp);
        Task SaveAsync();
    }
}
