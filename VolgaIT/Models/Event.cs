using System.ComponentModel.DataAnnotations;

namespace VolgaIT.Models
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<RequestUser> RequestsUsers { get; set; }
    }
}