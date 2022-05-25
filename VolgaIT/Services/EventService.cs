using VolgaIT.Data.Repository.Interface;
using VolgaIT.Models;
using VolgaIT.Services.Interface;

namespace VolgaIT.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repoEvent;

        public EventService(IEventRepository repoEvent)
        {
            _repoEvent = repoEvent;
        }

        public async Task<List<Event>> GetAllEventAsync()
        {
            return await _repoEvent.GetAllEventAsync();
        }

        public async Task<Event> GetEventByNameAsync(string nameEvent)
        {
            return await _repoEvent.GetEventByNameAsync(nameEvent);
        }
    }
}
