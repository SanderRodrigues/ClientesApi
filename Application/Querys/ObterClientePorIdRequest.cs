using System;
using Application.Dto;
using MediatR;

namespace Application.Querys
{
    public class ObterClientePorIdRequest: IRequest<ClienteDto>
    {
        public Guid Id { get; set; }
    }
}
