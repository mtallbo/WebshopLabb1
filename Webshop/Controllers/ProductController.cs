using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webshop.Services;

namespace Webshop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productservice)
        {
            _productService = productservice;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAll();
            return View(products);
        }

        public async Task<IActionResult> Details(Guid productId)
        {
            var product = await _productService.GetById(productId);
            if (product == null) 
            {
                return NotFound();
            }
            return View(product);
        }
    }
}