using System.Threading;
using System.Threading.Tasks;
using Application.Dto;
using Application.Querys;
using AutoMapper;
using Domain.Entitys.Cliente;
using MediatR;

namespace Application.QueryHandlers
{
    public class ObterClientePorIdRequestHandler : IRequestHandler<ObterClientePorIdRequest, ClienteDto>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ObterClientePorIdRequestHandler(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<ClienteDto> Handle(ObterClientePorIdRequest request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(request.Id.ToString());
            return _mapper.Map<ClienteDto>(cliente);
        }
    }
}
