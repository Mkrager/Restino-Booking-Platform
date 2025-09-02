using Moq;
using Restino.Appilcation.UnitTests.Mock;
using Restino.Application.Contracts.Persistance;
using Restino.Domain.Entities;

namespace Restino.Appilcation.UnitTests.Mocks
{
    public class AccommodationRepositoryMock
    {
        public static Mock<IAccommodationRepository> GetAccommodationRepository()
        {
            var accommodations = MockData.GetAccommodations();
            var categories = MockData.GetCategories();

            var mockAccommodationRepository = new Mock<IAccommodationRepository>();

            mockAccommodationRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(accommodations);

            mockAccommodationRepository.Setup(repo => repo.GetAccommodationsWithCategoriesAsync(It.IsAny<bool>())).
                ReturnsAsync( (bool? isAccommodationHot) =>
                {
                    if (isAccommodationHot == true)
                    {
                        return accommodations.Where(a => a.IsHotProposition == isAccommodationHot.Value).ToList();
                    }
                    return accommodations.ToList();
                });

            mockAccommodationRepository.Setup(repo => repo.AddAsync(It.IsAny<Accommodation>()))
                .ReturnsAsync((Accommodation accommodation) =>
                {
                    accommodations.Add(accommodation);
                    return accommodation;
                });

            mockAccommodationRepository.Setup(repo => repo.IsAccommodationNameAndCategoryUniqueAsync(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync((string name, Guid categoryId) =>
                {
                    return accommodations.Any(a => a.Name == name && a.CategoryId == categoryId);
                });

            mockAccommodationRepository.Setup(repo => repo.GetAccommodationWithCategoryByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>  accommodations.FirstOrDefault(a => a.Id == id));

            mockAccommodationRepository.Setup(r => r.DeleteAsync(It.IsAny<Accommodation>()))
                .Callback((Accommodation accommodation) => accommodations.Remove(accommodation));

            mockAccommodationRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => accommodations.FirstOrDefault(accommodation => accommodation.Id == id));

            mockAccommodationRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Accommodation>()))
                .Callback((Accommodation accommodation) =>
                {
                    var existingAccommodation = accommodations.FirstOrDefault(a => a.Id == accommodation.Id);
                    if (existingAccommodation != null)
                    {
                        existingAccommodation.Name = accommodation.Name;
                        existingAccommodation.Address = accommodation.Address;
                        existingAccommodation.Capacity = accommodation.Capacity;
                        existingAccommodation.CategoryId = accommodation.CategoryId;
                        existingAccommodation.ShortDescription = accommodation.ShortDescription;
                        existingAccommodation.ImgUrl = accommodation.ImgUrl;
                        existingAccommodation.Price = accommodation.Price;
                    }
                });

            mockAccommodationRepository.Setup(repo => repo.SearchAccommodationAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) => accommodations.Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList());

            mockAccommodationRepository.Setup(repo => repo.GetAccommodationsWithCategoriesByUserIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string userId) => accommodations.Where(r => r.CreatedBy == userId).ToList());

            return mockAccommodationRepository;
        }
    }
}
