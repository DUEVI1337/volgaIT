using System.ComponentModel.DataAnnotations;

namespace VolgaIT.Models.ViewModels
{
    public class AddAppViewModel
    {
        [Required(ErrorMessage = "Это поле не может быть пустым")]
        public string AppId { get; set; }
        [Required(ErrorMessage = "Это поле не может быть пустым")]
        public string AppName { get; set; }
    }
}
