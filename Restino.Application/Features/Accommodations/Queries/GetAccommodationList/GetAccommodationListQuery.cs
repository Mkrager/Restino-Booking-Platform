using MediatR;

namespace Restino.Application.Features.Accommodations.Queries.GetAccommodationList
{
    public class GetAccommodationListQuery : IRequest<List<AccommodationListVm>>
    {
        public Guid Id { get; set; }
        public bool isAccommodationHot { get; set; }
    }
}
