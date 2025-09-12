using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Application;
using Restino.Application.Contracts.Identity;
using Restino.Application.DTOs.Authentication;
using Restino.Domain.Entities;

namespace Restino.Application.Features.Accounts.Queries.AuthenticationVerifyTwoFactorCode
{
    public class AuthenticationVerifyTwoFactorCodeQueryHandler : IRequestHandler<AuthenticationVerifyTwoFactorCodeQuery, AuthenticationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICodeVerificationService<UserTwoFactorCode> _codeVerificationService;
        private readonly IAsyncIdentityRepository<UserTwoFactorCode> _userTwoFactorCodeRepository;
        public AuthenticationVerifyTwoFactorCodeQueryHandler(
            IMapper mapper, 
            IAuthenticationService authenticationService, 
            ICodeVerificationService<UserTwoFactorCode> codeVerificationService,
            IAsyncIdentityRepository<UserTwoFactorCode> userTwoFactorCodeRepository)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _codeVerificationService = codeVerificationService;
            _userTwoFactorCodeRepository = userTwoFactorCodeRepository;
        }

        public async Task<AuthenticationResponse> Handle(AuthenticationVerifyTwoFactorCodeQuery request, CancellationToken cancellationToken)
        {
            var userTwoFactorCode = await _userTwoFactorCodeRepository


            var authentication = await _authenticationService.AuthenticateAsync(authenticationVerifyCodeRequest);

            return authentication;

        }
    }
}
