using MediatR;

namespace Restino.Application.Features.Reservations.Queries.ListUserReservations
{
    public class GetUserReservationListQuery : IRequest<List<GetUserReservationListVm>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
