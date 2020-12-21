using System;
using System.Collections.Generic;
using System.Linq;
using ClientesApi;
using Infrastructure.CoreServices.DataAccess;
using Infrastructure.DataModel.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FuncionalTests
{
    public class CommandsWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
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
                var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<SqlDbContext>));

                services.Remove(descriptor);

                services.AddDbContext<SqlDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                InjecaoDependencias(services);

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;

                    var logger = scopedServices
                        .GetRequiredService<ILogger<CommandsWebApplicationFactory<TStartup>>>();

                    try
                    {
                        var context = scopedServices.GetRequiredService<SqlDbContext>();
                        context.Clientes.Add(new ClienteDbModel { Id = Guid.NewGuid(), Nome = "Guilherme", SobreNome="Morais", Email = "guilherme.morais@teste.com" });
                        context.SaveChanges();
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
