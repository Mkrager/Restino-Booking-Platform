using MediatR;

namespace Restino.Application.Features.User.Commands.DeleteTwofActorAuth
{
    public class DeleteTwoFactorAuthCommand : IRequest
    {
        public string Email { get; set; } = string.Empty;
        public string TwoFactorCode { get; set; } = string.Empty;
    }
}
