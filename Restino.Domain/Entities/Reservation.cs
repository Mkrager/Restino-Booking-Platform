using Restino.Domain.Common;

namespace Restino.Domain.Entities
{
    public class Reservation : AuditableEntity
    {
        public Guid AccommodationId { get; set; }
        public decimal ReservationPrice { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int GuestsCount { get; set; }
        public string AdditionalServices { get; set; } = string.Empty;
        public string CustomerNote { get; set; } = string.Empty;

        public Accommodation Accommodation { get; set; } = default!;
    }
}
