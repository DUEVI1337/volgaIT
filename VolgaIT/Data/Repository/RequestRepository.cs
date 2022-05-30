using VolgaIT.Data.Repository.Interface;
using VolgaIT.Models;

namespace VolgaIT.Data.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly DataContext _db;
        public RequestRepository(DataContext context)
        {
            _db = context;
        }

        public async Task AddRequsetAsync(RequestUser requestUser)
        {
            await _db.RequestUsers.AddAsync(requestUser);
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
