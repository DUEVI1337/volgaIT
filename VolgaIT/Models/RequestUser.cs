using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolgaIT.Models
{
    public class RequestUser
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("App")]
        public string AppId { get; set; }
        public App App { get; set; }
        [ForeignKey("Event")]
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string BonusInfo { get; set; }
    }
}