using MediatR;

namespace Restino.Application.Features.Accommodations.Queries.GetAccommodationList
{
    public class GetAccommodationListQuery : IRequest<List<AccommodationListVm>>
    {
        public bool IsAccommodationHot { get; set; }
    }
}
