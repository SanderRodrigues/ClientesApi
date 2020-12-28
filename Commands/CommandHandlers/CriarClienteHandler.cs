using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Commands.Commands;
using CrossCutting.Command;
using Domain.Entitys.Cliente;
using MediatR;

namespace Commands.CommandHandlers
{
    public class CriarClienteHandler: CommandHandler<CriarClienteCommand>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CriarClienteHandler(IMapper mapper, IClienteRepository clienteRepository, IMediator mediator)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public override async Task<CommandResult> Handle(CriarClienteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(request);
                _ = await _clienteRepository.InsertAsync(cliente, cancellationToken);
                cliente.RaiseEvents(_mediator);
                return CommandResult.Ok(cliente.Id.ToString(), CommandResultResourceAction.Created);
            }
            catch (Exception ex)
            {
                return CommandResult.Error(ex.Message);
            }
        }
    }
}
