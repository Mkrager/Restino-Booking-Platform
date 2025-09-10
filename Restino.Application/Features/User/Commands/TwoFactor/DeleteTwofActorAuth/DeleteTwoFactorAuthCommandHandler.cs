using MediatR;
using Restino.Application.Contracts.Application;
using Restino.Application.Contracts.Identity;
using Restino.Application.Exceptions;
using Restino.Domain.Entities;

namespace Restino.Application.Features.User.Commands.TwoFactor.DeleteTwofActorAuth
{
    public class DeleteTwoFactorAuthCommandHandler : IRequestHandler<DeleteTwoFactorAuthCommand>
    {
        private readonly IUserService _userService;
        private readonly IUserTwoFactorRepository _userTwoFactorService;
        private readonly ICodeVerificationService<UserTwoFactorCode> _codeVerificationService;

        public DeleteTwoFactorAuthCommandHandler(IUserService userService, IUserTwoFactorRepository userTwoFactorService, ICodeVerificationService<UserTwoFactorCode> codeVerificationService)
        {
            _userService = userService;
            _userTwoFactorService = userTwoFactorService;
            _codeVerificationService = codeVerificationService;
        }

        public async Task<Unit> Handle(DeleteTwoFactorAuthCommand request, CancellationToken cancellationToken)
        {
            var userTwoFactor = await _userTwoFactorService.GetByUserIdAsync(request.UserId);

            if (userTwoFactor == null)
                throw new NotFoundException(nameof(UserTwoFactorCode), request.UserId);

            var isValid = _codeVerificationService.VerifyCode(userTwoFactor, request.TwoFactorCode);

            if (!isValid)
                throw new InvalidCodeException();

            await _userService.DeleteTwoFactorFromAccountAsync(request.Email);

            await _userTwoFactorService.DeleteAsync(userTwoFactor);

            return Unit.Value;
        }
    }
}
