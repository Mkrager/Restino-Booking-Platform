using MediatR;

namespace Restino.Application.Features.Reservations.Queries.GetReservationList
{
    public class GetReservationListQuery : IRequest<List<ReservationListVm>>
    {        
    }
}
