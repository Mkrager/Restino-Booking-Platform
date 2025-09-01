using AutoMapper;
using Moq;
using Restino.Appilcation.UnitTests.Mock;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Features.Categories.Commands.CreateCategoryCommand;
using Restino.Application.Profiles;
using Shouldly;

namespace Restino.Appilcation.UnitTests.Categories.Commands
{
    public class CreateCategoryCommandTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public CreateCategoryCommandTests()
        {
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Should_Create_Category_Successfully()
        {
            var handler = new CreateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);

            var result = await handler.Handle(new CreateCategoryCommand() { Name = "Test" }, CancellationToken.None);
            
            var allCategories = await _mockCategoryRepository.Object.ListAllAsync();
            allCategories.Count.ShouldBe(8);
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenCategoryWithSameNameAlreadyExist()
        {
            var validator = new CreateCategoryCommandValidator(_mockCategoryRepository.Object);
            var query = new CreateCategoryCommand
            {
                Name = "Appartaments"
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.ErrorMessage == "An category with the same name already exists");
        }
    }
}
