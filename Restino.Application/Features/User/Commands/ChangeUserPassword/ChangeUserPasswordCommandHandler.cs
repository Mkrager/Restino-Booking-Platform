using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Identity;

namespace Restino.Application.Features.User.Commands.ChangeUserPassword
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public ChangeUserPasswordCommandHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Unit> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            await _userService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword);
            return Unit.Value;
        }
    }
}
