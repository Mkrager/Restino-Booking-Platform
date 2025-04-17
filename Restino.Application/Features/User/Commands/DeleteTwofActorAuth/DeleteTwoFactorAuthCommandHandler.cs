using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Identity;

namespace Restino.Application.Features.User.Commands.DeleteTwofActorAuth
{
    public class DeleteTwoFactorAuthCommandHandler : IRequestHandler<DeleteTwoFactorAuthCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public DeleteTwoFactorAuthCommandHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Unit> Handle(DeleteTwoFactorAuthCommand request, CancellationToken cancellationToken)
        {
            await _userService.DeleteTwoFactorAsync(request.Email, request.TwoFactorCode);
            return Unit.Value;
        }
    }
}
