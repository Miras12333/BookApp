using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using BooksApp.Business.Abstract;
using BooksApp.Business.Concrete;
using BooksApp.Data.Abstract;
using BooksApp.Data.Concrete.EfCore;
using BooksApp.Data.Concrete.EfCore.Context;
using BooksApp.Entity.Concrete.Identity;
using BooksApp.MVC.EmailServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BooksAppContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<BooksAppContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;// Пароль должен содержать хотя бы одну цифру
    options.Password.RequireLowercase = true;// Пароль должен содержать хотя бы одну строчную букву
    options.Password.RequireUppercase = true;// Пароль должен содержать хотя бы одну заглавную букву
    options.Password.RequiredLength = 6; // Длина пароля должна быть не менее 6 символов
    options.Password.RequireNonAlphanumeric = true;// Пароль должен содержать хотя бы один специальный символ
    // Пример допустимого пароля: Qwe123.

    options.Lockout.MaxFailedAccessAttempts = 3;// Максимальное количество неудачных попыток входа подряд: 3
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);// После 5 минут блокировки можно снова войти

    options.User.RequireUniqueEmail = true;// Уникальный email адрес для регистрации
    options.SignIn.RequireConfirmedEmail = false;// Подтверждение email адреса не требуется
    options.SignIn.RequireConfirmedPhoneNumber = false;// Подтверждение номера телефона не требуется
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";// Путь к странице входа
    options.LogoutPath = "/account/logout";// Путь к странице выхода
    options.AccessDeniedPath = "/account/accessdenied";// Путь к странице доступа запрещён
    options.SlidingExpiration = true;// Срок действия cookie обновляется при каждом запросе
    options.ExpireTimeSpan = TimeSpan.FromDays(10);// Срок действия cookie - 10 дней
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        Name = ".BooksApp.Security.Cookie"
    };
});

builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IBookService, BookManager>();
builder.Services.AddScoped<IAuthorService, AuthorManager>();
builder.Services.AddScoped<IImageService, ImageManager>();
builder.Services.AddScoped<ICartService, CartManager>();
builder.Services.AddScoped<ICartItemService, CartItemManager>();
builder.Services.AddScoped<IOrderService, OrderManager>();

builder.Services.AddScoped<ICategoryRepository, EfCoreCategoryRepository>();
builder.Services.AddScoped<IBookRepository, EfCoreBookRepository>();
builder.Services.AddScoped<IAuthorRepository, EfCoreAuthorRepository>();
builder.Services.AddScoped<IImageRepository, EfCoreImageRepository>();
builder.Services.AddScoped<ICartRepository, EfCoreCartRepository>();
builder.Services.AddScoped<ICartItemRepository, EfCoreCartItemRepository>();
builder.Services.AddScoped<IOrderRepository, EfCoreOrderRepository>();

builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(options => new SmtpEmailSender(
    builder.Configuration["EmailSender:Host"],
    builder.Configuration.GetValue<int>("EmailSender:Port"),
    builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
    builder.Configuration["EmailSender:UserName"],
    builder.Configuration["EmailSender:Password"]
));

builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 5;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseNotyf();


app.MapControllerRoute(
    name: "bookdetails",
    pattern: "bookdetails/{url}",
    defaults: new { controller = "Home", action = "BookDetails" }
);

app.MapControllerRoute(
    name: "categories",
    pattern: "books/{categoryurl?}",
    defaults: new { controller = "Home", action = "Index" }
);

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "admin/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    
app.Run();
