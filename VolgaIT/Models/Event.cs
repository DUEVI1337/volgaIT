using System.ComponentModel.DataAnnotations;

namespace VolgaIT.Models
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}