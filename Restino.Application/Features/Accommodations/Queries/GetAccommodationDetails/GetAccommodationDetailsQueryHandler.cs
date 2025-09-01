using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Persistance;

namespace Restino.Application.Features.Accommodations.Queries.GetAccommodationDetails
{
    public class GetAccommodationDetailsQueryHandler : IRequestHandler<GetAccommodationDetailsQuery, AccommodationDetailsVm>
    {
        private readonly IMapper _mapper;
        private readonly IAccommodationRepository _accommodationRepository;

        public GetAccommodationDetailsQueryHandler(IMapper mapper, IAccommodationRepository accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
            _mapper = mapper;
        }
        public async Task<AccommodationDetailsVm> Handle(GetAccommodationDetailsQuery request, CancellationToken cancellationToken)
        {
            var @accommodation = await _accommodationRepository.GetAccommodationWithCategoryByIdAsync(request.Id);
            return _mapper.Map<AccommodationDetailsVm>(@accommodation);
        }
    }
}


