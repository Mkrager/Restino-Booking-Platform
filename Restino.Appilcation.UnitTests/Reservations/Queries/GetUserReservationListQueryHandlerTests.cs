using AutoMapper;
using Moq;
using Restino.Appilcation.UnitTests.Mocks;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Features.Reservations.Queries.GetUserReservations;
using Restino.Application.Profiles;
using Shouldly;

namespace Restino.Appilcation.UnitTests.Reservations.Queries
{
    public class GetUserReservationListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IReservationRepository> _mockReservationRepository;

        public GetUserReservationListQueryHandlerTests()
        {
            _mockReservationRepository = ReservationRepositoryMock.GetReservationRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetUserReservationList_ReturnsListOfUserReservation()
        {
            var handler = new GetUserReservationListQueryHandler(_mapper, _mockReservationRepository.Object);

            string userId = "534ab6df-66b8-46f7-8198-c94332964001";

            var result = await handler.Handle(new GetUserReservationListQuery() { UserId = userId }, CancellationToken.None);

            result.ShouldBeOfType<List<GetUserReservationListVm>>();

            result.Count.ShouldBe(1);
        }
    }
}
