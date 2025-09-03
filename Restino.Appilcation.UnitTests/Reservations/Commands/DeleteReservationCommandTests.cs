using AutoMapper;
using Moq;
using Restino.Appilcation.UnitTests.Mocks;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Features.Reservations.Commands.DeleteReservation;
using Restino.Application.Profiles;
using Shouldly;

namespace Restino.Appilcation.UnitTests.Reservations.Commands
{
    public class DeleteReservationCommandTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IReservationRepository> _mockReservationRepository;

        public DeleteReservationCommandTests()
        {
            _mockReservationRepository = ReservationRepositoryMock.GetReservationRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Delete_Reservation_RemovesAccommodationFromRepo()
        {
            var handler = new DeleteReservationCommandHandler(_mapper, _mockReservationRepository.Object);
            await handler.Handle(new DeleteReservationCommand() { Id = Guid.Parse("7a4ab6df-66b8-55f7-6698-c94332964007"), UserRole = "Admin" }, CancellationToken.None);

            var allAccommodations = await _mockReservationRepository.Object.ListAllAsync();
            allAccommodations.Count.ShouldBe(0);
        }

    }
}
