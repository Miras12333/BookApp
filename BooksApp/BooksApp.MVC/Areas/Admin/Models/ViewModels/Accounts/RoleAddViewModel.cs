using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.MVC.Areas.Admin.Models.ViewModels.Accounts
{
    public class RoleAddViewModel
    {
        [DisplayName("Название роли")]
        [Required(ErrorMessage ="Поле \"Название роли\" обязательно для заполнения")]
        public string Name { get; set; }

        [DisplayName("Описание роли")]
        [Required(ErrorMessage = "Поле \"Описание роли\" обязательно для заполнения")]
        public string Description { get; set; }
    }
}
