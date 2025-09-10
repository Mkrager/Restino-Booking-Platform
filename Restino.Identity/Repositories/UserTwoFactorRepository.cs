using Microsoft.EntityFrameworkCore;
using Restino.Application.Contracts.Identity;
using Restino.Domain.Entities;

namespace Restino.Identity.Repositories
{
    public class UserTwoFactorRepository : BaseRepositrory<UserTwoFactorCode>, IUserTwoFactorRepository
    {
        public UserTwoFactorRepository(RestinoIdentityDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<UserTwoFactorCode?> GetByUserIdAsync(string userId)
        {
            return await _dbContext.UserTwoFactorCodes.FirstOrDefaultAsync(r => r.CreatedBy == userId);
        }
    }
}
