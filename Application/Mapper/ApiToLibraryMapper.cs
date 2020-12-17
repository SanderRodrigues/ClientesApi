using Application.Commands;
using AutoMapper;
using Domain.Entitys.Cliente;

namespace Application.Mapper
{
    public class ApiToLibraryMapper: Profile
    {
        public ApiToLibraryMapper()
        {
            CreateMap<CriarClienteCommand, Cliente>().ReverseMap();
        }
    }
}
