using MediatR;
using Restino.Application.Contracts.Persistance;
using AutoMapper;

namespace Restino.Application.Features.Accommodations.Queries.GetAccommodationList
{
    public class GetAccommodationListQueryHandler : IRequestHandler
        <GetAccommodationListQuery, List<AccommodationListVm>>
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IMapper _mapper;

        public GetAccommodationListQueryHandler(IMapper mapper, IAccommodationRepository accommodationRepository)
        {
            _mapper = mapper;
            _accommodationRepository = accommodationRepository;
        }

        public async Task<List<AccommodationListVm>> Handle(GetAccommodationListQuery request, CancellationToken cancellationToken)
        {
            var accommodations = await _accommodationRepository.GetAccommodationsWithCategoriesAsync(request.IsAccommodationHot);
            return _mapper.Map<List<AccommodationListVm>>(accommodations);
        }
    }
}


