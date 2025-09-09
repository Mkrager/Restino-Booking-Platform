using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restino.Application.Contracts.Identity;
using Restino.Application.Contracts.Infrastructure;
using Restino.Application.DTOs.Mail;
using Restino.Domain.Entities;
using Restino.Identity.Models;

namespace Restino.Identity.Service
{
    public class UserTwoFactorService : IUserTwoFactorService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RestinoIdentityDbContext _dbContext;
        private readonly ICodeGeneratorService _codeGeneratorService;
        private readonly IEmailService _emailService;
        public UserTwoFactorService(
            UserManager<ApplicationUser> userManager, 
            RestinoIdentityDbContext dbContext,
            ICodeGeneratorService codeGeneratorService,
            IEmailService emailService)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _codeGeneratorService = codeGeneratorService;
            _emailService = emailService;
        }
        public async Task<UserTwoFactor?> GetByUserIdAsync(string userId)
        {
            return await _dbContext.UserTwoFactors.FirstOrDefaultAsync(r => r.CreatedBy == userId);
        }

        public async Task SendTwoFactorCodeAsync(string email)
        {
            var code = _codeGeneratorService.GenerateCode();

            var emailContent = $"Your two-factor authentication code is {code}. It will expire in 10 minutes.";

            var emailToSend = new Email
            {
                To = email,
                Subject = "Two-factor authentication code",
                Body = emailContent
            };

            await _emailService.SendEmail(emailToSend);
        }

        public async Task AddTwoFactorToAccountAsync(UserTwoFactor userTwoFactor)
        {
            await _dbContext.UserTwoFactors.AddAsync(userTwoFactor);
        }

        public Task DeleteTwoFactorFromAccountAsync(UserTwoFactor userTwoFactor)
        {
            throw new NotImplementedException();
        }
    }
}
