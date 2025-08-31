﻿using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Identity;
using Restino.Application.DTOs.Authentication;

namespace Restino.Application.Features.Accounts.Commands.Registration
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, RegistrationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public RegistrationCommandHandler(IMapper mapper, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        public async Task<RegistrationResponse> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var registerUser = new RegistrationRequest
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                UserName = request.UserName
            };

            var register = await _authenticationService.RegisterAsync(registerUser);

            return register;
        }
    }
}
