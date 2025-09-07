using Restino.Domain.Entities;

namespace Restino.Application.Contracts.Identity
{
    public interface IUserTwoFactorService
    {
        Task<UserTwoFactor?> GetByUserIdAsync(string userId);
        Task AddTwoFactorToAccountAsync(UserTwoFactor userTwoFactor);
        Task DeleteTwoFactorFromAccountAsync(UserTwoFactor userTwoFactor);
        Task SendTwoFactorCodeAsync(string email);
    }
}
