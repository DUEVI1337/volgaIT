using System.ComponentModel.DataAnnotations;

namespace VolgaIT.Models
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Это поле не может быть пустым")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Это поле не может быть пустым")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}