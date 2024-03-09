using System.Text;
using System.Text.Json;
using ProductionService.Dtos;
using ProductionService.Interfaces;

namespace ProductionService.SyncDataServices.Http
{
    public class HttpOrderDataClient : IOrderDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpOrderDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendProductToOrder(ProductReadDto prd)
        {
            var httpContent = new StringContent( 
                JsonSerializer.Serialize(prd),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["OrderService"]}",httpContent);    
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("---> Sync post to order service was ok.");
            }        
            else
            {
                Console.WriteLine("---> Sync post to order service was NOT ok.");
            }
        }
    }
}