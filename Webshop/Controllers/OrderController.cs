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
            var response = await _orderService.CreateOrder(cartvm);
            
            return RedirectToAction("Details", "Order", response.Id);
        }

        public async Task<IActionResult> Details(Guid id) 
        {
            var order = await _orderService.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }
}