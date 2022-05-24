using System.ComponentModel.DataAnnotations.Schema;

namespace VolgaIT.Models
{
    public class UserApp
    {
        [ForeignKey("User")]
        public string UsersId { get; set; }
        public User User { get; set; }
        [ForeignKey("App")]
        public string AppsId { get; set; }
        public App App { get; set; }
    }
}
