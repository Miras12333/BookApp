using BooksApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BooksApp.MVC.Areas.Admin.Models.ViewModels
{
    public class AuthorAddViewModel
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsApproved { get; set; }

        [DisplayName("Имя автора")]
        [Required(ErrorMessage = "Поле \"Имя автора\" не должно быть пустым")]
        [MinLength(5, ErrorMessage = "Имя автора должно содержать минимум 5 символов")]
        [MaxLength(100, ErrorMessage = "Имя автора должно содержать максимум 100 символов")]
        public string Name { get; set; }

        [DisplayName("Дата рождения")]
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }

        [DisplayName("Об авторе")]
        [Required(ErrorMessage = "Поле \"Об авторе\" не должно быть пустым")]
        [MinLength(5, ErrorMessage = "Поле \"Об авторе\" должно содержать минимум 5 символов")]
        [MaxLength(1000, ErrorMessage = "Поле \"Об авторе\" должно содержать максимум 1000 символов")]
        public string About { get; set; }
        public List<Book> Books { get; set; }
    }
}
