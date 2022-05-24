using System.ComponentModel.DataAnnotations;

namespace VolgaIT.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Это поле не может быть пустым")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Это поле не может быть пустым")]
        [DataType(DataType.Password)]
        [StringLength(200, ErrorMessage = "Пароль должен быть больше 2 символов", MinimumLength = 3)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Это поле не может быть пустым")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }
}