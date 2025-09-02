using Moq;
using Restino.Application.Contracts.Application;
using Restino.Application.Contracts.Identity;
using Restino.Application.Contracts.Persistance;
using Restino.Application.DTOs.Authentication;
using Restino.Domain.Entities;

namespace Restino.Appilcation.UnitTests.Mock
{
    public class RepositoryMocks
    {
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

            mockReservationRepository.Setup(repo => repo.HasOverlapAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Guid>()))
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

            mockReservationRepository.Setup(repo => repo.GetReservationsWithAccommodationByUserIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string userId) =>
                {
                    var userReservations = reservations.Where(r => r.CreatedBy == userId).ToList();

                    return userReservations;
                });

            return mockReservationRepository;
        }

        public static Mock<IReservationService> GetReservationService()
        {
            var mockReservationService = new Mock<IReservationService>();

            mockReservationService.Setup(service => service.IsDateRangeValid(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns((DateTime checkInDate, DateTime checkOutDate) =>
                {
                    if (checkInDate >= checkOutDate)
                        return true;

                    var currentDate = DateTime.Today;
                    if (checkInDate < currentDate)
                        return true;

                    var lastDate = currentDate.AddYears(1);
                    if (checkInDate > lastDate)
                        return true;

                    return false;
                });

            mockReservationService.Setup(service => service.GetTotalPrice(It.IsAny<Accommodation>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns((Accommodation accommodation, DateTime checkInDate, DateTime checkOutDate) =>
                {
                    var numberOfDays = (checkOutDate - checkInDate).TotalDays;

                    var totalPrice = (decimal)numberOfDays * accommodation.Price;

                    return totalPrice;
                });

            return mockReservationService;
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

