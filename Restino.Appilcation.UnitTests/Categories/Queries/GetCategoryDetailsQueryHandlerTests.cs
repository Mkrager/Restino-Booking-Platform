using AutoMapper;
using Moq;
using Restino.Appilcation.UnitTests.Mocks;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Features.Categories.Queries.GetCategoryDetails;
using Restino.Application.Profiles;
using Shouldly;

namespace Restino.Appilcation.UnitTests.Categories.Queries
{
    public class GetCategoryDetailsQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public GetCategoryDetailsQueryHandlerTests()
        {
            _mockCategoryRepository = CategoryRepositoryMock.GetCategoryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetCategoryDetails_ReturnsCorrectCategoryDetails()
        {
            var handler = new GetCategoryDetailQueryHandler(_mapper, _mockCategoryRepository.Object);

            var result = await handler.Handle(new GetCategoryDetailQuery() { Id = Guid.Parse("c119661c-1d5a-42c1-8819-6b0885af4d4a") }, CancellationToken.None);

            result.ShouldBeOfType<CategoryDetailsVm>();

            result.Id.ShouldBe(Guid.Parse("c119661c-1d5a-42c1-8819-6b0885af4d4a"));
            result.Name.ShouldBe("Appartaments");
            result.Description.ShouldBe("Apartments are a modern type of accommodation...");
            result.ImgUrl.ShouldBe("https://cdn.pixabay.com/photo/2016/11/30/08/48/bedroom-1872196_1280.jpg");
        }

    }
}
