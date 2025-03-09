using MediatR;
using Restino.Application.DTOs.Authentication;

namespace Restino.Application.Features.User.Queries.GetUserDetails
{
    public class GetUserDetailsQuery : IRequest<GetUserDetailsResponse>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
