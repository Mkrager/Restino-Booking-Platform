using MediatR;
using Restino.Application.Contracts.Persistance;
using Restino.Domain.Entities;
using AutoMapper;

namespace Restino.Application.Features.Accommodations.Queries.GetUserAccommodationList
{
    public class GetUserAccommodationListQueryHandler : IRequestHandler
        <GetUserAccommodationListQuery, List<UserAccommodationListVm>>
    {
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IMapper _mapper;

        public GetUserAccommodationListQueryHandler(IMapper mapper, IAccommodationRepository accommodationRepository, IAsyncRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _accommodationRepository = accommodationRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<UserAccommodationListVm>> Handle(GetUserAccommodationListQuery request, CancellationToken cancellationToken)
        {
            var accommodationsUser = await _accommodationRepository.GetAccommodationsWithCategoriesByUserIdAsync(request.UserId);
            var accommodationUserDetailsDto = _mapper.Map<List<UserAccommodationListVm>>(accommodationsUser);

            var categories = await _categoryRepository.ListAllAsync();
            foreach (var accommodation in accommodationUserDetailsDto)
            {
                accommodation.Category = _mapper.Map<CategoryUserDtoAccommodation>(categories.FirstOrDefault(c => c.Id == accommodation.CategoryId));
            }
            return accommodationUserDetailsDto;
        }
    }
}


