using MediatR;

namespace Restino.Application.Features.User.Commands.ChangeUserPassword
{
    public class ChangeUserPasswordCommand : IRequest
    {
        public string Email { get; set; } = string.Empty;
        public string ResetPasswordCode { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
