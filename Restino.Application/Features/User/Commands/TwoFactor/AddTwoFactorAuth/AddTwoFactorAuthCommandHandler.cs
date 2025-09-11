using MediatR;
using Restino.Application.Contracts.Application;
using Restino.Application.Contracts.Identity;
using Restino.Application.Exceptions;
using Restino.Domain.Entities;

namespace Restino.Application.Features.User.Commands.TwoFactor.AddTwoFactorAuth
{
    public class AddTwoFactorAuthCommandHandler : IRequestHandler<AddTwoFactorAuthCommand>
    {
        private readonly IUserService _userService;
        private readonly IUserTwoFactorCodeRepository _userTwoFactorRepository;
        private readonly ICodeVerificationService<UserTwoFactorCode> _codeVerificationService;
        public AddTwoFactorAuthCommandHandler(
            IUserService userService,
            IUserTwoFactorCodeRepository userTwoFactorRepository, 
            ICodeVerificationService<UserTwoFactorCode> codeVerificationService)
        {
            _userService = userService;
            _userTwoFactorRepository = userTwoFactorRepository;
            _codeVerificationService = codeVerificationService;
        }

        public async Task<Unit> Handle(AddTwoFactorAuthCommand request, CancellationToken cancellationToken)
        {
            var userTwoFactor = await _userTwoFactorRepository.GetByUserIdAsync(request.UserId);

            if (userTwoFactor == null)
                throw new NotFoundException(nameof(UserTwoFactorCode), request.UserId);

            var isValid = _codeVerificationService.VerifyCode(userTwoFactor, request.TwoFactorCode);

            if (!isValid)
                throw new InvalidCodeException();

            await _userService.AddTwoFactorToAccountAsync(request.Email);

            await _userTwoFactorRepository.DeleteAsync(userTwoFactor);

            return Unit.Value;
        }
    }
}