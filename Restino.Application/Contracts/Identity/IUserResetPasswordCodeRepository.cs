using Restino.Domain.Entities;

namespace Restino.Application.Contracts.Identity
{
    public interface IUserResetPasswordCodeRepository : IAsyncIdentityRepository<UserResetPasswordCode>
    {
        Task<UserResetPasswordCode?> GetByEmailAsync(string email);
    }
}
