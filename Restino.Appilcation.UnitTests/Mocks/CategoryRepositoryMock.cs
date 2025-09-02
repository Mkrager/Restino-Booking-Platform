using Moq;
using Restino.Appilcation.UnitTests.Mock;
using Restino.Application.Contracts.Persistance;
using Restino.Domain.Entities;
using System.Xml.Linq;

namespace Restino.Appilcation.UnitTests.Mocks
{
    public class CategoryRepositoryMock
    {
        public static Mock<ICategoryRepository> GetCategoryRepository()
        {
            var categories = MockData.GetCategories();
            var accommodations = MockData.GetAccommodations();

            foreach (var category in categories)
            {
                category.Accommodations = accommodations.Where(a => a.CategoryId == category.Id).ToList();
            }

            var mockCategoryRepository = new Mock<ICategoryRepository>();

            mockCategoryRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(categories);


            mockCategoryRepository.Setup(repo => repo.AddAsync(It.IsAny<Category>()))
                .ReturnsAsync((Category category) =>
                {
                    categories.Add(category);
                    return category;
                });

            mockCategoryRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>  categories.FirstOrDefault(x => x.Id == id));

            mockCategoryRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Category>()))
                .Callback((Category category) => categories.Remove(category));

            mockCategoryRepository.Setup(repo => repo.IsCategoryNameUniqueAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) => categories.Any(c => c.Name == name));

            mockCategoryRepository.Setup(repo => repo.GetCategoryWithAccommodationAsync(It.IsAny<bool>(), It.IsAny<Guid?>()))
               .ReturnsAsync((bool onlyOneCategoryResult, Guid? categoryId) =>
               {
                   var filtered = categories;

                   if (onlyOneCategoryResult && categoryId.HasValue)
                   {
                       filtered = filtered
                           .Where(c => c.Accommodations.Any(a => a.CategoryId == categoryId.Value))
                           .ToList();
                   }

                   return filtered;
               });

            return mockCategoryRepository;
        }
    }
}
