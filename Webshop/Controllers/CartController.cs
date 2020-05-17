using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Webshop.Models;
using Webshop.Services;
using Webshop.ViewModels;

namespace Webshop.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(IProductService productservice, UserManager<ApplicationUser> usermanager)
        {
            _productService = productservice;
            _userManager = usermanager;
        }

        public const string SessionCartName = "_Cart";
        public const string SessionTotalPrice = "_TotalPrice";

        [Authorize]
        public IActionResult Index()
        {
            var sessionCart = HttpContext.Session.Get<List<CartProduct>>(SessionCartName);
            var sessionTotalPrice = HttpContext.Session.Get<decimal>(SessionTotalPrice);
            var user = _userManager.GetUserAsync(User);

            CartViewModel cvm = new CartViewModel();
            if (user != null)
            {
                cvm.User = user.Result;
            }

            if (sessionCart != null)
            {
                cvm.CartProducts = sessionCart;
                ViewBag.TotalPrice = sessionTotalPrice;
                return View(cvm);
            }
            else
            {
                cvm.CartProducts = new List<CartProduct>();
                ViewBag.EmptyCart = "Tom varukorg";
                return View(cvm);
            };
        }

        //[Authorize]
        //public IActionResult Add(Guid productId)
        //{
        //    var product = _productService.GetById(productId);
        //    var cart = HttpContext.Session.Get<List<CartProduct>>(SessionCartName);
        //    if (cart != null)
        //    {
        //        if (cart.Any(p => p.Product.Id == productId))
        //        {
        //            var index = cart.FindIndex(p => p.Product.Id == productId);
        //            cart[index].Quantity++;
        //        }
        //        else
        //        {
        //            cart.Add(new CartProduct { Product = product, Quantity = 1 });
        //        }
        //    }
        //    else
        //    {
        //        cart = new List<CartProduct> { };
        //        cart.Add(new CartProduct { Product = product, Quantity = 1 });
        //    }
        //    var totalPrice = cart.Sum(p => p.Product.Price * p.Quantity);
        //    HttpContext.Session.Set(SessionCartName, cart);
        //    HttpContext.Session.Set<decimal>(SessionTotalPrice, totalPrice);
        //    return RedirectToAction("Index", "Product");
        //}
    }
    public static class SessionExtension
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
