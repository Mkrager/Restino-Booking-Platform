using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restino.Application.Contracts.Identity;
using Restino.Domain.Entities;
using Restino.Identity.Models;

namespace Restino.Identity.Service
{
    public class UserTwoFactorService : IUserTwoFactorService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        protected readonly RestinoIdentityDbContext _dbContext;
        public UserTwoFactorService(UserManager<ApplicationUser> userManager, RestinoIdentityDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }
        public async Task<UserTwoFactor?> GetByUserIdAsync(string userId)
        {
            return await _dbContext.UserTwoFactors.FirstOrDefaultAsync(r => r.CreatedBy == userId);
        }

        public Task AddTwoFactorToAccountAsync(string userId)
        {

        }

        public Task DeleteTwoFactorFromAccountAsync(string userId)
        {
            throw new NotImplementedException();
        }

    }
}
