using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Persistance;

namespace Restino.Application.Features.Reservations.Queries.ListUserReservations
{
    public class GetUserReservationListQueryHandler : IRequestHandler<GetUserReservationListQuery, List<GetUserReservationListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;
        private readonly IAccommodationRepository _accommodationRepository;
        public GetUserReservationListQueryHandler(IMapper mapper, IReservationRepository reservationRepository, IAccommodationRepository accommodationRepository)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _accommodationRepository = accommodationRepository;
        }

        public async Task<List<GetUserReservationListVm>> Handle(GetUserReservationListQuery request, CancellationToken cancellationToken)
        {
            var userReservation = (await _reservationRepository.ListUserReservations(request.UserId)).OrderBy(x => x.CreatedBy);

            var userReservationDetailsDto = _mapper.Map<List<GetUserReservationListVm>>(userReservation);

            var accommodations = await _accommodationRepository.ListAllAsync();
            foreach (var reservation in userReservationDetailsDto)
            {
                reservation.Accommodation = _mapper.Map<AccommodationDtoReservation>(accommodations.FirstOrDefault(c => c.Id == reservation.AccommodationId));
            }

            return userReservationDetailsDto;
        }
    }
}
