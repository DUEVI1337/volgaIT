using VolgaIT.Models;

namespace VolgaIT.Data.Repository.Interface
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllEventAsync();
        Task<Event> GetEventByNameAsync(string nameEvent);
    }
}
