using System.Net.Http;
using System.Threading.Tasks;
using ClientesApi;
using Xunit;

namespace FuncionalTests.Controllers
{
    public class ClientControllerTest: IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ClientControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetClientsSucess()
        {
            var response = await _client.GetAsync("/Cliente");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Contains("Version", stringResponse);
            Assert.Contains("Last Updated", stringResponse);
        }
    }
}
