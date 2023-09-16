using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendPlatformToCommand(PlatformReadDto dto)
        {
            var httpContent = new StringContent(
                               JsonSerializer.Serialize(dto),
                                              System.Text.Encoding.UTF8,
                                                             "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                System.Console.WriteLine("--> Sync POST to Command Service was OK!");
            }
            else
            {
                System.Console.WriteLine("--> Sync POST to Command Service was NOT OK!");
            }
        }
    }
}
