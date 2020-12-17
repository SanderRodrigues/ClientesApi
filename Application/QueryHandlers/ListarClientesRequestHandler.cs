using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dto;
using Application.Querys;
using Domain.Entitys.Cliente;
using MediatR;

namespace Application.QueryHandlers
{
    public class ListarClientesRequestHandler : IRequestHandler<ListarClientesRequest, IEnumerable<ClienteDto>>
    {
        private readonly IClienteRepository _clienteRepository;

        public ListarClientesRequestHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<ClienteDto>> Handle(ListarClientesRequest request, CancellationToken cancellationToken)
        {
            var clientes = await _clienteRepository.ListarClientesAsync();
            return clientes.Select(a => new ClienteDto() { Id = a.Id, NomeCompleto = $"{a.Nome} {a.SobreNome}", Email = a.Email });
        }
    }
}
