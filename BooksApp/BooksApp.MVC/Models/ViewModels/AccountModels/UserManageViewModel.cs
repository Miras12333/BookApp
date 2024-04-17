using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.MVC.Models.ViewModels.AccountModels
{
    public class UserManageViewModel
    {
        public string Id { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Поле 'Имя' обязательно для заполнения")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Поле 'Фамилия' обязательно для заполнения")]
        public string LastName { get; set; }

        [DisplayName("Пол")]
        public string Gender { get; set; }

        [DisplayName("Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [DisplayName("Адрес")]
        public string Address { get; set; }

        [DisplayName("Город")]
        public string City { get; set; }

        [DisplayName("Имя пользователя")]
        [Required(ErrorMessage = "Поле 'Имя пользователя' обязательно для заполнения")]
        public string UserName { get; set; }

        [DisplayName("Электронная почта")]
        [Required(ErrorMessage = "Поле 'Электронная почта' обязательно для заполнения")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public List<SelectListItem> GenderSelectList { get; set; }
        public List<OrderViewModel> Orders { get; set; }
    }
}
