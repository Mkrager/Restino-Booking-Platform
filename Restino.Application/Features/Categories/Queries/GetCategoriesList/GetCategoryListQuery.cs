using MediatR;

namespace Restino.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoryListQuery : IRequest<List<CategoryListVm>>
    {
    }
}
