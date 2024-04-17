using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Core
{
    public static class Jobs
    {
        public static string GetUrl(string text)
        {
            #region Комментарии
            /*
             * Этот метод преобразует переданный ему текст:
             * 1) Заменяет символы, которые могут вызвать проблемы при преобразовании в латинский алфавит, такие как İ-i, ı-I.
             * 2) Заменяет другие турецкие символы соответствующими символами латинского алфавита. Например, ş->s, ö->o.
             * 3) Заменяет пробелы дефисами.
             * 4) Удаляет символы типа точек (.), слэшей (/) и т.д.
             */
            #endregion

            #region Исправление Проблемных Турецких Символов
            text = text.Replace("I", "ı");
            text = text.Replace("İ", "i");
            text = text.Replace("ı", "i");
            #endregion
            #region Преобразование к нижнему регистру
            text = text.ToLower();
            #endregion
            #region Исправление Турецких Символов
            text = text.Replace("ö", "o");
            text = text.Replace("ü", "u");
            text = text.Replace("ş", "s");
            text = text.Replace("ç", "c");
            text = text.Replace("ğ", "g");
            #endregion
            #region Исправление Проблемных Символов
            text = text.Replace(".", "");
            text = text.Replace("/", "");
            text = text.Replace("\\", "");
            text = text.Replace("'", "");
            text = text.Replace("`", "");
            text = text.Replace("\"", "");
            text = text.Replace("(", "");
            text = text.Replace(")", "");
            text = text.Replace("{", "");
            text = text.Replace("}", "");
            text = text.Replace("[", "");
            text = text.Replace("]", "");
            text = text.Replace("?", "");
            text = text.Replace(",", "");
            text = text.Replace("-", "");
            text = text.Replace("_", "");
            text = text.Replace("$", "");
            text = text.Replace("&", "");
            text = text.Replace("%", "");
            text = text.Replace("^", "");
            text = text.Replace("#", "");
            text = text.Replace("+", "");
            text = text.Replace("!", "");
            text = text.Replace("=", "");
            text = text.Replace(";", "");
            text = text.Replace(">", "");
            text = text.Replace("<", "");
            text = text.Replace("|", "");
            text = text.Replace("*", "");

            #endregion
            #region Замена Пробелов Дефисами
            text = text.Replace(" ", "-");
            #endregion
            return text;
        }
        public static string UploadImage(IFormFile image)
        {
            var extension = Path.GetExtension(image.FileName);
            var randomName = $"{Guid.NewGuid()}{extension}";
            //Теперь загружаем изображение на сервер
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/books", randomName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return randomName;
        }
        public static string CreateMessage(string title, string message, string alertType)
        {
            AlertMessage msg = new AlertMessage
            {
                Title = title,
                Message = message,
                AlertType = alertType
            };
            string result = JsonConvert.SerializeObject(msg);
            return result;
        }
    }
}
