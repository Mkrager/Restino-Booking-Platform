using Moq;
using Restino.Application.Contracts.Identity;
using Restino.Application.Contracts.Persistance;
using Restino.Application.DTOs.Authentication;
using Restino.Domain.Entities;

namespace Restino.Appilcation.UnitTests.Mock
{
    public class RepositoryMocks
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

            mockCategoryRepository.Setup(repo => repo.GetCategoryWithAccommodationAsync(It.IsAny<bool>(), It.IsAny<Guid>())).ReturnsAsync(categories);

            mockCategoryRepository.Setup(repo => repo.AddAsync(It.IsAny<Category>
                ())).ReturnsAsync(
                (Category category) =>
                {
                    category.Id = Guid.NewGuid();
                    categories.Add(category);
                    return category;
                });

            mockCategoryRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>
                ())).ReturnsAsync(
                (Guid id) =>
                {
                    return categories.FirstOrDefault(category => category.Id == id);
                });

            mockCategoryRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Category>())).Callback<Category>(
                (Category category) =>
                {
                    categories.Remove(category);
                }).Returns(Task.CompletedTask);

            var categoryNames = categories.Select(c => c.Name).ToList();

            mockCategoryRepository.Setup(repo => repo.IsCategoryNameUniqueAsync(It.Is<string>(name =>
                categories.Any(c => c.Name == name))))
                .ReturnsAsync(true);

            mockCategoryRepository.Setup(repo => repo.GetCategoryWithAccommodationAsync(It.IsAny<bool>(), It.IsAny<Guid?>()))
               .ReturnsAsync((bool onlyOneCategoryResult, Guid? categoryId) =>
               {
                   var allCategories = categories.AsQueryable();

                   if (onlyOneCategoryResult && categoryId.HasValue)
                   {
                       allCategories = allCategories.Where(x => x.Accommodations.Any(a => a.CategoryId == categoryId.Value));
                   }

                   return allCategories.ToList();
               });


            return mockCategoryRepository;
        }

        public static Mock<IAccommodationRepository> GetAccommodationRepository()
        {
            var accommodations = MockData.GetAccommodations();
            var categories = MockData.GetCategories();
            var mockAccommodationRepository = new Mock<IAccommodationRepository>();

            mockAccommodationRepository.Setup(repo => repo.GetAccommodationsWithCategoriesAsync(It.IsAny<bool>
                ())).ReturnsAsync(
                (bool? isAccommodationHot) =>
                {
                    if (isAccommodationHot == true)
                    {
                        return accommodations.Where(a => a.IsHotProposition == isAccommodationHot.Value).ToList();
                    }
                    return accommodations.ToList();
                });

            mockAccommodationRepository.Setup(repo => repo.AddAsync(It.IsAny<Accommodation>
            ())).ReturnsAsync(
            (Accommodation accommodation) =>
            {
                accommodation.Id = Guid.NewGuid();
                accommodations.Add(accommodation);
                return accommodation;
            });

            mockAccommodationRepository.Setup(repo =>
                    repo.IsAccommodationNameAndCategoryUniqueAsync(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync((string name, Guid categoryId) =>
                {
                    return accommodations.Any(a => a.Name == name && a.CategoryId == categoryId);
                });

            mockAccommodationRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Accommodation>())).Callback<Accommodation>(
            (Accommodation accommodation) =>
            {
                accommodations.Remove(accommodation);
            }).Returns(Task.CompletedTask);

            mockAccommodationRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>
            ())).ReturnsAsync(
            (Guid id) =>
            {
                return accommodations.FirstOrDefault(accommodation => accommodation.Id == id);
            });

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
                .ReturnsAsync((string name) =>
                {
                    var accommodations = MockData.GetAccommodations();
                    return accommodations.Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
                });

            mockAccommodationRepository.Setup(repo => repo.GetAccommodationsWithCategoriesByUserIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string userId) =>
                {
                    var userReservations = accommodations.Where(r => r.CreatedBy == userId).ToList();

                    return userReservations;
                });

            return mockAccommodationRepository;
        }

        public static Mock<IReservationRepository> GetReservationRepository()
        {
            var reservations = MockData.GetReservations();
            var accommodations = MockData.GetAccommodations();

            var mockReservationRepository = new Mock<IReservationRepository>();
            mockReservationRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(reservations);

            mockReservationRepository.Setup(repo => repo.AddAsync(It.IsAny<Reservation>
            ())).ReturnsAsync(
            (Reservation reservation) =>
            {
                reservation.Id = Guid.NewGuid();
                reservations.Add(reservation);
                return reservation;
            });

            mockReservationRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Reservation>())).Callback<Reservation>(
                (Reservation reservation) =>
                {
                    reservations.Remove(reservation);
                }).Returns(Task.CompletedTask);

            mockReservationRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>
                ())).ReturnsAsync(
                (Guid id) =>
                {
                    return reservations.FirstOrDefault(reservation => reservation.Id == id);
                });

            mockReservationRepository.Setup(repo => repo.IsDateRangeValidAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Guid>()))
                .ReturnsAsync((DateTime checkInDate, DateTime checkOutDate, Guid accommodationId) =>
                {
                    return reservations.Any(r =>
                    r.AccommodationId == accommodationId &&
                    checkInDate < r.CheckOutDate &&
                     checkOutDate > r.CheckInDate);
                });

            mockReservationRepository.Setup(repo => repo.IsGuestsCountWithinCapacityAsync(It.IsAny<int>(), It.IsAny<Guid>()))
                .ReturnsAsync((int guestsCount, Guid accommodationId) =>
                {
                    var accommodation = accommodations.FirstOrDefault(a => a.Id == accommodationId);
                    if (accommodation == null)
                    {
                        return false;
                    }

                    return guestsCount > accommodation.Capacity;
                });

            mockReservationRepository.Setup(repo => repo.GetReservationsByUserIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string userId) =>
                {
                    var userReservations = reservations.Where(r => r.CreatedBy == userId).ToList();

                    return userReservations;
                });

            return mockReservationRepository;
        }

        public static Mock<IAuthenticationService> GetAuthenticationService()
        {
            var mockAuthenticationService = new Mock<IAuthenticationService>();

            return mockAuthenticationService;
        }

        public static Mock<IUserService> GetUserService()
        {
            var users = new List<GetUserDetailsResponse>
    {
        new GetUserDetailsResponse { UserId = "35b820e5-1cea-47c8-a6a0-cedccfbda4e6", UserName = "user1", Email = "user1@example.com", FirstName = "John", LastName = "Doe" },
        new GetUserDetailsResponse { UserId = "35b820e5-1cea-47c8-a6a0-cedccfbda4e1", UserName = "user2", Email = "user2@example.com", FirstName = "John2", LastName = "Doe2" }
    };

            var mockUserService = new Mock<IUserService>();

            mockUserService
                .Setup(service => service.GetUserDetailsAsync(It.IsAny<string>()))
                .ReturnsAsync((string userId) => users.FirstOrDefault(u => u.UserId == userId));

            return mockUserService;
        }
    }
}

