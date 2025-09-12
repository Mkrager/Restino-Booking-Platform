using Restino.Domain.Entities;

namespace Restino.Application.Contracts.Identity
{
    public interface IUserTwoFactorCodeRepository : IAsyncIdentityRepository<UserTwoFactorCode>
    {
        Task<UserTwoFactorCode?> GetByUserIdAsync(string userId);
        Task<UserTwoFactorCode?> GetByUserEmailAsync(string email);
    }
}