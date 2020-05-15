using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Webshop.OrderAPI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Webshop.OrderAPI.Data;
using Microsoft.EntityFrameworkCore;
using Webshop.OrderAPI.Data;

namespace Webshop.OrderAPI.Tests
{
    class TestClientProvider
    {
        public TestServer Server { get; private set; }
        public HttpClient Client { get; private set; }
        public TestClientProvider()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            WebHostBuilder webHostBuilder = new WebHostBuilder();
            webHostBuilder.ConfigureServices(s => s.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))));

            //Make sure startup is referring to correct dependency
            webHostBuilder.UseStartup<Startup>();

            Server = new TestServer(webHostBuilder);

            Client = Server.CreateClient();
        }

        public void Dispose()
        {
            Server?.Dispose();
            Client?.Dispose();
        }
    }
}
