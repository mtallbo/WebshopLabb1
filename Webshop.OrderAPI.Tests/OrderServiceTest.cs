using System;
using System.Collections.Generic;
using System.Net;
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
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
        [Fact]
        public async Task GetSingleRandomGuid_Return200()
        {
            using (var client = new TestClientProvider().Client)
            {
                Guid g = Guid.NewGuid();
                var response = await client.GetAsync($"/api/order/{g}");
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
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
        //--
        //[Fact]
        //public async Task PostSingle_Return200()
        //{
        //    using (var client = new TestClientProvider().Client)
        //    {
        //        var testProduct = new Order
        //        {
        //            Id = Guid.Parse("7d3e444b-5dc3-453e-b5cf-c65caf744ca4"),
        //            Adress = "Testgatan",
        //            City = "Sthlm",
        //            Email = "test@test.se",
        //            FirstName = "Mikael",
        //            LastName = "Tallbo",
        //            PhoneNumber = "123 123 123",
        //            PostalCode = "174 43",
        //            OrderItems = new List<OrderItem>()
        //            {
        //                new OrderItem()
        //                {
        //                    Name = "1080RTX",
        //                    OrderId
        //                }
        //            }
        //        };

        //        var serializedProduct1 = new StringContent(JsonConvert.SerializeObject(testProduct), Encoding.UTF8, "application/json");
        //        var response = await client.PostAsync($"/api/product", serializedProduct1);

        //        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        //    }
        //}
        //[Fact]
        //public async Task UpdateSingle_Return204()
        //{
        //    //id to remove
        //    var id = "df46fa43-2b5f-460b-832e-a611e57aeec6";
        //    using (var client = new TestClientProvider().Client)
        //    {
        //        //get product deserialized
        //        var productResponse = await client.GetAsync($"/api/product/{id}");
        //        var content = await productResponse.Content.ReadAsStringAsync();

        //        var deserializedProduct = JsonConvert.DeserializeObject<Product>(content);

        //        //update product
        //        deserializedProduct.Name = "forsenCD";

        //        //serialize product
        //        var serializedProduct = new StringContent(JsonConvert.SerializeObject(deserializedProduct), Encoding.UTF8, "application/json");

        //        var response = await client.PutAsync($"/api/product/{id}", serializedProduct);
        //        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //    }
        //}

        //[Fact]
        //public async Task RemoveSingle_Return204()
        //{
        //    //id to remove
        //    var id = "df46fa43-2b5f-460b-832e-a611e57aeec6";
        //    using (var client = new TestClientProvider().Client)
        //    {
        //        var response = await client.DeleteAsync($"/api/product/{id}");
        //        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        //    }
        //}
    }
}
