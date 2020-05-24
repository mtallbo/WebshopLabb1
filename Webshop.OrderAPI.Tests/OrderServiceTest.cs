using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Webshop.OrderAPI.Models;
using Xunit;

namespace Webshop.OrderAPI.Tests
{
    public class OrderServiceTest
    {
        [Fact]
        public async Task GetAll_Return200()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/order");
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
        [Fact]
        public async Task GetSingleRandomGuid_Return400()
        {
            using (var client = new TestClientProvider().Client)
            {
                Guid g = Guid.NewGuid();
                var response = await client.GetAsync($"/api/order/{g}");
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
        [Fact]
        public async Task GetSingleNotAGuid_Return400()
        {
            using (var client = new TestClientProvider().Client)
            {
                Guid g = Guid.NewGuid();
                var response = await client.GetAsync($"/api/order/123");
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
        
        [Fact]
        public async Task PostSingle_Return200()
        {
            using (var client = new TestClientProvider().Client)
            {
                var testOrder = new Order
                {
                    Id = Guid.Parse("7d3e444b-5dc3-453e-b5cf-c65caf744ca4"),
                    Adress = "Testgatan",
                    City = "Sthlm",
                    Email = "test@test.se",
                    FirstName = "Mikael",
                    LastName = "Tallbo",
                    PhoneNumber = "123 123 123",
                    PostalCode = "174 43",
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Name = "TEST ITEM",
                            Quantity = 1,
                            Order = new Order(){Id = Guid.Parse("7d3e444b-5dc3-453e-b5cf-c65caf744ca4")}
                        }
                    }
                };

                var serializedOrder = new StringContent(JsonConvert.SerializeObject(testOrder), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"/api/order", serializedOrder);

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }
        [Fact]
        public async Task UpdateSingle_Return204()
        {
            //id to remove
            var id = "7d3e444b-5dc3-453e-b5cf-c65caf744ca4";
            using (var client = new TestClientProvider().Client)
            {
                //get order deserialized
                var orderResponse = await client.GetAsync($"/api/order/{id}");
                var content = await orderResponse.Content.ReadAsStringAsync();

                var deserializedOrder = JsonConvert.DeserializeObject<Order>(content);

                //update order
                deserializedOrder.FirstName = "forsenCD";

                //serialize order
                var serializedProduct = new StringContent(JsonConvert.SerializeObject(deserializedOrder), Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"/api/order/{id}", serializedProduct);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task RemoveSingle_Return204()
        {
            //id to remove
            var id = "7d3e444b-5dc3-453e-b5cf-c65caf744ca4";
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync($"/api/order/{id}");
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }
        }
    }
}
