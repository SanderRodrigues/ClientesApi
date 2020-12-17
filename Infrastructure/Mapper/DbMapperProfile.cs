using AutoMapper;
using Domain.Entitys.Cliente;
using Infrastructure.DataModel.Models;

namespace Infrastructure.Mapper
{
    public class DbMapperProfile:Profile
    {
        public DbMapperProfile()
        {
            CreateMap<ClienteDbModel, Cliente>().ReverseMap();
        }
    }
}
