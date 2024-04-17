using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.MVC.Models.ViewModels.AccountModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Токен обязателен")]
        public string Token { get; set; }

        [DisplayName("Адрес электронной почты")]
        [Required(ErrorMessage = "Адрес электронной почты обязателен")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Пароль обязателен")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Подтверждение пароля")]
        [Required(ErrorMessage = "Подтверждение пароля обязательно")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли должны совпадать")]
        public string RePassword { get; set; }
    }
}
