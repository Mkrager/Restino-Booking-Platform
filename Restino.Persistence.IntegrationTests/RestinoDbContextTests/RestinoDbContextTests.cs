using Microsoft.EntityFrameworkCore;
using Moq;
using Restino.Application.Contracts;
using Restino.Domain.Entities;

namespace Restino.Persistence.IntegrationTests.RestinoDbContextTests
{
    public class RestinoDbContextTests
    {
        private readonly RestinoDbContext _dbContext;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly string _currentUserId;

        public RestinoDbContextTests()
        {
            var options = new DbContextOptionsBuilder<RestinoDbContext>()
            .UseInMemoryDatabase("TestDatabase")
                .Options;

            _currentUserId = "00000000-0000-0000-0000-000000000000";
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(m => m.UserId).Returns(_currentUserId);

            _dbContext = new RestinoDbContext(options, _currentUserServiceMock.Object);
        }

        [Fact]
        public async Task SaveChangesAsync_ShouldSetCreatedDate_WhenEntityIsAdded()
        {
            var accommodation = new Accommodations { Name = "New Accommodation" };

            _dbContext.Accommodations.Add(accommodation);
            await _dbContext.SaveChangesAsync();

            var savedAccommodation = await _dbContext.Accommodations.FindAsync(accommodation.AccommodationsId);

            Assert.NotNull(savedAccommodation);
            Assert.NotEqual(DateTime.MinValue, savedAccommodation.CreatedDate);
        }

        [Fact]
        public async Task SaveChangesAsync_ShouldSetLastModifiedDate_WhenEntityIsModified()
        {
            var accommodation = new Accommodations
            {
                Name = "Test Accommodation"
            };

            await _dbContext.Accommodations.AddAsync(accommodation);
            await _dbContext.SaveChangesAsync();

            accommodation.Name = "Updated Accommodation";
            await _dbContext.SaveChangesAsync();

            Assert.NotEqual(DateTime.MinValue, accommodation.LastModifiedDate);
            Assert.Equal(accommodation.LastModifiedDate?.Date, DateTime.Now.Date); 
        }
    }
}

