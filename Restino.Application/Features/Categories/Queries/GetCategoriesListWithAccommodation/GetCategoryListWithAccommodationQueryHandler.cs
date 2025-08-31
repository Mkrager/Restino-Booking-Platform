using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Persistance;

namespace Restino.Application.Features.Categories.Queries.GetCategoriesListWithAccommodation
{
    public class GetCategoryListWithAccommodationQueryHandler : 
        IRequestHandler<GetCategoryListWithAccommodationQuery, List<CategoryAccommodationListVm>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryListWithAccommodationQueryHandler(IMapper mapper,
            ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryAccommodationListVm>> Handle(GetCategoryListWithAccommodationQuery request, CancellationToken cancellationToken)
        {
            var list = await _categoryRepository.GetCategoryWithAccommodation(request.OnlyOneCategoryResult, request.Id);
            return _mapper.Map<List<CategoryAccommodationListVm>>(list);
        }
    }
}
