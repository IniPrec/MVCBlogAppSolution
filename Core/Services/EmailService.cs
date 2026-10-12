using Application.Interfaces;
using Application.Interfaces.DTO;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(HttpClient httpClient, IConfiguration configuration, ILogger<EmailService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(EmailRequest emailRequest)
        {
            var baseUrl = _configuration["NotificationBaseURL"];

            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{baseUrl}/api/Notify/SendSingleMail", emailRequest);
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("Email API returned {StatusCode}: {Error}", response.StatusCode, error);
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sending email to {Receiver}", emailRequest.Reciever);
                return false;
            }
        }
    }
}
