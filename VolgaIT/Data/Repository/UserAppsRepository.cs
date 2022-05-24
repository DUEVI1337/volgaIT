using Microsoft.EntityFrameworkCore;
using VolgaIT.Data.Repository.Interface;
using VolgaIT.Models;

namespace VolgaIT.Data.Repository0
{
    public class UserAppsRepository : IUserAppsRepository
    {
        private readonly DataContext _db;

        public UserAppsRepository(DataContext context)
        {
            _db = context;
        }

        //public async Task<List<UserApp>> GetAllUserApp(string idUser)
        //{
        //    return await _db.Users.Include(x => x.UsersApps).
        //}

        public async Task AddUserAppAsync(UserApp userApp)
        {
            await _db.UsersApps.AddAsync(userApp);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
