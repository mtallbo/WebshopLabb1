using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
    }
}
