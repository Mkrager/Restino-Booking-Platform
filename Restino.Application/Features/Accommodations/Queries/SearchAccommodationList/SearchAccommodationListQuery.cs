using MediatR;
using Restino.Application.Features.Accommodations.Queries.GetAccommodationList;

namespace Restino.Application.Features.Accommodations.Queries.SearchAccommodationList
{
    public class SearchAccommodationListQuery : IRequest<List<AccommodationListVm>>
    {
        public string SearchString { get; set; } = string.Empty;
    }
}
