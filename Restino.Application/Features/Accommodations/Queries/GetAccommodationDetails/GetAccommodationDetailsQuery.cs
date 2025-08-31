using MediatR;

namespace Restino.Application.Features.Accommodations.Queries.GetAccommodationDetails
{
    public class GetAccommodationDetailsQuery : IRequest<AccommodationDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
