using System;
using System.Collections.Generic;
using System.Text;
using ClientesApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace FuncionalTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        public static List<KeyValuePair<string, string>> ParametrosConfig = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("ASPNETCORE_ENVIRONMENT", "Desenvolvimento")
            };

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
            .ConfigureAppConfiguration(a => a.AddInMemoryCollection(ParametrosConfig))
            .ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                InjecaoDependencias(services);

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;

                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    try
                    {
                        // Seed the database with test data.
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            $"database with test messages. Error: {ex.Message}");
                    }
                }
            });
            
        }

        private static void InjecaoDependencias(IServiceCollection services)
        {
        }
    }
}
