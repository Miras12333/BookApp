using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.MVC.Models.ViewModels.AccountModels
{
    public class RegisterViewModel
    {
        [DisplayName("Имя")]
        [Required(ErrorMessage="Поле \"Имя\" обязательно для заполнения")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Поле \"Фамилия\" обязательно для заполнения")]
        public string LastName { get; set; }

        [DisplayName("Имя пользователя")]
        [Required(ErrorMessage = "Поле \"Имя пользователя\" обязательно для заполнения")]
        public string UserName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Поле \"Email\" обязательно для заполнения")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Поле \"Пароль\" обязательно для заполнения")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Подтверждение пароля")]
        [Required(ErrorMessage = "Поле \"Подтверждение пароля\" обязательно для заполнения")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Пароли должны совпадать!")]
        public string RePassword { get; set; }
    }
}
