using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands;
using Application.Dto;
using Application.Querys;
using ClientesApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public async Task<IEnumerable<ClienteDto>> Get()
        {
            var result = await _mediator.Send(new ListarClientesRequest());
            return result;
        }

        [HttpGet]
        public async Task<ClienteDto> GetbyId(Guid id)
        {
            var result = await _mediator.Send(new ObterClientePorIdRequest() { Id = id });
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CriarClienteCommand criarClienteCommand, CancellationToken cancellationToken)
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
