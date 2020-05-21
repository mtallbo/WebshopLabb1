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
        public List<CartProduct> CartProducts { get; set; }

    }
}
