using System.Collections.Generic;
using MediatR;
using Queries.Dto;

namespace Queries.Requests
{
    public class ListarClientesTodosRequest : IRequest<IEnumerable<ClienteDto>>
    {
    }
}
