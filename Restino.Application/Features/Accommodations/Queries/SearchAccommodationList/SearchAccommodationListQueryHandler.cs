using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Features.Accommodations.Queries.GetAccommodationList;

namespace Restino.Application.Features.Accommodations.Queries.SearchAccommodationList
{
    public class SearchAccommodationListQueryHandler : IRequestHandler<SearchAccommodationListQuery, List<AccommodationListVm>>
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IMapper _mapper;

        public SearchAccommodationListQueryHandler(IAccommodationRepository accommodationRepository, IMapper mapper)
        {
            _accommodationRepository = accommodationRepository;
            _mapper = mapper;
        }

        public async Task<List<AccommodationListVm>> Handle(SearchAccommodationListQuery request, CancellationToken cancellationToken)
        {
            var searchResult = await _accommodationRepository.SearchAccommodationAsync(request.SearchString);
            return _mapper.Map<List<AccommodationListVm>>(searchResult);
        }
    }
}
