using MediatR;
using Restino.Application.Contracts.Identity;
using Restino.Application.Exceptions;
using Restino.Domain.Entities;

namespace Restino.Application.Features.User.Commands.DeleteTwofActorAuth
{
    public class DeleteTwoFactorAuthCommandHandler : IRequestHandler<DeleteTwoFactorAuthCommand>
    {
        private readonly IUserService _userService;
        private readonly IUserTwoFactorService _userTwoFactorService;
        public DeleteTwoFactorAuthCommandHandler(IUserService userService, IUserTwoFactorService userTwoFactorService)
        {
            _userService = userService;
            _userTwoFactorService = userTwoFactorService;
        }

        public async Task<Unit> Handle(DeleteTwoFactorAuthCommand request, CancellationToken cancellationToken)
        {
            await _userService.DeleteTwoFactorAsync(request.Email, request.TwoFactorCode);

            var userTwoFactor = await _userTwoFactorService.GetByUserIdAsync(request.UserId);

            if (userTwoFactor == null)
                throw new NotFoundException(nameof(UserTwoFactor), request.UserId);

            await _userTwoFactorService.DeleteTwoFactorRequestAsync(userTwoFactor);

            return Unit.Value;
        }
    }
}
