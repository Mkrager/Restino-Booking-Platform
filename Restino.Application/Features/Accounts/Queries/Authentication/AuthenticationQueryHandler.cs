using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Identity;
using Restino.Application.DTOs.Authentication;
using Restino.Application.Features.TwoFactor.Queries.SendTwoFactoreCode;

namespace Restino.Application.Features.Accounts.Queries.Authentication
{
    public class AuthenticationQueryHandler : IRequestHandler<AuthenticationQuery, AuthenticationVm>
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMediator _mediator;
        public AuthenticationQueryHandler(
            IMapper mapper, 
            IAuthenticationService authenticationService, 
            IMediator mediator)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<AuthenticationVm> Handle(AuthenticationQuery request, CancellationToken cancellationToken)
        {
            var authentication = await _authenticationService
                .AuthenticateAsync(_mapper.Map<AuthenticationRequest>(request));

            if (authentication.TwoFactorRequired)
            {
                await _mediator.Send(new SendTwoFactoreCodeQuery()
                {
                    Email = request.Email
                });
            }

            return _mapper.Map<AuthenticationVm>(authentication);
        }
    }
}