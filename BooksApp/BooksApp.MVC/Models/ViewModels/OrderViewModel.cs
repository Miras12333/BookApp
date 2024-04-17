using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.MVC.Models.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage ="Поле 'Имя' не может быть пустым.")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Поле 'Фамилия' не может быть пустым.")]
        public string LastName { get; set; }

        [DisplayName("Адрес")]
        [Required(ErrorMessage = "Поле 'Адрес' не может быть пустым.")]
        public string Address { get; set; }

        [DisplayName("Город")]
        [Required(ErrorMessage = "Поле 'Город' не может быть пустым.")]
        public string City { get; set; }

        [DisplayName("Номер телефона")]
        [Required(ErrorMessage = "Поле 'Номер телефона' не может быть пустым.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Поле 'Email' не может быть пустым.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public DateTime OrderDate { get; set; }
        public CartViewModel Cart { get; set; }
        public List<CartItemViewModel> OrderItems { get; set; }

        // Свойства, связанные с кредитной картой
        [DisplayName("Имя и фамилия на карте")]
        [Required(ErrorMessage ="Имя и фамилия обязательны")]
        public string CardName { get; set; }

        [DisplayName("Номер карты")]
        [Required(ErrorMessage = "Номер карты обязателен")]
        public string CardNumber { get; set; }

        [DisplayName("Месяц истечения срока")]
        [Required(ErrorMessage = "Месяц обязателен")]
        public string ExpirationMonth { get; set; }

        [DisplayName("Год истечения срока")]
        [Required(ErrorMessage = "Год обязателен")]
        public string ExpirationYear { get; set; }

        [DisplayName("CVC-код")]
        [Required(ErrorMessage = "CVC-код обязателен")]
        public string Cvc { get; set; }
    }
}
