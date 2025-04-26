using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Features.Reservation.Queries.ListUserReservations;

namespace Restino.Application.Features.Reservation.Queries.GetUserReservations
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
            var userReservation = (await _reservationRepository.ListUserReservations(request.UserId)).OrderBy(x => x.UserId);

            var userReservationDetailsDto = _mapper.Map<List<GetUserReservationListVm>>(userReservation);

            var accommodations = await _accommodationRepository.ListAllAccommodations(false);
            foreach(var reservation in userReservationDetailsDto)
            {
                reservation.Accommodation = _mapper.Map<AccommodationDtoReservation>(accommodations.FirstOrDefault(c => c.AccommodationsId == reservation.AccommodationsId));
            }

            return userReservationDetailsDto;
        }
    }
}
