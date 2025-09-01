using MediatR;

namespace Restino.Application.Features.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommand : IRequest<Guid>
    {
        public Guid AccommodationId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int GuestsCount { get; set; }
        public string AdditionalServices { get; set; } = string.Empty;
        public string CustomerNote { get; set; } = string.Empty;
    }
}
