using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientesApi;
using Commands.Commands;
using FluentAssertions;
using FuncionalTests.Util;
using Xunit;

namespace FuncionalTests.Controllers
{
    public class ClientControllerCommandsTest: IClassFixture<CommandsWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ClientControllerCommandsTest(CommandsWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateClientsSucess()
        {
            var objToPost = new CriarClienteCommand() {Nome = "João", SobreNome = "Almeida", Email = "joao.almeida@teste.com" };
            var body = new StringContent(JsonSerializer.Serialize(objToPost), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("v1/Clientes/Create", body);
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var objResponse = JsonSerializer.Deserialize<CreatedResponse>(stringResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});

            stringResponse.Should().NotBeNullOrEmpty();
            objResponse.Should().NotBeNull();
            objResponse.ResourceId.Should().NotBeEmpty();
        }
    }
}
