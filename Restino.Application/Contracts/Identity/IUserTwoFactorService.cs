using Restino.Domain.Entities;

namespace Restino.Application.Contracts.Identity
{
    public interface IUserTwoFactorService
    {
        Task<UserTwoFactorCode?> GetByUserIdAsync(string userId);
        Task<UserTwoFactorCode> CreateTwoFactorRequestAsync(UserTwoFactorCode userTwoFactor);
        Task DeleteTwoFactorRequestAsync(UserTwoFactorCode userTwoFactor);
    }
}
