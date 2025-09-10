using MediatR;

namespace Restino.Application.Features.User.Commands.AddTwoFactorAuth
{
    public class AddTwoFactorAuthCommand : IRequest
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TwoFactorCode { get; set; } = string.Empty;
    }
}
