using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Application;
using Restino.Application.Contracts.Identity;
using Restino.Application.DTOs.Authentication;
using Restino.Application.Exceptions;
using Restino.Domain.Entities;

namespace Restino.Application.Features.Accounts.Queries.AuthenticationVerifyTwoFactorCode
{
    public class AuthenticationVerifyTwoFactorCodeQueryHandler : IRequestHandler<AuthenticationVerifyTwoFactorCodeQuery, AuthenticationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICodeVerificationService<UserTwoFactorCode> _codeVerificationService;
        private readonly IUserTwoFactorCodeRepository _userTwoFactorCodeRepository;
        public AuthenticationVerifyTwoFactorCodeQueryHandler(
            IMapper mapper, 
            IAuthenticationService authenticationService, 
            ICodeVerificationService<UserTwoFactorCode> codeVerificationService,
            IUserTwoFactorCodeRepository userTwoFactorCodeRepository)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _codeVerificationService = codeVerificationService;
            _userTwoFactorCodeRepository = userTwoFactorCodeRepository;
        }

        public async Task<AuthenticationResponse> Handle(AuthenticationVerifyTwoFactorCodeQuery request, CancellationToken cancellationToken)
        {
            var userTwoFactorCode = await _userTwoFactorCodeRepository.GetByUserEmailAsync(request.Email);

            if (userTwoFactorCode == null)
                throw new NotFoundException(nameof(UserTwoFactorCode), request.Email);

            var isValid = _codeVerificationService.VerifyCode(userTwoFactorCode, request.TwoFactorCode);

            if (!isValid)
                throw new InvalidCodeException();

            var authentication = await _authenticationService.AuthenticateAsync(_mapper.Map<AuthenticationRequest>(request));

            return authentication;

        }
    }
}
