using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Application;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Exceptions;
using Restino.Domain.Entities;

namespace Restino.Application.Features.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IReservationService _reservationService;
        private readonly IAsyncRepository<Reservation> _reservationRepository;
        private readonly IAsyncRepository<Accommodation> _accommodationRepository;
        public CreateReservationCommandHandler(
            IMapper mapper,
            IAsyncRepository<Reservation> reservationRepository,
            IAsyncRepository<Accommodation> accommodationRepository,
            IReservationService reservationService)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _accommodationRepository = accommodationRepository;
            _reservationService = reservationService;
        }

        public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = _mapper.Map<Reservation>(request);

            var accommodation = await _accommodationRepository.GetByIdAsync(request.AccommodationId);

            if (accommodation == null)
                throw new NotFoundException(nameof(Accommodation), request.AccommodationId);

            reservation.Price = _reservationService.GetTotalPrice(accommodation, request.CheckInDate, request.CheckOutDate);

            reservation = await _reservationRepository.AddAsync(reservation);

            return reservation.Id;
        }
    }
}

