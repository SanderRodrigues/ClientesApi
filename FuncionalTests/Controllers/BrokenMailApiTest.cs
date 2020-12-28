using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientesApi;
using Commands.Commands;
using Domain.Entitys.Cliente;
using FluentAssertions;
using FuncionalTests.Util;
using Infrastructure.CoreServices.EmailSend;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace FuncionalTests.Controllers
{
    public class BrokenMailApiTest : IClassFixture<CommandsWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly CommandsWebApplicationFactory<Startup> _factory;

        public BrokenMailApiTest(CommandsWebApplicationFactory<Startup> factory)
        {

            _httpClient = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(ConfiguraMockEmailApiError().Object);
                });
            }).CreateClient();
            _httpClient.Timeout = TimeSpan.FromMinutes(5);
            _factory = factory;
        }

        [Fact]
        public async Task CreateClientsEmailSendError()
        {
            var objToPost = new CriarClienteCommand() { Nome = "João", SobreNome = "Almeida", Email = "joao.almeida@teste.com" };
            var body = new StringContent(JsonSerializer.Serialize(objToPost), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("v1/Clientes/Create", body);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var objResponse = JsonSerializer.Deserialize<CreatedResponse>(stringResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            stringResponse.Should().NotBeNullOrEmpty();
            objResponse.Should().NotBeNull();
            objResponse.ResourceId.Should().NotBeEmpty();


            using (var scope = _factory.Services.CreateScope())
            {
                IClienteRepository _clienteRepository = scope.ServiceProvider.GetRequiredService<IClienteRepository>();
                var cliente = await _clienteRepository.ObterPorIdAsync(objResponse.ResourceId.ToString());
                cliente.EmailConfirmacaoEnviado.Should().BeFalse();
                cliente.MensagemErro.Should().NotBeNullOrEmpty();
            }
        }

        private Mock<IEnvioEmailSevice> ConfiguraMockEmailApiError()
        {
            var brokenMailApiMock = new Mock<IEnvioEmailSevice>();
            brokenMailApiMock.Setup(a => a.EnviarEmail(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception("Erro ao requisitar MailApi."));
            return brokenMailApiMock;
        }
    }
}
