using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Webshop.Models;
using Webshop.Services;
using Webshop.ViewModels;

namespace Webshop.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;

        public const string SessionCartName = "_Cart";
        public const string SessionTotalPrice = "_TotalPrice";

        public OrderController(IOrderService orderservice , UserManager<ApplicationUser> usermanager)
        {
            _orderService = orderservice;
            _userManager = usermanager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout(CartViewModel cartvm)
        {
            var token = HttpContext.Session.Get<TokenBearer>("Token");
            if(token == null)
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name);
                var getToken = await _orderService.GetToken(user.Result.Email);
                HttpContext.Session.Set("Token", getToken.Token);
            }
            var response = await _orderService.CreateOrder(cartvm, token);
            if(response != null)
            {
                HttpContext.Session.Set(SessionCartName, new List<CartProduct>());
                HttpContext.Session.Set<decimal>(SessionTotalPrice, 0);
            }

            return RedirectToAction("Details", "Order", new { id = response.Id });
        }

        public async Task<IActionResult> Details(Guid id) 
        {
            var token = HttpContext.Session.Get<TokenBearer>("Token");
            var order = await _orderService.GetById(id, token);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }
}