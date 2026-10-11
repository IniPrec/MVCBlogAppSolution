using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Application.Services
{
    public class EmailService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public EmailService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(EmailNotification emailNotification)
        {
            var baseUrl = _configuration["NotificationBaseURL"];

            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{baseUrl}/api/Notify/SendSingleMail", emailNotification);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
