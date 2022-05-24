using Microsoft.EntityFrameworkCore;
using VolgaIT.Data.Repository.Interface;
using VolgaIT.Models;

namespace VolgaIT.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _db;

        public UserRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<User> GetUserByEmailAsync(string emailUser)
        {
            return await _db.Users.Include(x => x.UsersApps).FirstOrDefaultAsync(x => x.Email == emailUser);
        }
    }
}
