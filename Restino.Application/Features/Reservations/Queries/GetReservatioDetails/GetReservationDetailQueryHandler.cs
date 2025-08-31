using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Persistance;
using Restino.Domain.Entities;

namespace Restino.Application.Features.Reservations.Queries.GetReservatioDetails
{
    public class GetReservationDetailQueryHandler
        : IRequestHandler<GetReservationDetailQuery, ReservationDetailsVm>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Reservation> _reservationRepository;
        public GetReservationDetailQueryHandler(IMapper mapper, IAsyncRepository<Reservation> reservationRepository)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
        }
        public async Task<ReservationDetailsVm> Handle(GetReservationDetailQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.Id);
            return _mapper.Map<ReservationDetailsVm>(reservation);
        }
    }
}
