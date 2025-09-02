namespace Restino.Application.Contracts.Application
{
    public interface IReservationService
    {
        bool IsDateRangeValid(DateTime checkInDate, DateTime checkOutDate);

    }
}
