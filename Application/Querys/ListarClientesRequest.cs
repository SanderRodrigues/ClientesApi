using System.Collections.Generic;
using Application.Dto;
using MediatR;
namespace Application.Querys
{
    public class ListarClientesRequest : IRequest<IEnumerable<ClienteDto>>
    {
    }
}
