using Microsoft.EntityFrameworkCore;
using Restino.Application.Contracts.Persistance;
using Restino.Domain.Entities;

namespace Restino.Persistence.Repositories
{
    public class ReservationRepository : BaseRepositrory<Reservation>, IReservationRepository
    {
        public ReservationRepository(RestinoDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Reservation>> GetReservationsByUserIdAsync(string userId)
        {
            var userReservation = _dbContext.Reservation.Where(e => e.CreatedBy == userId);
            return await userReservation.ToListAsync();
        }

        public async Task<bool> HasOverlapAsync(DateTime checkInDate, DateTime checkOutDate, Guid accommodationId)
        {
            var isOverlap = await _dbContext.Reservation
                .Where(r => r.AccommodationId == accommodationId)
                .AnyAsync(r =>
                    (checkInDate < r.CheckOutDate && checkOutDate > r.CheckInDate) 
                );

            return isOverlap;
        }

        public async Task<bool> IsGuestsCountWithinCapacityAsync(int guestsCount, Guid accommodationId)
        {
            var exceedsCapacity = await _dbContext.Accommodations
                .Where(e => e.Id == accommodationId)
                .AnyAsync(e => guestsCount > e.Capacity);

            return exceedsCapacity;
        }
    }
}