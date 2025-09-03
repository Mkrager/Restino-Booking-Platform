using Moq;
using Restino.Application.Contracts.Application;
using Restino.Application.Contracts.Identity;
using Restino.Application.DTOs.Authentication;
using Restino.Domain.Entities;

namespace Restino.Appilcation.UnitTests.Mock
{
    public class RepositoryMocks
    {
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

