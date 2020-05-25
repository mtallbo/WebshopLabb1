using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Webshop.ProductAPI.Models;

using Xunit;

namespace Webshop.ProductAPI.Tests
{
    public class ProductServiceTest : IClassFixture<ProductFixture>
    {
        ProductFixture _fixture;
        public ProductServiceTest(ProductFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData("/api/product")]
        public async Task GetAllEndpoints_Return200(string endpoint)
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync(endpoint);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
        [Fact]
        public async Task GetSingleRandomGuid_Return400()
        {
            using (var client = new TestClientProvider().Client)
            {
                Guid g = Guid.NewGuid();
                var response = await client.GetAsync($"/api/product/{g}");
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
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
        [Fact]
        public async Task GetInsertAndRemoveFixture_Return200()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync($"/api/product/{_fixture.product.Id}");
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
