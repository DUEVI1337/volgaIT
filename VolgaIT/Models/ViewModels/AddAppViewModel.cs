using System.ComponentModel.DataAnnotations;

namespace VolgaIT.Models.ViewModels
{
    public class AddAppViewModel
    {
        [Required]
        public string AppId { get; set; }
        [Required]
        public string AppName { get; set; }
    }
}
