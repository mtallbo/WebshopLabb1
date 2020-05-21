using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using Ocelot;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Webshop.APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args);

            builder.ConfigureServices(s => s.AddSingleton(builder).AddOcelot())
                    .ConfigureAppConfiguration(ic => ic.AddJsonFile("ocelot.json"))
                    .UseStartup<Startup>()
                    .UseIISIntegration()
                    .Configure(app => app.UseOcelot().Wait());
            
            var host = builder.Build();
            return host;
        }
    }
}
