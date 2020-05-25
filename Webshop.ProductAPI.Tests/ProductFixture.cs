using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Webshop.ProductAPI.Models;

namespace Webshop.ProductAPI.Tests
{
    public class ProductFixture : IDisposable
    {
        public Product product { get; private set; }

        public ProductFixture()
        {
            product = Initialize().Result;
        }

        private async Task<Product> Initialize()
        {
            using (var client = new TestClientProvider().Client)
            {
                var testProduct = new Product
                {
                    Id = Guid.Parse("df46fa43-2b5f-460b-832e-a611e57aeec6"),
                    Name = "Test",
                    DateCreated = DateTime.Now,
                    Description = "Test",
                    Price = 1337.00M
                };

                var serializedProduct = new StringContent(JsonConvert.SerializeObject(testProduct), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"/api/product", serializedProduct);
                var responseProduct = await response.Content.ReadAsStringAsync();

                var createdProduct = JsonConvert.DeserializeObject<Product>(responseProduct);
                return createdProduct;
            }
        }

        public async void Dispose()
        {
            using (var client = new TestClientProvider().Client)
            {
                var deleteResponse = await client.DeleteAsync($"/api/product/{product.Id}");
                deleteResponse.EnsureSuccessStatusCode();
            }
        }
    }
}
