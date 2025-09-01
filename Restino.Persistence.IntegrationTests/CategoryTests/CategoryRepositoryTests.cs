using Microsoft.EntityFrameworkCore;
using Moq;
using Restino.Application.Contracts;
using Restino.Domain.Entities;
using Restino.Persistence.Repositories;

namespace Restino.Persistence.IntegrationTests.CategoryTests
{
    public class CategoryRepositoryTests
    {
        private readonly RestinoDbContext _dbContext;
        private readonly CategoryRepository _repository;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly string _currentUserId;

        public CategoryRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<RestinoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _currentUserId = "00000000-0000-0000-0000-000000000000";
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(m => m.UserId).Returns(_currentUserId);

            _dbContext = new RestinoDbContext(options, _currentUserServiceMock.Object);
            _repository = new CategoryRepository(_dbContext);
        }

        [Fact]
        public async Task IsNameUnique_ShouldReturnTrue_WhenUnique()
        {
            string name = "Test";
            _dbContext.Categories.Add(new Category
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Description = "Testtestest",
                ImgUrl = "Test"
            });
            await _dbContext.SaveChangesAsync();
            var result = await _repository.IsCategoryNameUniqueAsync(name);


            Assert.True(result);
        }

        [Fact]
        public async Task GetCategoryWithAccommodation_ReturnsCategoriesWithAccommodations_WhenOnlyOneCategoryResultIsFalse()
        {
            var categoryId = Guid.NewGuid();
            var category = new Category
            {
                Id = categoryId,
                Name = "Test Category"
            };

            var accommodation1 = new Accommodation { Id = Guid.NewGuid(), Name = "Accommodation 1", CategoryId = categoryId };
            var accommodation2 = new Accommodation { Id = Guid.NewGuid(), Name = "Accommodation 2", CategoryId = categoryId };

            category.Accommodations = new List<Accommodation> { accommodation1, accommodation2 };

            _dbContext.Categories.Add(category);
            _dbContext.Accommodations.AddRange(accommodation1, accommodation2);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetCategoryWithAccommodationAsync(false, categoryId);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(category.Name, result[0].Name);
            Assert.Equal(2, result[0].Accommodations.Count);
        }

        [Fact]
        public async Task GetCategoryWithAccommodation_ReturnsOnlyCategoriesWithSpecificAccommodation_WhenOnlyOneCategoryResultIsTrue()
        {
            var Id = Guid.NewGuid();
            var category1 = new Category
            {
                Id = Id,
                Name = "Category 1"
            };
            var category2 = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category 2"
            };

            var accommodation1 = new Accommodation { Id = Guid.NewGuid(), Name = "Accommodation 1" };
            var accommodation2 = new Accommodation { Id = Guid.NewGuid(), Name = "Accommodation 2" };
            var accommodation3 = new Accommodation { Id = Guid.NewGuid(), Name = "Accommodation 3" };

            category1.Accommodations = new List<Accommodation> { accommodation1, accommodation2 };
            category2.Accommodations = new List<Accommodation> { accommodation3 };

            _dbContext.Categories.AddRange(category1, category2);
            _dbContext.Accommodations.AddRange(accommodation1, accommodation2, accommodation3);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetCategoryWithAccommodationAsync(true, Id);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(category1.Name, result[0].Name);
            Assert.Equal(2, result[0].Accommodations.Count);
        }
    }
}


