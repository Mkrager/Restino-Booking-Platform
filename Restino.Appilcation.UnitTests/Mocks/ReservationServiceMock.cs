using Moq;
using Restino.Application.Contracts.Application;
using Restino.Domain.Entities;

namespace Restino.Appilcation.UnitTests.Mocks
{
    public class ReservationServiceMock
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
    }
}
