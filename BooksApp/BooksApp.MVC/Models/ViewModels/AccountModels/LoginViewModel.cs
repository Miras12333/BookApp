using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.MVC.Models.ViewModels.AccountModels
{
    public class LoginViewModel
    {
        [DisplayName("Имя пользователя")]
        [Required(ErrorMessage ="Поле \"Имя пользователя\" не должно быть пустым")]
        public string UserName { get; set; }

        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Поле \"Пароль\" не должно быть пустым")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
