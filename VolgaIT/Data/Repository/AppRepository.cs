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

        public async Task<App> GetAppByIdAsync(string appId)
        {
            return await _db.Apps.Include(x=>x.RequestsUsers).FirstOrDefaultAsync(x=>x.Id==appId);
        }

        public async Task<List<App>> GetAppRangeAsync(List<string> appsId)
        {
            return await _db.Apps.Where(x=>appsId.Contains(x.Id)).ToListAsync();
        }

        public async Task AddAppAsync(App app)
        {
            await _db.Apps.AddAsync(app);
        }

        public void UpdateApp(App app)
        {
            _db.Apps.Update(app);
        }

        public void RemoveApp(App app)
        {
            _db.Apps.Remove(app);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
