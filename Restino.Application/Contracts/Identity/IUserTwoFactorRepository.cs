using Restino.Domain.Entities;

namespace Restino.Application.Contracts.Identity
{
    public interface IUserTwoFactorRepository : IAsyncIdentityRepository<UserTwoFactorCode>
    {
        Task<UserTwoFactorCode?> GetByUserIdAsync(string userId);
    }
}
