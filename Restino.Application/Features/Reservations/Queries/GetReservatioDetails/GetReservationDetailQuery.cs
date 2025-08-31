using MediatR;

namespace Restino.Application.Features.Reservations.Queries.GetReservatioDetails
{
    public class GetReservationDetailQuery : IRequest<ReservationDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
