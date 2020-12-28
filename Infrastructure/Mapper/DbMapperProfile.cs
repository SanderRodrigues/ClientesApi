using AutoMapper;
using Domain.Entitys.Cliente;
using Infrastructure.DataModel.Models;

namespace Infrastructure.Mapper
{
    public class DbMapperProfile:Profile
    {
        public DbMapperProfile()
        {
            CreateMap<ClienteDbModel, Cliente>().ConstructUsing(a => new Cliente(a.Id, a.Nome, a.SobreNome, a.Email)).ReverseMap();
        }
    }
}
