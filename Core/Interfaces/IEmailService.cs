using Application.Interfaces.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailRequest emailRequest);
    }
}
