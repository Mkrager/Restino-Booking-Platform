using Restino.Application.DTOs.Authentication;

namespace Restino.Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest response);
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest response);
        Task<AuthenticationResponse> VerifyTwoFactorCodeAsync(VerifyCodeRequest response);
    }
}
