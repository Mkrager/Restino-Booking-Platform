using AutoMapper;
using Moq;
using Restino.Appilcation.UnitTests.Mock;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Features.Reservations.Queries.ListUserReservations;
using Restino.Application.Profiles;
using Shouldly;

namespace Restino.Appilcation.UnitTests.Reservations.Queries
{
    public class GetUserReservationListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IReservationRepository> _mockReservationRepository;
        private readonly Mock<IAccommodationRepository> _mockAccommodationRepository;

        public GetUserReservationListQueryHandlerTests()
        {
            _mockReservationRepository = RepositoryMocks.GetReservationRepository();
            _mockAccommodationRepository = RepositoryMocks.GetAccommodationRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task ListUserReservationListTest()
        {
            var handler = new GetUserReservationListQueryHandler(_mapper, _mockReservationRepository.Object, _mockAccommodationRepository.Object);

            string userId = "534ab6df-66b8-46f7-8198-c94332964001";

            var result = await handler.Handle(new GetUserReservationListQuery() { UserId = userId }, CancellationToken.None);

            result.ShouldBeOfType<List<GetUserReservationListVm>>();

            result.Count.ShouldBe(1);
        }
    }
}
