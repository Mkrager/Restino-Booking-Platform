using MediatR;

namespace Restino.Application.Features.User.Commands.AddTwoFactorAuth
{
    public class AddTwoFactorAuthCommand : IRequest
    {
        public string Email { get; set; } = string.Empty;
        public string TwoFactorCode { get; set; } = string.Empty;
    }
}
