using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Identity;

namespace Restino.Application.Features.User.Commands.AddTwoFactorAuth
{
    public class AddTwoFactorAuthCommandHandler : IRequestHandler<AddTwoFactorAuthCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public AddTwoFactorAuthCommandHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Unit> Handle(AddTwoFactorAuthCommand request, CancellationToken cancellationToken)
        {
            await _userService.AddTwoFactorAsync(request.Email, request.TwoFactorCode);
            return Unit.Value;
        }
    }
}
