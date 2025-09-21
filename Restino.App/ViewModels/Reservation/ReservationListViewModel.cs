using Restino.App.ViewModels.Accommodation;

namespace Restino.App.ViewModels.Reservation
{
    public class ReservationListViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public Guid AccommodationId { get; set; }
        public double ReservationPrice { get; set; }
        public DateTime ReservationCreated { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int GuestsCount { get; set; }
        public string AdditionalServices { get; set; } = string.Empty;
        public string CustomerNote { get; set; } = string.Empty;
        public AccommodationListViewModel Accommodation { get; set; } = default!;
    }
}
