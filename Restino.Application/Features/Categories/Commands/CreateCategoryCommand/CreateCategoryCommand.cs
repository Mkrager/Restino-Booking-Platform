using MediatR;

namespace Restino.Application.Features.Categories.Commands.CreateCategoryCommand
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImgUrl { get; set; } = string.Empty;

    }
}
