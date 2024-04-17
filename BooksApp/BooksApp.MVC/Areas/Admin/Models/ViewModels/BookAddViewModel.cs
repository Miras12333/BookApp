using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BooksApp.Entity.Concrete;
using Microsoft.AspNetCore.Http;

namespace BooksApp.MVC.Areas.Admin.Models.ViewModels
{
    public class BookAddViewModel
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        [DisplayName("Подтверждено")]
        public bool IsApproved { get; set; }

        [DisplayName("Название книги")]
        [Required(ErrorMessage = "Поле \"Название книги\" не должно быть пустым")]
        [MinLength(5, ErrorMessage = "Название книги должно содержать минимум 5 символов")]
        [MaxLength(100, ErrorMessage = "Название книги должно содержать максимум 100 символов")]
        public string Name { get; set; }

        [DisplayName("Количество на складе")]
        [Required(ErrorMessage = "Поле \"Количество на складе\" не должно быть пустым")]
        public int? Stock { get; set; }

        [DisplayName("Цена")]
        [Required(ErrorMessage = "Поле \"Цена\" не должно быть пустым")]
        public decimal? Price { get; set; }

        [DisplayName("Количество страниц")]
        [Required(ErrorMessage = "Поле \"Количество страниц\" не должно быть пустым")]
        public int? PageCount { get; set; }

        [DisplayName("Год издания")]
        [Required(ErrorMessage = "Поле \"Год издания\" не должно быть пустым")]
        public int? EditionYear { get; set; }

        [DisplayName("Номер издания")]
        [Required(ErrorMessage = "Поле \"Номер издания\" не должно быть пустым")]
        public int? EditionNumber { get; set; }

        [Required(ErrorMessage = "Необходимо выбрать как минимум одну категорию")]
        public int[] SelectedCategories { get; set; }
        public List<Category> Categories { get; set; }

        [Required(ErrorMessage = "Необходимо выбрать как минимум одного автора")]
        public int[] SelectedAuthors { get; set; }
        public List<Author> Authors { get; set; }

        [DisplayName("Ссылка")]
        public string Url { get; set; }

        [DisplayName("Описание книги")]
        [Required(ErrorMessage = "Поле \"Описание книги\" не должно быть пустым")]
        [MinLength(5, ErrorMessage = "Описание книги должно содержать минимум 5 символов")]
        [MaxLength(1000, ErrorMessage = "Описание книги должно содержать максимум 1000 символов")]
        public string Summary { get; set; }

        [DisplayName("Изображение")]
        [Required(ErrorMessage = "Необходимо выбрать изображение")]
        public List<IFormFile> Images { get; set; }
    }
}
