using MediatR;

namespace Restino.Application.Features.Categories.Commands.DeleteCategoryCommand
{
    public class DeleteCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
