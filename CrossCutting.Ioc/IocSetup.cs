using System;
using AutoMapper;
using Domain.Entitys.Cliente;
using Infrastructure.CoreServices.DataAccess;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCuttingIoc
{
    public static class IocSetup
    {
        public static void ConfigurarIoc(IServiceCollection services)
        {
            AdicionaUtilitarios(services);
            AdicionarDB(services);
            AdicionarInjecoes(services);
        }

        private static void AdicionaUtilitarios(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.Load("Infrastructure"), AppDomain.CurrentDomain.Load("Application"));
            services.AddMediatR(AppDomain.CurrentDomain.Load("Application"));
        }

        private static void AdicionarDB(IServiceCollection services)
        {
            services.AddDbContext<SqlDbContext>(x => x.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CadastroClientes;Integrated Security=True"));
            services.AddDatabaseDeveloperPageExceptionFilter();
            var sp = services.BuildServiceProvider();
            var sqlDbContext = sp.GetRequiredService<SqlDbContext>();
            sqlDbContext.Database.EnsureCreated();
        }

        private static void AdicionarInjecoes(IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
        }
    }
}
