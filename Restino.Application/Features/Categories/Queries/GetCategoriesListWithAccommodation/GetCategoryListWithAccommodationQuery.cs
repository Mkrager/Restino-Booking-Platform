using MediatR;

namespace Restino.Application.Features.Categories.Queries.GetCategoriesListWithAccommodation
{
    public class GetCategoryListWithAccommodationQuery : IRequest<List<CategoryAccommodationListVm>>
    {
        public Guid? Id { get; set; }
        public bool OnlyOneCategoryResult { get; set; }
    }
}
