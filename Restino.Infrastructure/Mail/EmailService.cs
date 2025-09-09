using Microsoft.Extensions.Options;
using Restino.Application.Contracts.Infrastructure;
using Restino.Application.DTOs.Mail;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace Restino.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettings { get; }
        public EmailService(IOptions<EmailSettings> mailSettings)
        {
            _emailSettings = mailSettings.Value;
        }

        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendGridMessage);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

        public async Task<bool> SendPasswordResetCode(string email, string code)
        {
            var emailToSend = new Email
            {
                To = email,
                Subject = "Password Reset Code",
                Body = $"Your password reset code: {code}"
            };

            return await SendEmail(emailToSend);
        }

        public async Task<bool> SendTwoFactorCode(string email, string code)
        {
            var emailToSend = new Email
            {
                To = email,
                Subject = "Two-factor authentication code",
                Body = $"Your two-factor authentication code is {code}. It will expire in 10 minutes.""
            };

            return await SendEmail(emailToSend);
        }
    }
}

