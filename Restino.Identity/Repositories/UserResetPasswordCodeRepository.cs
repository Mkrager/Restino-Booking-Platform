using Microsoft.EntityFrameworkCore;
using Restino.Application.Contracts.Identity;
using Restino.Domain.Entities;

namespace Restino.Identity.Repositories
{
    public class UserResetPasswordCodeRepository : BaseRepositrory<UserResetPasswordCode>, IUserResetPasswordCodeRepository
    {
        public UserResetPasswordCodeRepository(RestinoIdentityDbContext dbcontext) : base(dbcontext)
        {
        }
        public async Task<UserResetPasswordCode?> GetByEmailAsync(string email)
        {
            return await _dbContext.UserResetPasswordCodes
                .FirstOrDefaultAsync(r => r.Email == email);
        }
    }
}
