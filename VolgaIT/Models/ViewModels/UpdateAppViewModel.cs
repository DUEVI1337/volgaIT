using System.ComponentModel.DataAnnotations;

namespace VolgaIT.Models.ViewModels
{
    public class UpdateAppViewModel
    {
        [Required]
        public string AppId { get; set; }
        [Required(ErrorMessage = "Это поле не может быть пустым")]
        public string AppName { get; set; }
        [Required]
        public string AddDateCreate { get; set; }
    }
}
