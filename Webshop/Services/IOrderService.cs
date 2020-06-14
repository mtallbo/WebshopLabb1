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
        Task<Order> GetById(Guid id, TokenBearer token);
        Task<Order> CreateOrder(CartViewModel cartviewModel, TokenBearer token);
        Task<TokenBearer> GetToken(string email);
    }
}
