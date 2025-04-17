using MediatR;
using Restino.Application.DTOs.Authentication;

namespace Restino.Application.Features.Accounts.Queries.AuthenticationVerifyTwoFactorCode
{
    public class AuthenticationVerifyTwoFactorCodeQuery : IRequest<AuthenticationResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string TwoFactorCode { get; set; } = string.Empty;
    }
}

