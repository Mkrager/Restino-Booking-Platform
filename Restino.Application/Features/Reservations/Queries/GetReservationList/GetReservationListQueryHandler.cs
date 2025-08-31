using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Persistance;
using Restino.Domain.Entities;

namespace Restino.Application.Features.Reservations.Queries.GetReservationList
{
    public class GetReservationListQueryHandler
        : IRequestHandler<GetReservationListQuery, List<ReservationListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Domain.Entities.Reservation> _reservationRepository;
        public GetReservationListQueryHandler(IMapper mapper, IAsyncRepository<Domain.Entities.Reservation> reservationRepository)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
        }
        public async Task<List<ReservationListVm>> Handle(GetReservationListQuery request, CancellationToken cancellationToken)
        {
            var allReservation = (await _reservationRepository.ListAllAsync()).OrderBy(x => x.Id);
            return _mapper.Map<List<ReservationListVm>>(allReservation);
        }
    }
}
