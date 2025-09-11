using MediatR;
using Restino.Application.Contracts.Application;
using Restino.Application.Contracts.Identity;
using Restino.Application.Exceptions;
using Restino.Domain.Entities;

namespace Restino.Application.Features.PasswordReset.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly IUserResetPasswordCodeRepository _userResetPasswordRepository;
        private readonly ICodeVerificationService<UserResetPasswordCode> _codeVerificationService;
        private readonly IUserService _userService;
        public ResetPasswordCommandHandler(
            IUserResetPasswordCodeRepository userResetPasswordRepository, 
            IUserService userService, 
            ICodeVerificationService<UserResetPasswordCode> codeVerificationService)
        {
            _userResetPasswordRepository = userResetPasswordRepository;
            _userService = userService;
            _codeVerificationService = codeVerificationService;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var userResetPassword = await _userResetPasswordRepository.GetByEmailAsync(request.Email);

            if (userResetPassword == null)
                throw new NotFoundException(nameof(UserResetPasswordCode), request.Email);

            var isValid = _codeVerificationService.VerifyCode(userResetPassword, request.ResetPasswordCode);

            if (!isValid)
                throw new InvalidCodeException();

            await _userService.ResetPasswordAsync(request.Email, request.NewPassword);

            await _userResetPasswordRepository.DeleteAsync(userResetPassword);

            return Unit.Value;
        }
    }
}