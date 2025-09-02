using Restino.Application.DTOs.Authentication;

namespace Restino.Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest response);
        Task<string> RegisterAsync(RegistrationRequest response);
        Task<AuthenticationResponse> VerifyTwoFactorCodeAsync(VerifyCodeRequest response);
    }
}
