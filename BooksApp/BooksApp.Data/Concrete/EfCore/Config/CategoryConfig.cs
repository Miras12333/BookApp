using BooksApp.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BooksApp.Data.Concrete.EfCore.Config
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id); // Устанавливаем первичный ключ
            builder.Property(x => x.Id).ValueGeneratedOnAdd(); // Генерация значения Id при добавлении записи

            // Устанавливаем ограничения и свойства для других полей сущности
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);

            // Начальные данные для таблицы категорий
            builder.HasData(
                new Category { Id = 1, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Name = "Литература", Description = "Литературный жанр здесь", Url="edebiyat" },
                new Category { Id = 2, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Name = "Справочные", Description = "Книги для обращения здесь", Url="basvuru" },
                new Category { Id = 3, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Name = "Детские", Description = "Книги для детей здесь", Url="cocuk" },
                new Category { Id = 4, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Name = "Учебные материалы", Description = "Книги для учебы здесь", Url="ders-kitabi" }
            );
        }
    }
}
