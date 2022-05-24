using Microsoft.EntityFrameworkCore;
using VolgaIT.Data.Repository.Interface;
using VolgaIT.Models;

namespace VolgaIT.Data.Repository
{
    public class AppRepository : IAppRepository
    {
        private readonly DataContext _db;

        public AppRepository(DataContext context)
        {
            _db = context;
        }

        public async Task<List<App>> GetAllAppAsync()
        {
            return await _db.Apps.ToListAsync();
        }

        public async Task<List<App>> GetAllUserApp(string userId)
        {
            return _db.Apps.Where(x=>x.UsersApps.ToList() ==).ToListAsync();
        }

        public async Task AddAppAsync(App app)
        {
            await _db.Apps.AddAsync(app);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
