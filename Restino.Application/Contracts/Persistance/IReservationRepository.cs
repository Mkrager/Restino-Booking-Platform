using Restino.Domain.Entities;

namespace Restino.Application.Contracts.Persistance
{
    public interface IReservationRepository : IAsyncRepository<Reservation>
    {
        Task<bool> IsGuestsCountWithinCapacity(int guestsCount, Guid accommodationId);
        Task<bool> IsDateRangeValid(DateTime checkInDate, DateTime checkOutDate, Guid accommodationId);
        Task<decimal> TotalPrice(Guid accommodationId, DateTime checkInDate, DateTime checkOutDate);
        Task<List<Reservation>> ListUserReservations(string userId);
    }
}
