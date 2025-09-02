using Restino.Application.Contracts.Application;

namespace Restino.Application.Services
{
    public class ReservationService : IReservationService
    {
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
