using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Models
{
    public class CartProduct
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
