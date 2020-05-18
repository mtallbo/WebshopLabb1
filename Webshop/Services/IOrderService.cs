using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webshop.Models;
using Webshop.ViewModels;

namespace Webshop.Services
{
    public interface IOrderService
    {
        Task<Order> GetById(Guid id);
        Task<Order> CreateOrder(CartViewModel cartviewModel);
    }
}
