using Restino.Application.Contracts.Application;
using Restino.Domain.Entities;

namespace Restino.Application.Services
{
    public class ReservationService : IReservationService
    {
        public decimal GetTotalPrice(Accommodation accommodation, DateTime checkInDate, DateTime checkOutDate)
        {
            var numberOfDays = (checkOutDate - checkInDate).TotalDays;

            var totalPrice = (decimal)numberOfDays * accommodation.Price;

            return totalPrice;
        }

        public bool IsDateRangeValid(DateTime checkInDate, DateTime checkOutDate)
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
        }
    }
}
