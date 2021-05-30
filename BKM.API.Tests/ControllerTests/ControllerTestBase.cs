
using BKM.API.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BKM.API.Tests
{
    public class ControllerTestBase
    {
        protected HttpClient _client;
        protected void ConfigureServer()
        {
            var server = new TestServer(new WebHostBuilder()
               .ConfigureAppConfiguration((hostingContext, config) =>
               {
                   config.AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true);
               })
               .UseEnvironment("Developmenet")
               .UseStartup<Startup>());
            _client = server.CreateClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenHelper.GetToken());
        }

    }
}