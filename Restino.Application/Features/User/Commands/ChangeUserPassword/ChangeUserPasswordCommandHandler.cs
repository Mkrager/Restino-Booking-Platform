using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Identity;
using Restino.Application.Exceptions;

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
            var validation = new ChangeUserPasswordCommandValidator();
            var validationResult = await validation.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            await _userService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword);
            return Unit.Value;
        }
    }
}
