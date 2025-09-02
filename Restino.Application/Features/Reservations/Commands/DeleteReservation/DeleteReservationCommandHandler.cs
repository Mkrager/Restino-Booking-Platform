using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Exceptions;
using Restino.Domain.Entities;

namespace Restino.Application.Features.Reservations.Commands.DeleteReservation
{
    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Reservation> _reservationRepository;
        public DeleteReservationCommandHandler(IMapper mapper, IAsyncRepository<Reservation> reservationRepository)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository; 
        }
        public async Task<Unit> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservationToDelete = await _reservationRepository.GetByIdAsync(request.Id);

            if (reservationToDelete == null)
                throw new NotFoundException(nameof(Accommodation), request.Id);

            await _reservationRepository.DeleteAsync(reservationToDelete);

            return Unit.Value;
        }
    }
}