using VolgaIT.Models;

namespace VolgaIT.Data.Repository.Interface
{
    public interface IRequestRepository
    {
        Task AddRequsetAsync(RequestUser requestUser);
        Task SaveAsync();
    }
}
