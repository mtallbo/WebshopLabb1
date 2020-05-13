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
            if (products != null && products.Count() != 0)
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
            if (id == null)
            {
                return BadRequest();
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                return product;
            } else
            {
                return Ok(new { Message = $"Product with id:{id} does not exist" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct(Guid? id, Product product)
        {
            if (id == null || product == null)
            {
                return BadRequest();
            }
            var existingproduct = _context.Products.FindAsync(id);
            if (existingproduct.Result == null)
            {
                return Ok(new { Message = $"Product with id:{id} does not exist" });
            }
            existingproduct.Result.Name = product.Name;
            existingproduct.Result.Description = product.Description;
            existingproduct.Result.Category = product.Category;
            existingproduct.Result.Price = product.Price;
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { Message = $"Product id:{existingproduct.Result.Id} updated" });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProduct),
                new { Id = product.Id },
                product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(Guid? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var product = await _context.Products.FindAsync(id);
            if(product == null)
            {
                return Ok(new { Message = $"Product id:{id} does not exist" });
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
