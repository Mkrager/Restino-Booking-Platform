using Restino.Domain.Entities;

namespace Restino.Application.Contracts.Persistance
{
    public interface IReservationRepository : IAsyncRepository<Reservation>
    {
        Task<bool> IsGuestsCountWithinCapacityAsync(int guestsCount, Guid accommodationId);
        Task<bool> HasOverlapAsync(DateTime checkInDate, DateTime checkOutDate, Guid accommodationId);
        Task<List<Reservation>> GetReservationsWithAccommodationByUserIdAsync(string userId);
    }
}
