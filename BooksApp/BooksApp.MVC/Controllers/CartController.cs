using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using BooksApp.Business.Abstract;
using BooksApp.Core;
using BooksApp.Entity.Concrete;
using BooksApp.Entity.Concrete.Identity;
using BooksApp.MVC.Models.ViewModels;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksApp.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ICartService _cartService;
        private readonly ICartItemService _cartItemService;
        private readonly IOrderService _orderService;
        private readonly INotyfService _notyfService;

        public CartController(UserManager<User> userManager, ICartService cartService, ICartItemService cartItemService, IOrderService orderService, INotyfService notyfService)
        {
            _userManager = userManager;
            _cartService = cartService;
            _cartItemService = cartItemService;
            _orderService = orderService;
            _notyfService = notyfService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var cart = await _cartService.GetCartByUserId(userId);
            CartViewModel cartViewModel = new CartViewModel
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(ci => new CartItemViewModel
                {
                    CartItemId = ci.Id,
                    BookId = ci.BookId,
                    BookName = ci.Book.Name,
                    Quantity = ci.Quantity,
                    BookUrl = ci.Book.Url,
                    ItemPrice = ci.Book.Price,
                    ImageUrl = ci.Book.Images[0].Url
                }).ToList()
            };
            return View(cartViewModel);
        }

        [HttpPost]
        public IActionResult AddToCart(int Id, int Quantity)
        {
            var userId = _userManager.GetUserId(User);
            _cartService.AddToCart(userId, Id, Quantity);
            _notyfService.Success("Товар успешно добавлен в вашу корзину");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteFromCart(int id)
        {
            var cartItem = await _cartItemService.GetByIdAsync(id);
            _cartItemService.Delete(cartItem);

            _notyfService.Information("Товар удален из корзины", 2);
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart(int id)
        {
            _cartItemService.ClearCart(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeQuantity(CartItemViewModel cartItemViewModel)
        {
            if (ModelState.IsValid)
            {
                await _cartItemService.ChangeQuantityAsync(cartItemViewModel.CartItemId, cartItemViewModel.Quantity);
            }
            else
            {
                TempData["QuantityMessage"] = "Минимальное количество - 1";
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var cart = await _cartService.GetCartByUserId(userId);
            OrderViewModel orderViewModel = new OrderViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                City = user.City,
                Phone = user.PhoneNumber,
                Email = user.Email,
                Cart = new CartViewModel
                {
                    CartId = cart.Id,
                    CartItems = cart.CartItems.Select(ci => new CartItemViewModel
                    {
                        CartItemId = ci.Id,
                        BookId = ci.BookId,
                        BookName = ci.Book.Name,
                        ItemPrice = ci.Book.Price,
                        Quantity = ci.Quantity,
                        ImageUrl = ci.Book.Images[0].Url
                    }).ToList(),
                }
            };
            return View(orderViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderViewModel orderViewModel)
        {
            var userId = _userManager.GetUserId(User);
            var cart = await _cartService.GetCartByUserId(userId);
            if (ModelState.IsValid)
            {

                orderViewModel.Cart = new CartViewModel
                {
                    CartId = cart.Id,
                    CartItems = cart.CartItems.Select(ci => new CartItemViewModel
                    {
                        CartItemId = ci.Id,
                        BookId = ci.BookId,
                        BookName = ci.Book.Name,
                        ItemPrice = ci.Book.Price,
                        ImageUrl = ci.Book.Images[0].Url,
                        Quantity = ci.Quantity
                    }).ToList()
                };
                //Здесь мы проведем ОБРАБОТКУ ОПЛАТЫ.

                //2) Если номер карты действителен и нет других проблем, мы примем оплату.
                //3) Если оплата прошла успешно, мы ЗАПИШЕМ ПРОДАЖИ, то есть создадим заказ!

                //Сначала проверяем номер карты.
                if (!CardNumberControl(orderViewModel.CardNumber))
                {
                    _notyfService.Error("Недействительный номер карты!");
                    return View(orderViewModel);
                }

                //Payment payment = PaymentProcess(orderViewModel);
                //if (payment.Status == "success")
                //{
                    SaveOrder(orderViewModel, userId);
                    _cartItemService.ClearCart(orderViewModel.Cart.CartId);
                    _notyfService.Success("Ваш платеж принят, и ваш заказ оформлен!");
                    return RedirectToAction("Index", "Home");
                //}
                //_notyfService.Error("Произошла ошибка!");
            }
            orderViewModel.Cart = new CartViewModel
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(ci => new CartItemViewModel
                {
                    CartItemId = ci.Id,
                    BookId = ci.BookId,
                    BookName = ci.Book.Name,
                    ItemPrice = ci.Book.Price,
                    Quantity = ci.Quantity,
                    ImageUrl = ci.Book.Images[0].Url
                }).ToList()
            };
            return View(orderViewModel);
        }

        private async void SaveOrder(OrderViewModel orderViewModel, string userId)
        {
            Order order = new Order();
            order.OrderState = EnumOrderState.Unpaid;
            order.OrderType = EnumOrderType.CreditCard;
            order.OrderDate = DateTime.Now;
            order.FirstName = orderViewModel.FirstName;
            order.LastName = orderViewModel.LastName;
            order.NormalizedName = (orderViewModel.FirstName + orderViewModel.LastName).ToUpper();
            order.Phone = orderViewModel.Phone;
            order.Email = orderViewModel.Email;
            order.Address = orderViewModel.Address;
            order.City = orderViewModel.City;
            order.UserId = userId;
            order.OrderItems = new List<Entity.Concrete.OrderItem>();
            foreach (var cartItem in orderViewModel.Cart.CartItems)
            {
                Entity.Concrete.OrderItem orderItem = new Entity.Concrete.OrderItem();
                orderItem.Price = cartItem.ItemPrice;
                orderItem.Quantity = cartItem.Quantity;
                orderItem.BookId = cartItem.BookId;
                order.OrderItems.Add(orderItem);
            }
            await _orderService.CreateAsync(order);
        }

        private bool CardNumberControl(string cardNumber)
        {
            #region Comments
            /* - We will remove spaces and dashes from cardNumber.
             * - We will check the length of cardNumber.
             * - We will check for numerical values.
             * - We will test compliance with the Luhn algorithm
             */
            #endregion
            cardNumber = cardNumber.Replace("-", "").Replace(" ", "");
            if (cardNumber.Length != 16) return false;
            foreach (var chr in cardNumber)
            {
                if (!Char.IsNumber(chr)) return false;
            }
            int oddTotal = 0;
            int evenTotal = 0;
            for (int i = 0; i < cardNumber.Length; i += 2)
            {
                int nextOddNumber = Convert.ToInt32(cardNumber[i].ToString());
                int nextEvenNumber = Convert.ToInt32(cardNumber[i + 1].ToString());
                int addedOddNumber = nextOddNumber * 2;
                addedOddNumber = addedOddNumber >= 10 ? addedOddNumber - 9 : addedOddNumber;
                oddTotal += addedOddNumber;
                evenTotal += nextEvenNumber;
            }
            int total = oddTotal + evenTotal;
            bool isValidNumber = total % 10 == 0 ? true : false;
            return isValidNumber;
        }

        private Payment PaymentProcess(OrderViewModel orderViewModel)
        {
            #region Payment Options Created
            Options options = new Options();
            options.ApiKey = "sandbox-GE1AGKrFySgl9T8mcd78R3b9zcmPyGcN";
            options.SecretKey = "sandbox-duB9wikGW6YHl4xD1zoeuCajb3alnBdG";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";
            #endregion
            #region Create Payment Request
            CreatePaymentRequest request = new CreatePaymentRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = new Random().Next(1000000, 9999999).ToString(),
                Price = Convert.ToInt32(orderViewModel.Cart.TotalPrice()).ToString(),
                PaidPrice = Convert.ToInt32(orderViewModel.Cart.TotalPrice()).ToString(),
                Currency = Currency.TRY.ToString(),
                Installment = 1,
                BasketId = orderViewModel.Cart.CartId.ToString(),
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                PaymentCard = new PaymentCard
                {
                    CardHolderName = orderViewModel.CardName,
                    CardNumber = orderViewModel.CardNumber,
                    ExpireMonth = orderViewModel.ExpirationMonth,
                    ExpireYear = orderViewModel.ExpirationYear,
                    Cvc = orderViewModel.Cvc,
                    RegisterCard = 0
                },
                Buyer = new Buyer
                {
                    Id = "BY999",
                    Name = orderViewModel.FirstName,
                    Surname = orderViewModel.LastName,
                    GsmNumber = orderViewModel.Phone,
                    Email = orderViewModel.Email,
                    IdentityNumber = "87955588899",
                    RegistrationAddress = orderViewModel.Address,
                    Ip = "84.99.155.212",
                    City = orderViewModel.City,
                    Country = "Türkiye",
                    ZipCode = "34700"
                },
                ShippingAddress = new Address
                {
                    ContactName = orderViewModel.FirstName + " " + orderViewModel.LastName,
                    City = orderViewModel.City,
                    Country = "Türkiye",
                    Description = orderViewModel.Address
                },
                BillingAddress = new Address
                {
                    ContactName = orderViewModel.FirstName + " " + orderViewModel.LastName,
                    City = orderViewModel.City,
                    Country = "Türkiye",
                    Description = orderViewModel.Address
                }
            };
            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem basketItem;
            foreach (var item in orderViewModel.Cart.CartItems)
            {
                basketItem = new BasketItem
                {
                    Id = item.CartItemId.ToString(),
                    Name = item.BookName.ToString(),
                    Category1 = "Ürün",
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = Convert.ToInt32(item.ItemPrice * item.Quantity).ToString()
                };
                basketItems.Add(basketItem);
            }
            request.BasketItems = basketItems;
            #endregion
            Payment payment = Payment.Create(request, options);
            return payment;
        }
    }
}
