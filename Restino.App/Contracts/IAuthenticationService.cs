using Restino.App.Infrastructure.Api;
using Restino.App.ViewModels;
using Restino.App.ViewModels.Authenticate;

namespace Restino.App.Contracts
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<bool>> Authenticate(AuthenticateRequest request);
        //Task<ApiResponse<bool>> AuthenticateTwoFactor(string email, string code);
        Task<ApiResponse<bool>> Register(RegistrationRequest request);
        Task Logout();
    }
}
