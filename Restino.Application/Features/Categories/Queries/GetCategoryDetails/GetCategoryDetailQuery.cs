using MediatR;

namespace Restino.Application.Features.Categories.Queries.GetCategoryDetails
{
    public class GetCategoryDetailQuery : IRequest<CategoryDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
