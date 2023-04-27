using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RatingService.Dtos;

namespace RatingService.SyncDataServices.Http
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

        public async Task SendRatingToNotification(RatingReadDto rating)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(rating),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(_configuration["NotificationService"], httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to NotificationService was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync POST to NotificationService was NOT OK!");
            }
        }
    }
}