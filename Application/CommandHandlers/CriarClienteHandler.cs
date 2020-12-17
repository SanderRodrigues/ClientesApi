using System.Threading;
using System.Threading.Tasks;
using Application.Commands;
using AutoMapper;
using CrossCutting.Command;
using Domain.Entitys.Cliente;

namespace Application.CommandHandlers
{
    public class CriarClienteHandler: CommandHandler<CriarClienteCommand>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public CriarClienteHandler(IMapper mapper, IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public override async Task<CommandResult> Handle(CriarClienteCommand request, CancellationToken cancellationToken)
        {
            var retorno = await _clienteRepository.InsertAsync(_mapper.Map<Cliente>(request), cancellationToken);
            return CommandResult.Ok(retorno.Id.ToString(), CommandResultResourceAction.Created);
        }
    }
}
