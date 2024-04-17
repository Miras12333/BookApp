namespace BooksApp.MVC.EmailServices
{
    public interface IEmailSender
    {
        /* Это интерфейс, который мы создали для настройки методов отправки электронной почты в зависимости от различных почтовых серверов */
        Task SendEmailAsync(string email, string subject, string body);
    }
}
