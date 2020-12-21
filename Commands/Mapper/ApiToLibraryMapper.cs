using Commands.Commands;
using AutoMapper;
using Domain.Entitys.Cliente;

namespace Commands.Mapper
{
    public class ApiToLibraryMapper: Profile
    {
        public ApiToLibraryMapper()
        {
            CreateMap<CriarClienteCommand, Cliente>().ReverseMap();
        }
    }
}
