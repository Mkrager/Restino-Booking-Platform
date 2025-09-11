using MediatR;

namespace Restino.Application.Features.TwoFactor.Commands.DeleteTwofActorAuth
{
    public class DeleteTwoFactorAuthCommand : IRequest
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TwoFactorCode { get; set; } = string.Empty;
    }
}
