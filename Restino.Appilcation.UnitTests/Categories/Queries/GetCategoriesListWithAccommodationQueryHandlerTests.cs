using AutoMapper;
using Moq;
using Restino.Appilcation.UnitTests.Mock;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Features.Categories.Queries.GetCategoriesListWithAccommodation;
using Restino.Application.Profiles;
using Shouldly;

namespace Restino.Appilcation.UnitTests.Categories.Queries
{
    public class GetCategoriesListWithAccommodationQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public GetCategoriesListWithAccommodationQueryHandlerTests()
        {
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetCategoriesListWithAccommodation()
        {

            var handler = new GetCategoryListWithAccommodationQueryHandler(_mapper, _mockCategoryRepository.Object);

            var result = await handler.Handle(new GetCategoryListWithAccommodationQuery() { OnlyOneCategoryResult = false, Id = Guid.Empty }, CancellationToken.None);

            result.ShouldBeOfType<List<CategoryAccommodationListVm>>();

            result.Count.ShouldBe(7);
        }

        [Fact]
        public async Task GetCategoriesListWithAccommodation_OnlyOnCategoryResultTrue()
        {
            var handler = new GetCategoryListWithAccommodationQueryHandler(_mapper, _mockCategoryRepository.Object);

            var result = await handler.Handle(new GetCategoryListWithAccommodationQuery() { OnlyOneCategoryResult = true, Id = Guid.Parse("c119661c-1d5a-42c1-8819-6b0885af4d4a") }, CancellationToken.None);

            result.ShouldBeOfType<List<CategoryAccommodationListVm>>();

            result.Count.ShouldBe(1);

            var category = result.FirstOrDefault();

            category.Accommodations.ShouldNotBeNull();

            category.Accommodations.Count.ShouldBeGreaterThan(0);

            foreach (var accommodation in category.Accommodations)
            {
                accommodation.CategoryId.ShouldBe(Guid.Parse("c119661c-1d5a-42c1-8819-6b0885af4d4a"));
            }
        }

    }
}
