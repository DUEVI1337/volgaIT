using Microsoft.AspNetCore.Identity;

namespace VolgaIT.Models
{
    public class User : IdentityUser
    {
        public ICollection<UserApp> UsersApps { get; set; }
    }
}
