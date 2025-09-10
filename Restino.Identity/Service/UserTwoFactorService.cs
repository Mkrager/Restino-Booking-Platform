using Microsoft.EntityFrameworkCore;
using Restino.Application.Contracts.Identity;
using Restino.Domain.Entities;

namespace Restino.Identity.Service
{
    public class UserTwoFactorService : IUserTwoFactorService
    {
        private readonly RestinoIdentityDbContext _dbContext;
        public UserTwoFactorService(RestinoIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UserTwoFactorCode?> GetByUserIdAsync(string userId)
        {
            return await _dbContext.UserTwoFactorCodes.FirstOrDefaultAsync(r => r.CreatedBy == userId);
        }

        public async Task<UserTwoFactorCode> CreateTwoFactorRequestAsync(UserTwoFactorCode userTwoFactor)
        {
            await _dbContext.AddAsync(userTwoFactor);
            await _dbContext.SaveChangesAsync();
            return userTwoFactor;
        }
        public async Task DeleteTwoFactorRequestAsync(UserTwoFactorCode userTwoFactor)
        {
            _dbContext.Remove(userTwoFactor);
            await _dbContext.SaveChangesAsync();
        }
    }
}
