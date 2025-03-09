using MediatR;
using Restino.Application.DTOs.Authentication;

namespace Restino.Application.Features.User.Queries.SearchUser
{
    public class SearchUserQuery : IRequest<List<SearchUserResponse>>
    {
        public string UserName { get; set; } = string.Empty;
    }
}
