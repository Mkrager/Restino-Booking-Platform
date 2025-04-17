using MediatR;

namespace Restino.Application.Features.User.Queries.SendTwoFactoreCode
{
    public class SendTwoFactoreCodeQuery : IRequest
    {
        public string Email {  get; set; } = string.Empty;
    }
}
