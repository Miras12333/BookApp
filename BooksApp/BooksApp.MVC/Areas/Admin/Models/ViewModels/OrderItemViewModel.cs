using System.ComponentModel.DataAnnotations;

namespace BooksApp.MVC.Areas.Admin.Models.ViewModels
{
    public class OrderItemViewModel
    {
        public int OrderItemId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookUrl { get; set; }
        public decimal? ItemPrice { get; set; }
        public string ImageUrl { get; set; }
        
        [Required(ErrorMessage = "Это поле не может быть пустым")]
        [Range(1, 10, ErrorMessage = "Количество должно быть от 1 до 10")]
        public int Quantity { get; set; }
    }
}
