using AutoMapper;
using Moq;
using Restino.Appilcation.UnitTests.Mocks;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Features.Accommodations.Queries.GetAccommodationList;
using Restino.Application.Features.Accommodations.Queries.SearchAccommodationList;
using Restino.Application.Profiles;
using Shouldly;

namespace Restino.Appilcation.UnitTests.Accommodations.Queries
{
    public class SearchAccommodationListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAccommodationRepository> _mockAccommodationRepository;

        public SearchAccommodationListQueryHandlerTests()
        {
            _mockAccommodationRepository = AccommodationRepositoryMock.GetAccommodationRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task SearchAccommodationsList_ReturnsListOfAccommodations()
        {
            var handler = new SearchAccommodationListQueryHandler(_mockAccommodationRepository.Object, _mapper);

            var result = await handler.Handle(new SearchAccommodationListQuery() { SearchString = "City" }, CancellationToken.None);

            result.ShouldBeOfType<List<AccommodationListVm>>();
            result.Count.ShouldBe(1);

            var accommodation = result.First();

            accommodation.Id.ShouldBe(Guid.Parse("1a4ab6df-66b8-46f7-8198-c94332964001"));
            accommodation.Name.ShouldBe("City View Apartment");
            accommodation.Address.ShouldBe("101 Central St, Cityville");
            accommodation.Capacity.ShouldBe(4);
            accommodation.CreatedDate.Date.ShouldBe(DateTime.Today.AddDays(20).Date);
            accommodation.CategoryId.ShouldBe(Guid.Parse("c119661c-1d5a-42c1-8819-6b0885af4d4a"));
            accommodation.ShortDescription.ShouldBe("A modern apartment with a city view.");
            accommodation.ImgUrl.ShouldBe("https://cdn.pixabay.com/photo/2015/09/05/21/51/room-924058_1280.jpg");
            accommodation.Price.ShouldBe(7000);
        }
    }
}
