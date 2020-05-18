using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Webshop.Models;

namespace Webshop.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        public OrderService(HttpClient client)
        {
            _httpClient = client;
        }
        public async Task CreateOrder(Order order)
        {
            var orderContent = new StringContent(JsonConvert.SerializeObject(order), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/order/add/", orderContent);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Order> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"order/{id}");
            response.EnsureSuccessStatusCode();
            var orderresponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Order>(orderresponse);
        }
    }
}
