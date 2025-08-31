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

        public Task<List<Reservation>> ListUserReservations(string userId)
        {
            var userReservation = _dbContext.Reservation.Where(e => e.CreatedBy == userId);
            return userReservation.ToListAsync();
        }

        public async Task<bool> IsDateRangeValid(DateTime checkInDate, DateTime checkOutDate, Guid accommodationId)
        {

            if (checkInDate >= checkOutDate)
            {
                return true;
            }

            var currentDate = DateTime.Today;
            if (checkInDate < currentDate)
            {
                return true;
            }

            var lastDate = currentDate.AddYears(1);
            if (checkInDate > lastDate)
            {
                return true;
            }

            var isOverlap = await _dbContext.Reservation
                .Where(r => r.AccommodationId == accommodationId)
                .AnyAsync(r =>
                    (checkInDate < r.CheckOutDate && checkOutDate > r.CheckInDate) 
                );

            return isOverlap;
        }


        public Task<bool> IsGuestsCountWithinCapacity(int guestsCount, Guid accommodationId)
        {
            var exceedsCapacity = _dbContext.Accommodations
                .Where(e => e.Id == accommodationId)
                .Any(e => guestsCount > e.Capacity);

            return Task.FromResult(exceedsCapacity);
        }

        public async Task<decimal> TotalPrice(Guid accommodationId, DateTime checkInDate, DateTime checkOutDate)
        {
            var accommodation = await _dbContext.Accommodations
                .FirstOrDefaultAsync(a => a.Id == accommodationId);

            var numberOfDays = (checkOutDate - checkInDate).TotalDays;

            var totalPrice = (decimal)numberOfDays * accommodation.Price;

            return totalPrice;
        }
    }
}
