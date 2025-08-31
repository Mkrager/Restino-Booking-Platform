using MediatR;

namespace Restino.Application.Features.Accommodations.Queries.GetUserAccommodationList
{
    public class GetUserAccommodationListQuery : IRequest<List<UserAccommodationListVm>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
