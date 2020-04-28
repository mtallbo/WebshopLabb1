using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webshop.Models;

namespace Webshop.Services
{
    public interface IProductService
    {
        Product GetById(Guid id);
        List<Product> GetAll();
    }
}
