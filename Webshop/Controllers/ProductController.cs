using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult Index()
        {
            //Get string of "User", which is the logged in users name.
            ViewBag.sessionv =  HttpContext.Session.GetString("User");
            var products = _productService.GetAll();
            return View(products);
        }
    }
}