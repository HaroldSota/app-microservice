using AppWeather.Api;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;

namespace AppWeather.Tests.Infrastructure
{
    public sealed class TestServerFixture : IDisposable
    {
        private static readonly TestServer TestServer;

        static TestServerFixture()
        {
            var builder = new WebHostBuilder()
               .ConfigureServices(services => services.AddAutofac())
                .UseEnvironment("Testing")
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Tests.json"));
                })
                .UseStartup<Startup>();

            TestServer = new TestServer(builder);

        }

        public TestServerFixture()
        {
            Client = TestServer.CreateClient();
        }

        public HttpClient Client { get; private set; }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}