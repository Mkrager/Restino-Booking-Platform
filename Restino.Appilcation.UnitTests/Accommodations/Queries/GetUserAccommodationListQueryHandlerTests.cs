using AutoMapper;
using Moq;
using Restino.Appilcation.UnitTests.Mock;
using Restino.Appilcation.UnitTests.Mocks;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Features.Accommodations.Queries.GetAccommodationList;
using Restino.Application.Features.Accommodations.Queries.GetUserAccommodationList;
using Restino.Application.Profiles;
using Shouldly;

namespace Restino.Appilcation.UnitTests.Accommodations.Queries
{
    public class GetUserAccommodationListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAccommodationRepository> _mockAccommodationRepository;

        public GetUserAccommodationListQueryHandlerTests()
        {
            _mockAccommodationRepository = AccommodationRepositoryMock.GetAccommodationRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetUserAccommdoationsList_ReturnsListOfUserAccommodations()
        {
            var handler = new GetUserAccommodationListQueryHandler(_mapper, _mockAccommodationRepository.Object);

            string userId = "12334556456745";

            var result = await handler.Handle(new GetUserAccommodationListQuery() { UserId = userId }, CancellationToken.None);

            result.ShouldBeOfType<List<AccommodationListVm>>();

            result.Count.ShouldBe(1);
        }

    }
}
