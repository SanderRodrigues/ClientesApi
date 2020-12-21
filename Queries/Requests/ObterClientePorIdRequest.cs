using System;
using MediatR;
using Queries.Dto;

namespace Queries.Requests
{
    public class ObterClientePorIdRequest: IRequest<ClienteDto>
    {
        public Guid Id { get; set; }
    }
}
