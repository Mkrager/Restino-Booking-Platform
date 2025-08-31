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
        public async Task IsCategoryNameUnique_ShouldReturnTrue_WhenUnique()
        {
            string name = "Test";
            _dbContext.Categories.Add(new Categories
            {
                CategoriesId = Guid.NewGuid(),
                CategoryName = "Test",
                Description = "Testtestest",
                ImgUrl = "Test"
            });
            await _dbContext.SaveChangesAsync();
            var result = await _repository.IsCategoryNameUnique(name);


            Assert.True(result);
        }

        [Fact]
        public async Task GetCategoryWithAccommodation_ReturnsCategoriesWithAccommodations_WhenOnlyOneCategoryResultIsFalse()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = new Categories
            {
                CategoriesId = categoryId,
                CategoryName = "Test Category"
            };

            var accommodation1 = new Accommodation { Name = "Accommodation 1", CategoryId = categoryId };
            var accommodation2 = new Accommodation { Name = "Accommodation 2", CategoryId = categoryId };

            category.Accommodations = new List<Accommodation> { accommodation1, accommodation2 };

            _dbContext.Categories.Add(category);
            _dbContext.Accommodations.AddRange(accommodation1, accommodation2);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.GetCategoryWithAccommodation(false, categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            var returnedCategory = result.First();
            Assert.Equal(category.CategoryName, returnedCategory.CategoryName);
            Assert.Equal(2, returnedCategory.Accommodations.Count);
        }

        [Fact]
        public async Task GetCategoryWithAccommodation_ReturnsOnlyCategoriesWithSpecificAccommodation_WhenOnlyOneCategoryResultIsTrue()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category1 = new Categories
            {
                CategoriesId = categoryId,
                CategoryName = "Category 1"
            };
            var category2 = new Categories
            {
                CategoriesId = Guid.NewGuid(),
                CategoryName = "Category 2"
            };

            var accommodation1 = new Accommodation { Name = "Accommodation 1", CategoryId = categoryId };
            var accommodation2 = new Accommodation { Name = "Accommodation 2", CategoryId = categoryId };
            var accommodation3 = new Accommodation { Name = "Accommodation 3", CategoryId = category2.CategoriesId };

            category1.Accommodations = new List<Accommodation> { accommodation1, accommodation2 };
            category2.Accommodations = new List<Accommodation> { accommodation3 };

            _dbContext.Categories.AddRange(category1, category2);
            _dbContext.Accommodations.AddRange(accommodation1, accommodation2, accommodation3);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.GetCategoryWithAccommodation(true, categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            var returnedCategory = result.First();
            Assert.Equal(category1.CategoryName, returnedCategory.CategoryName);
            Assert.Equal(2, returnedCategory.Accommodations.Count);
        }
    }
}


