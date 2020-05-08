using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webshop.Models;

namespace Webshop.ViewModels
{
    public class CartViewModel
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<CartProduct> CartProducts { get; set; }
    }
}
