using Restino.Domain.Entities;

namespace Restino.Application.Contracts.Identity
{
    public interface IUserTwoFactorService
    {
        Task<UserTwoFactor?> GetByUserIdAsync(string userId);
        Task<UserTwoFactor> CreateTwoFactorRequestAsync(UserTwoFactor userTwoFactor);
        Task DeleteTwoFactorRequestAsync(UserTwoFactor userTwoFactor);
    }
}
