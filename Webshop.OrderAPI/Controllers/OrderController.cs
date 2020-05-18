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
            var orders = await _context.Orders.ToListAsync();
            if (orders != null && orders.Count() != 0)
            {
                return orders;
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
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                return order;
            }
            else
            {
                return Ok(new { Message = $"Order with id:{id} does not exist" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> PutOrder(Guid? id, Order order)
        {
            if (id == null || order == null)
            {
                return BadRequest();
            }
            var existingorder = await _context.Orders.FindAsync(id);
            if (existingorder == null)
            {
                return Ok(new { Message = $"Order with id:{id} does not exist" });
            }
            order.Id = existingorder.Id;
            _context.Entry(existingorder).CurrentValues.SetValues(order);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { Message = $"Order id:{existingorder.Id} updated" });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromBody]Order order)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(
                nameof(PostOrder),
                new { Id = order.Id },
                order);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return Ok(new { Message = $"Order id:{id} does not exist" });
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
