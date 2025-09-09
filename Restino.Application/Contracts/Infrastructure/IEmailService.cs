using Restino.Application.DTOs.Mail;

namespace Restino.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
        Task<bool> SendPasswordResetCode(string email, string code);
        Task<bool> SendTwoFactorCode(string email, string code);
    }
}
