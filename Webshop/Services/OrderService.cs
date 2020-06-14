using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Webshop.Models;
using Webshop.ViewModels;

namespace Webshop.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public OrderService(HttpClient client, IConfiguration configuration)
        {
            _httpClient = client;
            _configuration = configuration;
        }
        public async Task<Order> CreateOrder(CartViewModel cartvm, TokenBearer token)
        {
            var orderMapped = MapCartToOrder(cartvm);
            var orderJSON = JsonConvert.SerializeObject(orderMapped);

            var orderContent = new StringContent(orderJSON, System.Text.Encoding.UTF8, "application/json");
            
            //construct request with token
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = orderContent,
                RequestUri = new Uri(_configuration["APIGatewayUrl"] + "/order/add")
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);

            var response = await _httpClient.SendAsync(request);
            var orderResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Order>(orderResponse);
        }

        public async Task<Order> GetById(Guid id, TokenBearer token)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_configuration["APIGatewayUrl"] + "/order/" + id)
            };

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var orderresponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Order>(orderresponse);
        }

        public Order MapCartToOrder(CartViewModel cart)
        {
            return new Order
            {
                OrderItems = cart.CartProducts.Select(cp => new OrderProduct
                {
                    Name = cp.Product.Name,
                    Price = cp.Product.Price,
                    Quantity = cp.Quantity
                }).ToList(),
                FirstName = cart.User.FirstName,
                LastName = cart.User.LastName,
                City = cart.User.City,
                PostalCode = cart.User.PostalCode,
                Adress = cart.User.Adress,
                PhoneNumber = cart.User.PhoneNumber,
                Email = cart.User.Email
            };
        }

        public async Task<TokenBearer> GetToken(string email)
        {

            var body = JsonConvert.SerializeObject(new { Email = email});
            var content = new StringContent(body, System.Text.Encoding.UTF8, "application/json");

            var registerResponse = await _httpClient.PostAsync($"/user/register", content);
            if (registerResponse.IsSuccessStatusCode)
            {
                var authReponse = await _httpClient.PostAsync($"/user/authenticate", content);
                if (authReponse.IsSuccessStatusCode)
                {
                    var token = await authReponse.Content.ReadAsStringAsync();
                    var deserializedToken = JsonConvert.DeserializeObject<TokenBearer>(token);
                    return deserializedToken;
                }
            }
            return null;
        }
    }
}
