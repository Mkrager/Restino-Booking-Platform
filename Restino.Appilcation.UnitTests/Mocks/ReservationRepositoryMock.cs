using Moq;
using Restino.Appilcation.UnitTests.Mock;
using Restino.Application.Contracts.Persistance;
using Restino.Domain.Entities;

namespace Restino.Appilcation.UnitTests.Mocks
{
    public class ReservationRepositoryMock
    {
        public static Mock<IReservationRepository> GetReservationRepository()
        {
            var reservations = MockData.GetReservations();
            var accommodations = MockData.GetAccommodations();

            var mockReservationRepository = new Mock<IReservationRepository>();
            mockReservationRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(reservations);

            mockReservationRepository.Setup(repo => repo.AddAsync(It.IsAny<Reservation>()))
                .ReturnsAsync((Reservation reservation) =>
                {
                    reservations.Add(reservation);
                    return reservation;
                });

            mockReservationRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Reservation>()))
                .Callback((Reservation reservation) => reservations.Remove(reservation));

            mockReservationRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => reservations.FirstOrDefault(reservation => reservation.Id == id));

            mockReservationRepository.Setup(repo => repo.HasOverlapAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Guid>()))
                .ReturnsAsync((DateTime checkInDate, DateTime checkOutDate, Guid accommodationId) =>
                {
                    return reservations.Any(r =>
                    r.AccommodationId == accommodationId &&
                    checkInDate < r.CheckOutDate &&
                     checkOutDate > r.CheckInDate);
                });

            mockReservationRepository.Setup(repo => repo.IsGuestsCountWithinCapacityAsync(It.IsAny<int>(), It.IsAny<Guid>()))
                .ReturnsAsync((int guestsCount, Guid accommodationId) =>
                {
                    var accommodation = accommodations.FirstOrDefault(a => a.Id == accommodationId);
                    if (accommodation == null)
                    {
                        return false;
                    }

                    return guestsCount > accommodation.Capacity;
                });

            mockReservationRepository.Setup(repo => repo.GetReservationsWithAccommodationByUserIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string userId) => reservations.Where(r => r.CreatedBy == userId).ToList());

            return mockReservationRepository;
        }
    }
}
