using MediatR;
using Restino.Application.Contracts.Application;
using Restino.Application.Contracts.Identity;
using Restino.Application.Exceptions;
using Restino.Domain.Entities;

namespace Restino.Application.Features.User.Commands.AddTwoFactorAuth
{
    public class AddTwoFactorAuthCommandHandler : IRequestHandler<AddTwoFactorAuthCommand>
    {
        private readonly IUserService _userService;
        private readonly IUserTwoFactorService _userTwoFactorService;
        private readonly ICodeVerificationService<UserTwoFactorCode> _codeVerificationService;
        public AddTwoFactorAuthCommandHandler(IUserService userService, IUserTwoFactorService userTwoFactorService, ICodeVerificationService<UserTwoFactorCode> codeVerificationService)
        {
            _userService = userService;
            _userTwoFactorService = userTwoFactorService;
            _codeVerificationService = codeVerificationService;
        }

        public async Task<Unit> Handle(AddTwoFactorAuthCommand request, CancellationToken cancellationToken)
        {
            var userTwoFactor = await _userTwoFactorService.GetByUserIdAsync(request.UserId);

            if (userTwoFactor == null)
                throw new NotFoundException(nameof(UserTwoFactorCode), request.UserId);

            var isValid = _codeVerificationService.VerifyCode(userTwoFactor, request.TwoFactorCode);

            if (!isValid)
                throw new InvalidCodeException();

            await _userService.AddTwoFactorToAccountAsync(request.Email);

            await _userTwoFactorService.DeleteTwoFactorRequestAsync(userTwoFactor);

            return Unit.Value;
        }
    }
}
