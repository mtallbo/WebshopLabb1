using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public OrderService(HttpClient client)
        {
            _httpClient = client;
        }
        public async Task<Order> CreateOrder(CartViewModel cartvm)
        {

            var orderMapped = MapCartToOrder(cartvm);
            var orderJSON = JsonConvert.SerializeObject(orderMapped);

            var orderContent = new StringContent(orderJSON, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/order/add", orderContent);
            var orderResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Order>(orderResponse);
        }

        public async Task<Order> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"order/{id}");
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
    }
}
