using MediatR;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Exceptions;
using Restino.Domain.Entities;

namespace Restino.Application.Features.Accommodations.Commands.DeleteAccommodation
{
    public class DeleteAccommodationCommandHandler : IRequestHandler<DeleteAccommodationCommand>
    {
        private readonly IAsyncRepository<Accommodation> _accommodationRepository;

        public DeleteAccommodationCommandHandler(IAsyncRepository<Accommodation> accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
        }

        public async Task<Unit> Handle(DeleteAccommodationCommand request, CancellationToken cancellationToken)
        {
            var accommodationToDelete = await _accommodationRepository.GetByIdAsync(request.Id);

            if (accommodationToDelete == null)
                throw new NotFoundException(nameof(Accommodation), request.Id);

            await _accommodationRepository.DeleteAsync(accommodationToDelete);

            return Unit.Value;
        }
    }
}