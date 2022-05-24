using VolgaIT.Models;

namespace VolgaIT.Data.Repository.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string emailUser);
    }
}
