using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webshop.OrderAPI.Data;
using Webshop.OrderAPI.Models;

namespace Webshop.OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderContext _context;

        public OrderController(OrderContext context)
        {
            _context = context;
        }

        //GET: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var products = await _context.Orders.ToListAsync();
            if (products != null && products.Count() != 0)
            {
                return products;
            }
            else
            {
                return Ok(new { Message = "No orders found" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var product = await _context.Orders.FindAsync(id);
            if (product != null)
            {
                return product;
            }
            else
            {
                return Ok(new { Message = $"Order with id:{id} does not exist" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> PutOrder(Guid? id, Order product)
        {
            if (id == null || product == null)
            {
                return BadRequest();
            }
            var existingorder = _context.Orders.FindAsync(id);
            if (existingorder.Result == null)
            {
                return Ok(new { Message = $"Order with id:{id} does not exist" });
            }
            //existingproduct.Result.Name = product.Name;
            //existingproduct.Result.Description = product.Description;
            //existingproduct.Result.Category = product.Category;
            //existingproduct.Result.Price = product.Price;
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { Message = $"Order id:{existingorder.Result.Id} updated" });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetOrder),
                new { Id = product.Id },
                product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var product = await _context.Orders.FindAsync(id);
            if (product == null)
            {
                return Ok(new { Message = $"Order id:{id} does not exist" });
            }
            _context.Orders.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
