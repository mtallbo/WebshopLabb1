using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Webshop.OrderAPI.Models;
using Webshop.OrderAPI.Services;
using Xunit;
using System.Net.Http;


namespace Webshop.OrderAPI.Tests
{
    public class OrderServiceTest
    {
        private readonly TokenBearer _token;
        private readonly Order _testOrder;

        public OrderServiceTest()
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
                            Quantity = 1
                        }
                    }
            };
            _testOrder = testOrder;
            var user = new User
            {
                Email = "Test",
            };
            //get auth token to be able to do all the requests in this test
            using (var client = new TestClientProvider().Client)
            {
                var body = JsonConvert.SerializeObject(new { Email = "Test" });
                var content = new StringContent(body, System.Text.Encoding.UTF8, "application/json");

                var response = client.PostAsync("/user/authenticate", content);
                if (response.Result.IsSuccessStatusCode)
                {
                    var token = response.Result.Content.ReadAsStringAsync();
                    var deserializedToken = JsonConvert.DeserializeObject<TokenBearer>(token.Result);
                    _token = deserializedToken;
                }
            }
        }
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
        public async Task PostSingle_Return201()
        {
            using (var client = new TestClientProvider().Client)
            {
                var serializedOrder = new StringContent(JsonConvert.SerializeObject(_testOrder), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    Content = serializedOrder,
                    RequestUri = new Uri($"http://localhost:80/api/order")
                };
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token.Token);

                var response = await client.SendAsync(request);
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
        public async Task RemoveSingle_Return200()
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
                            Quantity = 1
                        }
                    }
                };

                var serializedOrder = new StringContent(JsonConvert.SerializeObject(testOrder), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    Content = serializedOrder,
                    RequestUri = new Uri($"http://localhost:80/api/order")
                };
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token.Token);

                var response = await client.SendAsync(request);
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
                var addRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    Content = serializedOrder,
                    RequestUri = new Uri($"http://localhost:80/api/order")
                };
                addRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token.Token);
                
                var postResponse = await client.SendAsync(addRequest);
                if (postResponse.IsSuccessStatusCode)
                {
                    //deserialize order that was returned
                    var orderResponse = postResponse.Content.ReadAsStringAsync();
                    var deserializedOrderReponse = JsonConvert.DeserializeObject<Order>(orderResponse.Result);

                    //update order that was returned
                    var updatedOrder = deserializedOrderReponse;
                    updatedOrder.FirstName = "Max";
                    var serializedUpdatedOrder = new StringContent(JsonConvert.SerializeObject(updatedOrder), Encoding.UTF8, "application/json");
                    var updaterequest = new HttpRequestMessage
                    {
                        Method = HttpMethod.Put,
                        Content = serializedUpdatedOrder,
                        RequestUri = new Uri($"http://localhost:80/api/order/" + deserializedOrderReponse.Id)
                    };
                    updaterequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token.Token);
                    var updateReponse = await client.SendAsync(updaterequest);

                    if (updateReponse.IsSuccessStatusCode)
                    {
                        //remove order
                        var deleteReponse = await client.DeleteAsync($"/api/order/{deserializedOrderReponse.Id}");
                    }
                    Assert.Equal(HttpStatusCode.OK, updateReponse.StatusCode);
                }
            }
        }
        
    }
}
