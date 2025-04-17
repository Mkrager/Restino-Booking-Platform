using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Identity;
using Restino.Application.DTOs.Authentication;

namespace Restino.Application.Features.Accounts.Queries.AuthenticationVerifyTwoFactorCode
{
    public class AuthenticationVerifyTwoFactorCodeQueryHandler : IRequestHandler<AuthenticationVerifyTwoFactorCodeQuery, AuthenticationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationVerifyTwoFactorCodeQueryHandler(IMapper mapper, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> Handle(AuthenticationVerifyTwoFactorCodeQuery request, CancellationToken cancellationToken)
        {
            var authenticationVerifyCodeRequest = new VerifyCodeRequest
            {
                Email = request.Email,
                TwoFactorCode = request.TwoFactorCode,
            };

            var authentication = await _authenticationService.VerifyTwoFactorCodeAsync(authenticationVerifyCodeRequest);

            return authentication;

        }
    }
}
