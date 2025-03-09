using FluentValidation;
using System.Text.RegularExpressions;

namespace Restino.Application.Features.User.Commands.ChangeUserPassword
{
    public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordCommandValidator()
        {
            RuleFor(p => p.NewPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[!@#$%^&*(),.?\":{}|<>]").WithMessage("Password must contain at least one special character.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.");
        }
    }
}
