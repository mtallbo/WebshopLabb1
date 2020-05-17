using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webshop.Models;

namespace Webshop.Services
{
    public interface IProductService
    {
        Task<Product> GetById(Guid id);
        Task<List<Product>> GetAll();
    }
}
