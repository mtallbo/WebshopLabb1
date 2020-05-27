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
        public async Task RegisterUserForToken_Return200()
        {
            using (var client = new TestClientProvider().Client)
            {
                AuthenticateModel model = new AuthenticateModel()
                {
                    Email = "test@test.se"
                };
                var serializedModel = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var reponse = await client.PostAsync($"/user/register", serializedModel);
                Assert.Equal(HttpStatusCode.OK, reponse.StatusCode);
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
                    Id = Guid.NewGuid(),
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
                if (response.IsSuccessStatusCode)
                {
                    var orderResponse = response.Content.ReadAsStringAsync();
                    var deserializedOrderReponse = JsonConvert.DeserializeObject<Order>(orderResponse.Result);

                    var deleteReponse = await client.DeleteAsync($"/api/order/{deserializedOrderReponse.Id}");
                }

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }
        [Fact]
        public async Task UpdateSingle_Return204()
        {
            using (var client = new TestClientProvider().Client)
            {
                var testOrder = new Order
                {
                    Id = Guid.NewGuid(),
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
                //add order
                var serializedOrder = new StringContent(JsonConvert.SerializeObject(testOrder), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"/api/order", serializedOrder);
                if (response.IsSuccessStatusCode)
                {
                    //deserialize order that was returned
                    var orderResponse = response.Content.ReadAsStringAsync();
                    var deserializedOrderReponse = JsonConvert.DeserializeObject<Order>(orderResponse.Result);

                    //update order that was returned
                    var updatedOrder = deserializedOrderReponse;
                    updatedOrder.FirstName = "Max";
                    var serializedUpdatedOrder = new StringContent(JsonConvert.SerializeObject(updatedOrder), Encoding.UTF8, "application/json");


                    var putReponse = await client.PutAsync($"/api/order/{deserializedOrderReponse.Id}", serializedUpdatedOrder);
                    if (putReponse.IsSuccessStatusCode)
                    {
                        //remove order
                        var deleteReponse = await client.DeleteAsync($"/api/order/{deserializedOrderReponse.Id}");
                    }

                }
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }
    }
}
