using Microsoft.EntityFrameworkCore;
using Restino.Application.Contracts.Identity;
using Restino.Domain.Entities;

namespace Restino.Identity.Repositories
{
    public class UserTwoFactorCodeRepository : BaseRepositrory<UserTwoFactorCode>, IUserTwoFactorCodeRepository
    {
        public UserTwoFactorCodeRepository(RestinoIdentityDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<UserTwoFactorCode?> GetByUserEmailAsync(string email)
        {
            return await _dbContext.UserTwoFactorCodes
                .FirstOrDefaultAsync(r => r.Email == email);
        }

        public async Task<UserTwoFactorCode?> GetByUserIdAsync(string userId)
        {
            return await _dbContext.UserTwoFactorCodes
                .FirstOrDefaultAsync(r => r.CreatedBy == userId);
        }
    }
}
