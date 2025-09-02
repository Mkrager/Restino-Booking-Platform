using Restino.Domain.Entities;

namespace Restino.Application.Contracts.Application
{
    public interface IReservationService
    {
        bool IsDateRangeValid(DateTime checkInDate, DateTime checkOutDate);
        decimal GetTotalPrice(Accommodation accommodation, DateTime checkInDate, DateTime checkOutDate);

    }
}
