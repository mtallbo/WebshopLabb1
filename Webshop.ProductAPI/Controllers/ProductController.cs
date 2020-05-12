using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webshop.ProductAPI.Data;
using Webshop.ProductAPI.Models;

namespace Webshop.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;
        }

        //GET: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() 
        {
            var products = await _context.Products.ToListAsync();
            if(products != null && products.Count() != 0)
            {
                return products;
            } else
            {
                return Ok(new { Message = "No products found" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid? id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product != null)
            {
                return product;
            } else
            {
                return Ok(new { Message = "Product not found" });
            }
        }
    }
}
