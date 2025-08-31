using Microsoft.EntityFrameworkCore;
using Moq;
using Restino.Application.Contracts;
using Restino.Domain.Entities;
using Restino.Persistence.Repositories;

namespace Restino.Persistence.IntegrationTests.ReservationTests
{
    public class ReservationRepositoryTests
    {
        private readonly RestinoDbContext _dbContext;
        private readonly ReservationRepository _repository;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly string _currentUserId;

        public ReservationRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<RestinoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _currentUserId = "00000000-0000-0000-0000-000000000000";
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(m => m.UserId).Returns(_currentUserId);

            _dbContext = new RestinoDbContext(options, _currentUserServiceMock.Object);
            _repository = new ReservationRepository(_dbContext);
        }

        [Fact]
        public async Task IsDateRangeValid_ShouldReturnTrue_WhenCheckInDateIsGreaterThanCheckOutDate()
        {
            // Arrange
            var checkInDate = DateTime.Today.AddDays(2);
            var checkOutDate = DateTime.Today.AddDays(1);
            var accommodationId = Guid.NewGuid();

            // Act
            var result = await _repository.IsDateRangeValid(checkInDate, checkOutDate, accommodationId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsDateRangeValid_ShouldReturnTrue_WhenCheckInDateIsBeforeCurrentDate()
        {
            // Arrange
            var checkInDate = DateTime.Today.AddDays(-2);
            var checkOutDate = DateTime.Today.AddDays(3);
            var accommodationId = Guid.NewGuid();

            // Act
            var result = await _repository.IsDateRangeValid(checkInDate, checkOutDate, accommodationId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsDateRangeValid_ShouldReturnTrue_WhenCheckInDateIsMoreThanOneYearAhead()
        {
            // Arrange
            var checkInDate = DateTime.Today.AddYears(2);
            var checkOutDate = DateTime.Today.AddYears(2).AddDays(3);
            var accommodationId = Guid.NewGuid();

            // Act
            var result = await _repository.IsDateRangeValid(checkInDate, checkOutDate, accommodationId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsDateRangeValid_ShouldReturnFalse_WhenDatesOverlapWithExistingReservation()
        {
            // Arrange
            var checkInDate = DateTime.Today.AddDays(3);
            var checkOutDate = DateTime.Today.AddDays(7);
            var accommodationId = Guid.NewGuid();

            _dbContext.Reservation.Add(new Reservations
            {
                ReservationId = Guid.NewGuid(),
                AccommodationsId = accommodationId,
                CheckInDate = DateTime.Today.AddDays(8),
                CheckOutDate = DateTime.Today.AddDays(11),
                GuestsCount = 2
            });
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.IsDateRangeValid(checkInDate, checkOutDate, accommodationId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task IsGuestsCountWithinCapacity_ShouldReturnTrue_WhenGuestsCountExceedsCapacity()
        {
            // Arrange
            var accommodationId = Guid.NewGuid();
            var accommodation = new Accommodation
            {
                Id = accommodationId,
                Capacity = 2
            };
            _dbContext.Accommodations.Add(accommodation);
            await _dbContext.SaveChangesAsync();
            var guestsCount = 3;

            // Act
            var result = await _repository.IsGuestsCountWithinCapacity(guestsCount, accommodationId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsGuestsCountWithinCapacity_ShouldReturnFalse_WhenGuestsCountDoesNotExceedCapacity()
        {
            // Arrange
            var accommodationId = Guid.NewGuid();
            var accommodation = new Accommodation
            {
                Id = accommodationId,
                Capacity = 2
            };
            _dbContext.Accommodations.Add(accommodation);
            await _dbContext.SaveChangesAsync();
            var guestsCount = 2;

            // Act
            var result = await _repository.IsGuestsCountWithinCapacity(guestsCount, accommodationId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task TotalPrice_ShouldReturnCorrectTotalPrice()
        {
            // Arrange
            var accommodationId = Guid.NewGuid();
            var accommodation = new Accommodation
            {
                Id = accommodationId,
                Price = 100
            };
            _dbContext.Accommodations.Add(accommodation);
            await _dbContext.SaveChangesAsync();

            var checkInDate = DateTime.Today;
            var checkOutDate = DateTime.Today.AddDays(3);

            // Act
            var result = await _repository.TotalPrice(accommodationId, checkInDate, checkOutDate);

            // Assert
            Assert.Equal(300, result);
        }

        [Fact]
        public async Task ListUserReservation_ShouldReturnUserReservations()
        {
            string userId = "00000000-0000-0000-0000-000000000000"; 
            // Arrange
            _dbContext.Reservation.AddRange(new List<Reservations>
            {
                new Reservations
                {
                    ReservationId = Guid.NewGuid(),
                    AccommodationsId = Guid.NewGuid(),         
                }
 
            });
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.ListUserReservations(userId);

            // Assert
            Assert.Equal(1, result.Count);
            Assert.Contains(result, a => a.UserId == userId);
        }
    }
}
