using Restino.Application.DTOs.Authentication;

namespace Restino.Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<string> GenerateJwtForUserAsync(string email);
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest response);
        Task<string> RegisterAsync(RegistrationRequest response);
    }
}
