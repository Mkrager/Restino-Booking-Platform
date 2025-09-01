using AutoMapper;
using Moq;
using Restino.Appilcation.UnitTests.Mock;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Features.Categories.Commands.DeleteCategoryCommand;
using Restino.Application.Profiles;
using Shouldly;

namespace Restino.Appilcation.UnitTests.Categories.Commands
{
    public class DeleteCategoryCommandTests
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public DeleteCategoryCommandTests()
        {
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
        }

        [Fact]
        public async Task Delete_Category_RemovesCategoryFromRepo()
        {
            var handler = new DeleteCategoryCommandHandler(_mockCategoryRepository.Object);
            await handler.Handle(new DeleteCategoryCommand() { Id = Guid.Parse("c119661c-1d5a-42c1-8819-6b0885af4d4a") }, CancellationToken.None);

            var allCategories = await _mockCategoryRepository.Object.ListAllAsync();
            allCategories.Count.ShouldBe(6);
        }
    }
}
