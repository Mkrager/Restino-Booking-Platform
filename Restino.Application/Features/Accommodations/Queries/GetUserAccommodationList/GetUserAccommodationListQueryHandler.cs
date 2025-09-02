using MediatR;
using Restino.Application.Contracts.Persistance;
using AutoMapper;
using Restino.Application.Features.Accommodations.Queries.GetAccommodationList;

namespace Restino.Application.Features.Accommodations.Queries.GetUserAccommodationList
{
    public class GetUserAccommodationListQueryHandler : IRequestHandler
        <GetUserAccommodationListQuery, List<AccommodationListVm>>
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IMapper _mapper;

        public GetUserAccommodationListQueryHandler(IMapper mapper, IAccommodationRepository accommodationRepository)
        {
            _mapper = mapper;
            _accommodationRepository = accommodationRepository;
        }

        public async Task<List<AccommodationListVm>> Handle(GetUserAccommodationListQuery request, CancellationToken cancellationToken)
        {
            var userAccommodation = await _accommodationRepository.GetAccommodationsWithCategoriesByUserIdAsync(request.UserId);
            return _mapper.Map<List<AccommodationListVm>>(userAccommodation);
        }
    }
}


