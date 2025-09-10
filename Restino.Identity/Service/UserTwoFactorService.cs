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
        public async Task<UserTwoFactor?> GetByUserIdAsync(string userId)
        {
            return await _dbContext.UserTwoFactors.FirstOrDefaultAsync(r => r.CreatedBy == userId);
        }

        public async Task<UserTwoFactor> CreateTwoFactorRequestAsync(UserTwoFactor userTwoFactor)
        {
            await _dbContext.AddAsync(userTwoFactor);
            await _dbContext.SaveChangesAsync();
            return userTwoFactor;
        }
        public async Task DeleteTwoFactorRequestAsync(UserTwoFactor userTwoFactor)
        {
            _dbContext.Remove(userTwoFactor);
            await _dbContext.SaveChangesAsync();
        }
    }
}
