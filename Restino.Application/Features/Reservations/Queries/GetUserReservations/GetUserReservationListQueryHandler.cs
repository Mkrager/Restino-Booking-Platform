﻿using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Persistance;

namespace Restino.Application.Features.Reservations.Queries.GetUserReservations
{
    public class GetUserReservationListQueryHandler : IRequestHandler<GetUserReservationListQuery, List<GetUserReservationListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;
        public GetUserReservationListQueryHandler(IMapper mapper, IReservationRepository reservationRepository)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
        }

        public async Task<List<GetUserReservationListVm>> Handle(GetUserReservationListQuery request, CancellationToken cancellationToken)
        {
            var userReservation = await _reservationRepository.GetReservationsWithAccommodationByUserIdAsync(request.UserId);
            return _mapper.Map<List<GetUserReservationListVm>>(userReservation);
        }
    }
}
