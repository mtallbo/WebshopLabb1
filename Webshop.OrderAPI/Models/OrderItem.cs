using System;

namespace Webshop.OrderAPI.Models
{
    public class OrderItem
    {
        public Guid Id{ get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public decimal Price { get; set; }
        public Order Order { get; set; }
    }
}