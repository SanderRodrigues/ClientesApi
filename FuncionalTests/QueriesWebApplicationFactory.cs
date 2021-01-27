using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using ClientesApi;
using Infrastructure.CoreServices.DataAccess;
using Infrastructure.DataModel.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FuncionalTests
{
    public class QueriesWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        public static List<KeyValuePair<string, string>> ParametrosConfig = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("ASPNETCORE_ENVIRONMENT", "Desenvolvimento")
            };

        private SqliteConnection _connection;

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
                    options.UseSqlite(CreateInMemoryDatabase());
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
                        context.Database.EnsureCreated();
                        context.Clientes.Add(new ClienteDbModel { Id = Guid.Parse("80b38fc1-a6a2-4ac5-8b5c-749e4c262ff7"), Nome = "Guilherme", SobreNome="Morais", Email = "guilherme.morais@teste.com" });
                        context.Clientes.Add(new ClienteDbModel { Id = Guid.Parse("93103eca-f133-4263-b751-7c739b7a4ec6"), Nome = "Ana", SobreNome = "Maria", Email = "ana.maria@teste.com" });
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

        private DbConnection CreateInMemoryDatabase()
        {
            _connection = new SqliteConnection("DataSource=file::memory:?cache=shared");

            _connection.Open();

            return _connection;
        }

        private static void InjecaoDependencias(IServiceCollection services)
        {
        }

        protected override void Dispose(bool disposing)
        {
            _connection.Close();
            _connection.Dispose();
            base.Dispose(disposing);
        }
    }
}
