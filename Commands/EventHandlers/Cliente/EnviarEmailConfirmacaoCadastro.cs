using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entitys.Cliente;
using Domain.Events;
using Infrastructure.CoreServices.EmailSend;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Commands.EventHandlers.Cliente
{
    public class EnviarEmailConfirmacaoCadastro : INotificationHandler<ClienteCriado>
    {
        private readonly IEnvioEmailSevice _envioEmailSevice;
        private readonly IClienteRepository _clienteRepository;
        private readonly ILogger _log;

        public EnviarEmailConfirmacaoCadastro(IEnvioEmailSevice envioEmailSevice, IClienteRepository clienteRepository, ILogger<EnviarEmailConfirmacaoCadastro> log)
        {
            _envioEmailSevice = envioEmailSevice;
            _clienteRepository = clienteRepository;
            _log = log;
        }

        public async Task Handle(ClienteCriado notification, CancellationToken cancellationToken)
        {
            Domain.Entitys.Cliente.Cliente cliente = null;

            try
            {
                cliente = await _clienteRepository.ObterPorIdAsync(notification.Id.ToString());
                await _envioEmailSevice.EnviarEmail(notification.Email, "Cadastro realizado com sucesso.");
                cliente.SinalizarEmailConfirmacaoEnviado();
                await _clienteRepository.UpdateAsync(cliente);
            }
            catch(Exception ex)
            {
                _log.LogError(ex.Message);

                if(cliente != null)
                {
                    cliente.SinalizarErroEnvioEmailConfirmacao(ex.Message);
                    await _clienteRepository.UpdateAsync(cliente);
                }
            }
        }
    }
}
