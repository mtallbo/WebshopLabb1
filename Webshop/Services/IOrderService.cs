using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webshop.Models;

namespace Webshop.Services
{
    interface IOrderService
    {
        Task<Order> GetById(Guid id);
        Task CreateOrder(Order order);
    }
}
