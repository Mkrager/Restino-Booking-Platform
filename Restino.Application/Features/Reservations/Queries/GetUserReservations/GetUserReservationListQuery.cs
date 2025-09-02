using MediatR;

namespace Restino.Application.Features.Reservations.Queries.GetUserReservations
{
    public class GetUserReservationListQuery : IRequest<List<GetUserReservationListVm>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
