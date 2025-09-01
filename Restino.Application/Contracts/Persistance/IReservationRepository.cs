using Restino.Domain.Entities;

namespace Restino.Application.Contracts.Persistance
{
    public interface IReservationRepository : IAsyncRepository<Reservation>
    {
        Task<bool> IsGuestsCountWithinCapacityAsync(int guestsCount, Guid accommodationId);
        Task<bool> IsDateRangeValidAsync(DateTime checkInDate, DateTime checkOutDate, Guid accommodationId);
        Task<decimal> GetTotalPriceAsync(Guid accommodationId, DateTime checkInDate, DateTime checkOutDate);
        Task<List<Reservation>> GetReservationsByUserIdAsync(string userId);
    }
}
