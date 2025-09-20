using Restino.App.Infrastructure.Api;
using Restino.App.ViewModels;

namespace Restino.App.Contracts
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<LoginRequest>> Authenticate(string email, string password);
        Task<ApiResponse<LoginRequest>> AuthenticateTwoFactor(string email, string code);
        Task<ApiResponse<bool>> Register(string firstName, string lastName, string userName, string email, string password);
        Task Logout();
    }
}
