namespace Restino.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendPasswordResetCode(string email, string code);
        Task<bool> SendTwoFactorCode(string email, string code);
    }
}
