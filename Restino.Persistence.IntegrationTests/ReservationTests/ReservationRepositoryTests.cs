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

        //[Fact]
        //public async Task IsDateRangeValid_ShouldReturnTrue_WhenCheckInDateIsGreaterThanCheckOutDate()
        //{
        //    var checkInDate = DateTime.Today.AddDays(2);
        //    var checkOutDate = DateTime.Today.AddDays(1);
        //    var accommodationId = Guid.NewGuid();

        //    var result = await _repository.HasOverlapAsync(checkInDate, checkOutDate, accommodationId);

        //    Assert.True(result);
        //}

        //[Fact]
        //public async Task IsDateRangeValid_ShouldReturnTrue_WhenCheckInDateIsBeforeCurrentDate()
        //{
        //    var checkInDate = DateTime.Today.AddDays(-2);
        //    var checkOutDate = DateTime.Today.AddDays(3);
        //    var accommodationId = Guid.NewGuid();

        //    var result = await _repository.HasOverlapAsync(checkInDate, checkOutDate, accommodationId);

        //    Assert.True(result);
        //}

        //[Fact]
        //public async Task IsDateRangeValid_ShouldReturnTrue_WhenCheckInDateIsMoreThanOneYearAhead()
        //{
        //    var checkInDate = DateTime.Today.AddYears(2);
        //    var checkOutDate = DateTime.Today.AddYears(2).AddDays(3);
        //    var accommodationId = Guid.NewGuid();

        //    var result = await _repository.HasOverlapAsync(checkInDate, checkOutDate, accommodationId);

        //    Assert.True(result);
        //}

        //[Fact]
        //public async Task IsDateRangeValid_ShouldReturnFalse_WhenDatesOverlapWithExistingReservation()
        //{
        //    var checkInDate = DateTime.Today.AddDays(3);
        //    var checkOutDate = DateTime.Today.AddDays(7);
        //    var accommodationId = Guid.NewGuid();

        //    _dbContext.Reservation.Add(new Reservation
        //    {
        //        Id = Guid.NewGuid(),
        //        AccommodationId = accommodationId,
        //        CheckInDate = DateTime.Today.AddDays(8),
        //        CheckOutDate = DateTime.Today.AddDays(11),
        //        GuestsCount = 2
        //    });
        //    await _dbContext.SaveChangesAsync();

        //    var result = await _repository.HasOverlapAsync(checkInDate, checkOutDate, accommodationId);

        //    Assert.False(result);
        //}

        //[Fact]
        //public async Task IsGuestsCountWithinCapacity_ShouldReturnTrue_WhenGuestsCountExceedsCapacity()
        //{
        //    var accommodationId = Guid.NewGuid();
        //    var accommodation = new Accommodation
        //    {
        //        Id = accommodationId,
        //        Capacity = 2
        //    };
        //    _dbContext.Accommodations.Add(accommodation);
        //    await _dbContext.SaveChangesAsync();
        //    var guestsCount = 3;

        //    var result = await _repository.IsGuestsCountWithinCapacityAsync(guestsCount, accommodationId);

        //    Assert.True(result);
        //}

        //[Fact]
        //public async Task IsGuestsCountWithinCapacity_ShouldReturnFalse_WhenGuestsCountDoesNotExceedCapacity()
        //{
        //    var accommodationId = Guid.NewGuid();
        //    var accommodation = new Accommodation
        //    {
        //        Id = accommodationId,
        //        Capacity = 2
        //    };
        //    _dbContext.Accommodations.Add(accommodation);
        //    await _dbContext.SaveChangesAsync();
        //    var guestsCount = 2;

        //    var result = await _repository.IsGuestsCountWithinCapacityAsync(guestsCount, accommodationId);

        //    Assert.False(result);
        //}

        //[Fact]
        //public async Task TotalPrice_ShouldReturnCorrectTotalPrice()
        //{
        //    var accommodationId = Guid.NewGuid();
        //    var accommodation = new Accommodation
        //    {
        //        Id = accommodationId,
        //        Price = 100
        //    };
        //    _dbContext.Accommodations.Add(accommodation);
        //    await _dbContext.SaveChangesAsync();

        //    var checkInDate = DateTime.Today;
        //    var checkOutDate = DateTime.Today.AddDays(3);

        //    var result = await _repository.GetTotalPriceAsync(accommodationId, checkInDate, checkOutDate);

        //    Assert.Equal(300, result);
        //}

        [Fact]
        public async Task ListUserReservation_ShouldReturnUserReservations()
        {
            string userId = "00000000-0000-0000-0000-000000000000"; 
            _dbContext.Reservation.AddRange(new List<Reservation>
            {
                new Reservation
                {
                    Id = Guid.NewGuid(),
                    AccommodationId = Guid.NewGuid(),         
                }
 
            });
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetReservationsWithAccommodationByUserIdAsync(userId);

            Assert.Single(result);
            Assert.Contains(result, a => a.CreatedBy == userId);
        }
    }
}
