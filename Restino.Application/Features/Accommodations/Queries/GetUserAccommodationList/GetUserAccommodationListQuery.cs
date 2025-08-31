using MediatR;
using Restino.Application.Features.Accommodations.Queries.GetAccommodationList;

namespace Restino.Application.Features.Accommodations.Queries.GetUserAccommodationList
{
    public class GetUserAccommodationListQuery : IRequest<List<AccommodationListVm>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
