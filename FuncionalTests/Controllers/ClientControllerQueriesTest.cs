using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ClientesApi;
using FluentAssertions;
using Queries.Dto;
using Xunit;

namespace FuncionalTests.Controllers
{
    public class ClientControllerQueriesTest : IClassFixture<QueriesWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ClientControllerQueriesTest(QueriesWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllClientsSucess()
        {
            var response = await _client.GetAsync("v1/Clientes/GetAll");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var objResponse = JsonSerializer.Deserialize<IEnumerable<ClienteDto>>(stringResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            stringResponse.Should().NotBeNullOrEmpty();
            objResponse.Should().NotBeNull();
            objResponse.Should().HaveCount(2);
        }
    }
}
