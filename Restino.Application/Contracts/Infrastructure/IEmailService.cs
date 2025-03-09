using Restino.Application.DTOs.Mail;

namespace Restino.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
