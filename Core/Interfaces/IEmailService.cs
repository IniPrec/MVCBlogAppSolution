using Domain.Entities;

namespace Application.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailNotification emailNotification);
    }
}
