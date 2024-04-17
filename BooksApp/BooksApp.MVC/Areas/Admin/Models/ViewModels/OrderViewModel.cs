using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.MVC.Areas.Admin.Models.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        
        [DisplayName("Имя")]
        [Required(ErrorMessage ="Поле имени не может быть пустым.")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Поле фамилии не может быть пустым.")]
        public string LastName { get; set; }

        [DisplayName("Адрес")]
        [Required(ErrorMessage = "Поле адреса не может быть пустым.")]
        public string Address { get; set; }

        [DisplayName("Город")]
        [Required(ErrorMessage = "Поле города не может быть пустым.")]
        public string City { get; set; }

        [DisplayName("Телефон")]
        [Required(ErrorMessage = "Поле телефона не может быть пустым.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DisplayName("Электронная почта")]
        [Required(ErrorMessage = "Поле электронной почты не может быть пустым.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        public DateTime OrderDate { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}
