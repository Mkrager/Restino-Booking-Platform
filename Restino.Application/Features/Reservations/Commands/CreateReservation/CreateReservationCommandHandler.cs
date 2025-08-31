using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Identity;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Exceptions;

namespace Restino.Application.Features.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserService _userService;

        public CreateReservationCommandHandler(IMapper mapper, IReservationRepository reservationRepository, IUserService userService)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _userService = userService;
        }

        public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = new Domain.Entities.Reservation()
            {
                AccommodationId = request.AccommodationId,
                AdditionalServices = request.AdditionalServices,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                CustomerNote = request.CustomerNote,
                GuestsCount = request.GuestsCount,
                ReservationPrice = await _reservationRepository.TotalPrice(request.AccommodationId, request.CheckInDate, request.CheckOutDate),
            };

            reservation = await _reservationRepository.AddAsync(reservation);

            reservation = _mapper.Map<Domain.Entities.Reservation>(reservation);

            return reservation.Id;
        }
    }
}

