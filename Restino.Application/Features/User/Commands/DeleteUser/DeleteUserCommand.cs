using MediatR;

namespace Restino.Application.Features.User.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public string UserId { get; set; } = string.Empty;
    }
}
