using BooksApp.Entity.Concrete.Identity;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.MVC.Areas.Admin.Models.ViewModels.Accounts
{
    public class UserUpdateViewModel
    {
        public string Id { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage ="Поле \"Имя\" обязательно для заполнения")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Поле \"Фамилия\" обязательно для заполнения")]
        public string LastName { get; set; }

        [DisplayName("Имя пользователя")]
        [Required(ErrorMessage = "Поле \"Имя пользователя\" обязательно для заполнения")]
        public string UserName { get; set; }

        [DisplayName("Электронная почта")]
        [Required(ErrorMessage = "Поле \"Электронная почта\" обязательно для заполнения")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Подтверждение почты")]
        public bool EmailConfirmed { get; set; }

        [Required(ErrorMessage ="Необходимо выбрать хотя бы одну роль")]
        public IList<string> SelectedRoles { get; set; }

        public List<RoleViewModel> Roles { get; set; }
    }
}
