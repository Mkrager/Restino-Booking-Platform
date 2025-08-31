using Microsoft.EntityFrameworkCore;
using Moq;
using Restino.Application.Contracts;
using Restino.Domain.Entities;
using Restino.Persistence.Repositories;


namespace Restino.Persistence.IntegrationTests.AccommodationTests
{
    public class AccommodationRepositoryTests
    {
        private readonly RestinoDbContext _dbContext;
        private readonly AccommodationRepository _repository;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly string _currentUserId;


        public AccommodationRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<RestinoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _currentUserId = "00000000-0000-0000-0000-000000000000";
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(m => m.UserId).Returns(_currentUserId);

            _dbContext = new RestinoDbContext(options, _currentUserServiceMock.Object);
            _repository = new AccommodationRepository(_dbContext);
        }

        [Fact]
        public async Task IsAccommodationNameAndCategoryUnique_ShouldReturnTrue_WhenUnique()
        {
            string name = "Forest Tent";
            Guid categoryId = Guid.Parse("8f67819c-0d09-43e8-b64f-17c9123b6040");
            _dbContext.Accommodations.Add(new Accommodation
            {
                Id = Guid.NewGuid(),
                Address = "Testtestesttesttesttesttest",
                Capacity = 42,
                ShortDescription = "Testtestesttesttesttesttest",
                ImgUrl = "test",
                Price = 42,
                IsHotProposition = true,
                Name = name,
                CategoryId = categoryId
            });
            await _dbContext.SaveChangesAsync();
            var result = await _repository.IsAccommodationNameAndCategoryUnique("Forest Tent", Guid.Parse("8f67819c-0d09-43e8-b64f-17c9123b6040"));


            Assert.True(result);
        }

        [Fact]
        public async Task SearchAccommodation_ShouldReturn()
        {
            string searchString = "Test";
            Guid categoryId = Guid.Parse("8f67819c-0d09-43e8-b64f-17c9123b6040");
            _dbContext.Accommodations.Add(new Accommodation
            {
                Id = Guid.NewGuid(),
                Address = "Testtestesttesttesttesttest",
                Capacity = 42,
                ShortDescription = "Testtestesttesttesttesttest",
                ImgUrl = "test",
                Price = 42,
                IsHotProposition = true,
                Name = "Test",
                CategoryId = categoryId
            });
            _dbContext.Accommodations.Add(new Accommodation
            {
                Id = Guid.NewGuid(),
                Address = "Testtestesttesttesttesttest",
                Capacity = 42,
                ShortDescription = "Testtestesttesttesttesttest",
                ImgUrl = "test",
                Price = 42,
                IsHotProposition = true,
                Name = "Test42",
                CategoryId = categoryId
            });
            await _dbContext.SaveChangesAsync();
            var result = await _repository.SearchAccommodation(searchString);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task ListAllAccommodations_ShouldReturnAll_WhenNoFilterIsApplied()
        {
            // Arrange
            _dbContext.Accommodations.AddRange(new List<Accommodation>
            {
                new Accommodation
                {
                    Id = Guid.NewGuid(),
                    Address = "Test Address 1",
                    Capacity = 10,
                    ShortDescription = "Test Description 1",
                    ImgUrl = "test1.jpg",
                    Price = 100,
                    IsHotProposition = false,
                    Name = "Test 1",
                    CategoryId = Guid.NewGuid()
                },
                new Accommodation
                {
                    Id = Guid.NewGuid(),
                    Address = "Test Address 2",
                    Capacity = 20,
                    ShortDescription = "Test Description 2",
                    ImgUrl = "test2.jpg",
                    Price = 200,
                    IsHotProposition = true,
                    Name = "Test 2",
                    CategoryId = Guid.NewGuid()
                }
            });
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.ListAllAccommodations(false);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, a => a.Name == "Test 1" && a.Address == "Test Address 1");
            Assert.Contains(result, a => a.Name == "Test 2" && a.Address == "Test Address 2");
        }

        [Fact]
        public async Task ListAllAccommodations_ShouldReturnHotAccommodations_WhenFilterIsApplied()
        {
            // Arrange
            _dbContext.Accommodations.AddRange(new List<Accommodation>
            {
                new Accommodation
                {
                    Id = Guid.NewGuid(),
                    Address = "Test Address 1",
                    Capacity = 10,
                    ShortDescription = "Test Description 1",
                    ImgUrl = "test1.jpg",
                    Price = 100,
                    IsHotProposition = false,
                    Name = "Test 1",
                    CategoryId = Guid.NewGuid()
                },
                new Accommodation
                {
                    Id = Guid.NewGuid(),
                    Address = "Test Address 2",
                    Capacity = 20,
                    ShortDescription = "Test Description 2",
                    ImgUrl = "test2.jpg",
                    Price = 200,
                    IsHotProposition = true,
                    Name = "Test 2",
                    CategoryId = Guid.NewGuid()
                }
            });
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.ListAllAccommodations(true);

            // Assert
            Assert.Single(result);
            Assert.True(result.All(a => a.IsHotProposition));
            Assert.Contains(result, a => a.Name == "Test 2" && a.IsHotProposition);
        }

        [Fact]
        public async Task ListUserReservation_ShouldReturnUserReservations()
        {
            string userId = "00000000-0000-0000-0000-000000000000";
            // Arrange
            _dbContext.Accommodations.AddRange(new List<Accommodation>
            {
                new Accommodation
                {
                    CreatedBy = userId,
                    Id = Guid.NewGuid(),
                }

            });
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.ListUserAccommodations(userId);

            // Assert
            Assert.Equal(1, result.Count);
            Assert.Contains(result, a => a.CreatedBy == userId);
        }


    }
}

