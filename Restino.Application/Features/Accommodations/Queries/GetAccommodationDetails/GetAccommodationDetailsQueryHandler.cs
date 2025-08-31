using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Persistance;
using Restino.Domain.Entities;

namespace Restino.Application.Features.Accommodations.Queries.GetAccommodationDetails
{
    public class GetAccommodationDetailsQueryHandler : IRequestHandler<GetAccommodationDetailsQuery,
        AccommodationDetailsVm>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Accommodation> _accommodationRepository;
        private readonly IAsyncRepository<Category> _categoryRepository;

        public GetAccommodationDetailsQueryHandler(IMapper mapper, IAsyncRepository<Accommodation> accommodationRepository, IAsyncRepository<Category> categoryRepository)
        {
            _accommodationRepository = accommodationRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<AccommodationDetailsVm> Handle(GetAccommodationDetailsQuery request, CancellationToken cancellationToken)
        {
            var @accommodation = await _accommodationRepository.GetByIdAsync(request.Id);
            var accommodationDetailsDto = _mapper.Map<AccommodationDetailsVm>(@accommodation);

            var category = await _categoryRepository.GetByIdAsync(@accommodation.CategoryId);

            accommodationDetailsDto.Category = _mapper.Map<CategoryDto>(category);

            return accommodationDetailsDto;
        }
    }
}


