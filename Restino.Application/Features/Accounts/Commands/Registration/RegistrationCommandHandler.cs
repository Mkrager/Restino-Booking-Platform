using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Identity;
using Restino.Application.DTOs.Authentication;

namespace Restino.Application.Features.Accounts.Commands.Registration
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public RegistrationCommandHandler(IMapper mapper, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        public async Task<string> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var register = await _authenticationService
                .RegisterAsync(_mapper.Map<RegistrationRequest>(request));

            return register;
        }
    }
}
