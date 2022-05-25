using System.ComponentModel.DataAnnotations;

namespace VolgaIT.Models.ViewModels
{
    public class UpdateProfileViewModel
    {
        [Required]
        public string IdUser { get; set; }
        [Required(ErrorMessage = "Это поле не может быть пустым")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string NewEmail { get; set; }
    }
}
