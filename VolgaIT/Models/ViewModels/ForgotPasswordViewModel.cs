using System.ComponentModel.DataAnnotations;

namespace VolgaIT.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Ввведите почту")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Неккоректный адрес")]
        public string Email { get; set; }
    }
}