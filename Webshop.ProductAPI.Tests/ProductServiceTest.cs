using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Webshop.ProductAPI.Models;
using Xunit;

namespace Webshop.ProductAPI.Tests
{
    public class ProductServiceTest
    {
        [Theory]
        [InlineData("/api/product")]
        public async Task GetAllEndpoints_Return200(string endpoint)
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync(endpoint);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
        [Fact]
        public async Task GetSingleRandomGuid_Return400()
        {
            using (var client = new TestClientProvider().Client)
            {
                Guid g = Guid.NewGuid();
                var response = await client.GetAsync($"/api/product/{g}");
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
        [Fact]
        public async Task GetSingleNotAGuid_Return400()
        {
            using (var client = new TestClientProvider().Client)
            {
                Guid g = Guid.NewGuid();
                var response = await client.GetAsync($"/api/product/123");
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
        [Fact]
        public async Task PostSingle_Return200() 
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

                var serializedProduct1 = new StringContent(JsonConvert.SerializeObject(testProduct), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"/api/product", serializedProduct1);

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }
        [Fact]
        public async Task UpdateSingle_Return204()
        {
            //id to remove
            var id = "df46fa43-2b5f-460b-832e-a611e57aeec6";
            using (var client = new TestClientProvider().Client)
            {
                //get product deserialized
                var productResponse = await client.GetAsync($"/api/product/{id}");
                var content = await productResponse.Content.ReadAsStringAsync();

                var deserializedProduct = JsonConvert.DeserializeObject<Product>(content);

                //update product
                deserializedProduct.Name = "forsenCD";

                //serialize product
                var serializedProduct = new StringContent(JsonConvert.SerializeObject(deserializedProduct), Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"/api/product/{id}", serializedProduct);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task RemoveSingle_Return204()
        {
            //id to remove
            var id = "df46fa43-2b5f-460b-832e-a611e57aeec6";
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync($"/api/product/{id}");
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }
        }
    }
}
