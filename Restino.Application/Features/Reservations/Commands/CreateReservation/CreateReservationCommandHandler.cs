using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Identity;
using Restino.Application.Contracts.Persistance;
using Restino.Domain.Entities;

namespace Restino.Application.Features.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;

        public CreateReservationCommandHandler(IMapper mapper, IReservationRepository reservationRepository)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
        }

        public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = _mapper.Map<Reservation>(request);

            reservation.Price = await _reservationRepository.GetTotalPriceAsync(request.AccommodationId, request.CheckInDate, request.CheckOutDate);

            reservation = await _reservationRepository.AddAsync(reservation);

            return reservation.Id;
        }
    }
}

