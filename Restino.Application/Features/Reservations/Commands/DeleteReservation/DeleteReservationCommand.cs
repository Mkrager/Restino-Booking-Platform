using MediatR;

namespace Restino.Application.Features.Reservations.Commands.DeleteReservation
{
    public class DeleteReservationCommand : IRequest
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserRole {  get; set; } = string.Empty;
    }
}
