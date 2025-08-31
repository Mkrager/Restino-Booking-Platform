namespace Restino.Application.Features.Reservations.Queries.GetReservatioDetails
{
    public class ReservationDetailsVm
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public Guid AccommodationId { get; set; }
        public int ReservationPrice { get; set; }
        public DateTime ReservationCreated { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int GuestsCount { get; set; }
        public string AdditionalServices { get; set; } = string.Empty;
        public string CustomerNote { get; set; } = string.Empty;

    }
}
