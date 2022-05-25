using VolgaIT.Models;

namespace VolgaIT.Services.Interface
{
    public interface IEventService
    {
        Task<List<Event>> GetAllEventAsync();
        Task<Event> GetEventByNameAsync(string nameEvent);
    }
}
