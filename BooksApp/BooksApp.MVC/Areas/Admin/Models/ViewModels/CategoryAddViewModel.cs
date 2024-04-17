using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.MVC.Areas.Admin.Models.ViewModels
{
    public class CategoryAddViewModel
    {
        [DisplayName("Название категории")]
        [Required(ErrorMessage = "Поле \"Название категории\" не должно быть пустым")]
        [MinLength(5, ErrorMessage = "Название категории должно содержать минимум 5 символов")]
        [MaxLength(100, ErrorMessage = "Название категории должно содержать максимум 100 символов")]
        public string Name { get; set; }

        [DisplayName("Описание категории")]
        [Required(ErrorMessage = "Поле \"Описание категории\" не должно быть пустым")]
        [MinLength(5, ErrorMessage = "Описание категории должно содержать минимум 5 символов")]
        [MaxLength(500, ErrorMessage = "Описание категории должно содержать максимум 500 символов")]
        public string Description { get; set; }

        public string Url { get; set; }

        [DisplayName("Подтверждено")]
        public bool IsApproved { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
