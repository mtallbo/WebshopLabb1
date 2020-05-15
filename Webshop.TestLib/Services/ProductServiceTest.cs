using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Webshop.TestLib.Services
{
    public class ProductServiceTest
    {
        [Fact]
        public async Task GetAll_Return200()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/product");
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
                var response = await client.GetAsync($"/api/product/{g}");
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
                var response = await client.GetAsync($"/api/product/123");
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
    }
}
