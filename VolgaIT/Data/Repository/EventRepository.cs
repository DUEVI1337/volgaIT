using Microsoft.EntityFrameworkCore;
using VolgaIT.Data.Repository.Interface;
using VolgaIT.Models;

namespace VolgaIT.Data.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly DataContext _db;

        public EventRepository(DataContext context)
        {
            _db = context;
        }

        public async Task<List<Event>> GetAllEventAsync()
        {
            return await _db.Events.ToListAsync();
        }

        public async Task<Event> GetEventByNameAsync(string nameEvent)
        {
            return await _db.Events.FirstOrDefaultAsync(x => x.Name == nameEvent);
        }

    }
}
