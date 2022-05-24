using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolgaIT.Models
{
    [Index(nameof(Name), IsUnique = true)] //name теперь уникально
    public class App
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Это поле не может быть пустым")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Это поле не может быть пустым")]
        public string Name { get; set; }
        [Required]
        public DateTime DateCreate { get; set; } = DateTime.Now;

        public ICollection<UserApp> UsersApps { get; set; }
    }
}