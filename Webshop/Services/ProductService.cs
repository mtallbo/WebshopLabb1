using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Webshop.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Webshop.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpclient)
        {
            _httpClient = httpclient;
        }

        public async Task<List<Product>> GetAll()
        {
            var response = await _httpClient.GetAsync("product/");
            var productResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Product>>(productResponse);
        }

        public async Task<Product> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"product/{id}");
            var productResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Product>(productResponse);
        }
    }
}
