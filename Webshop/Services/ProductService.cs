using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webshop.Models;

namespace Webshop.Services
{
    public class ProductService : IProductService
    {
        private List<Product> Products = new List<Product>();
        public ProductService()
        {
            for (int i = 0; i < 10; i++)
            {
                if(i % 2 == 0)
                {
                    Products.Add(new Product
                    {
                        Id = Guid.NewGuid(),
                        Description = "En kaffekopp man kan dricka kaffe ur",
                        ImageURL = "https://networkprojectum.se/wp-content/uploads/2017/11/placeholder.png",
                        Name = "Kaffekopp",
                        Price = 200
                    });
                } else
                {
                    Products.Add(new Product
                    {
                        Id = Guid.NewGuid(),
                        Description = "En gaming mus med RGB för att höja ditt aim",
                        ImageURL = "https://networkprojectum.se/wp-content/uploads/2017/11/placeholder.png",
                        Name = "Logitech G903",
                        Price = 999
                    });
                }
            }
        }

        public Product GetById(Guid id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }
        public List<Product> GetAll()
        {
            return Products;
        }
    }
}
