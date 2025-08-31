using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Persistance;
using Restino.Domain.Entities;

namespace Restino.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoryListQueryHandler :
        IRequestHandler<GetCategoryListQuery, List<CategoryListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Category> _categoryRepository;

        public GetCategoryListQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryListVm>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var allCategory = (await _categoryRepository.ListAllAsync()).OrderBy(x => x.Name);
            return _mapper.Map<List<CategoryListVm>>(allCategory);
        }
    }
}
