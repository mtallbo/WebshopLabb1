using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Webshop.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Product> CartProducts { get; set; }
    }
}
