using MediatR;

namespace Restino.Application.Features.PasswordReset.Queries.SendPasswoedResetCode
{
    public class SendPasswordResetCodeQuery : IRequest
    {
        public string Email { get; set; } = string.Empty;
    }
}
