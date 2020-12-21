using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ClientesApi.Controllers.Base;
using Commands.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Queries.Dto;
using Queries.Requests;

namespace ClientesApi.Controllers.v1
{
    [ApiController]
    [Route("v1/[controller]/[action]")]
    public class ClientesController : BaseController
    {
        private readonly ILogger<ClientesController> _logger;
        private readonly IMediator _mediator;
        public ClientesController(ILogger<ClientesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<ClienteDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ListarClientesTodosRequest(), cancellationToken);
            return result;
        }

        [HttpGet]
        public async Task<ClienteDto> GetbyIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ObterClientePorIdRequest() { Id = id }, cancellationToken);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CriarClienteCommand criarClienteCommand, CancellationToken cancellationToken)
        {
            criarClienteCommand.Validate();

            if (!criarClienteCommand.Valid)
            {
                return BadRequest(criarClienteCommand.Notifications);
            }

            return TratarRetorno(await _mediator.Send(criarClienteCommand, cancellationToken));
        }
    }
}
