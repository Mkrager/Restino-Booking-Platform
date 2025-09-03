using AutoMapper;
using Moq;
using Restino.Appilcation.UnitTests.Mock;
using Restino.Appilcation.UnitTests.Mocks;
using Restino.Application.Contracts.Application;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Features.Reservations.Commands.CreateReservation;
using Restino.Application.Profiles;
using Shouldly;

namespace Restino.Appilcation.UnitTests.Reservations.Commands
{
    public class CreateReservationCommandTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IReservationRepository> _mockReservationRepository;
        private readonly Mock<IAccommodationRepository> _mockAccommodationRepository;
        private readonly Mock<IReservationService> _mockReservationService;
        public CreateReservationCommandTests()
        {
            _mockReservationRepository = ReservationRepositoryMock.GetReservationRepository();
            _mockAccommodationRepository = AccommodationRepositoryMock.GetAccommodationRepository();
            _mockReservationService = RepositoryMocks.GetReservationService();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Should_Create_Reservation_Successfully()
        {
            var handler = new CreateReservationCommandHandler(
                _mapper, 
                _mockReservationRepository.Object,
                _mockAccommodationRepository.Object,
                _mockReservationService.Object);
            var command = new CreateReservationCommand
            {
                AccommodationId = Guid.Parse("6a4ab6df-66b8-46f7-8198-c94332964006"),
                AdditionalServices = "test",
                CustomerNote = "test",
                GuestsCount = 1,
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(10)
            };

            await handler.Handle(command, CancellationToken.None);


            var allReservation = await _mockReservationRepository.Object.ListAllAsync();
            allReservation.Count.ShouldBe(2);

            var createdReservation = allReservation.LastOrDefault();
            createdReservation.ShouldNotBeNull();
            createdReservation.AccommodationId.ShouldBe(command.AccommodationId);
            createdReservation.AdditionalServices.ShouldBe(command.AdditionalServices);
            createdReservation.CustomerNote.ShouldBe(command.CustomerNote);
            createdReservation.GuestsCount.ShouldBe(command.GuestsCount);
            createdReservation.CheckInDate.ShouldBe(command.CheckInDate);
            createdReservation.CheckOutDate.ShouldBe(command.CheckOutDate);
            createdReservation.Price.ShouldBe(15000);
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenReservationHaveInvalidDateRange()
        {
            var validator = new CreateReservationCommandValidator(_mockReservationRepository.Object, _mockReservationService.Object);
            var query = new CreateReservationCommand
            {
                AccommodationId = Guid.Parse("1a4ab6df-66b8-46f7-8198-c94332964001"),
                AdditionalServices = "test",
                CustomerNote = "test",
                GuestsCount = 1,
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(50)
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.ErrorMessage == "The selected dates overlap with an existing reservation");
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenReservationHaveInvalidGuestsCount()
        {
            var validator = new CreateReservationCommandValidator(_mockReservationRepository.Object, _mockReservationService.Object);
            var query = new CreateReservationCommand
            {
                AccommodationId = Guid.Parse("1a4ab6df-66b8-46f7-8198-c94332964001"),
                AdditionalServices = "test",
                CustomerNote = "test",
                GuestsCount = 100,
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(50)
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.ErrorMessage == "Guest count exceeds accommodation capacity");
        }
    }
}
