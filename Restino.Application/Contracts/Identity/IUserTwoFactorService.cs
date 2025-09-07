using Restino.Domain.Entities;

namespace Restino.Application.Contracts.Identity
{
    public interface IUserTwoFactorService
    {
        Task<UserTwoFactor> GetByUserId(string userId);
        Task AddTwoFactorToAccount(string userId);
        Task DeleteTwoFactorFromAccount(string userId);
    }
}
