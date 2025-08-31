using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Exceptions;
using Restino.Domain.Entities;

namespace Restino.Application.Features.Accommodations.Commands.DeleteAccommodation
{
    public class DeleteAccommodationCommandHandler : IRequestHandler<DeleteAccommodationCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Accommodation> _accommodationRepository;

        public DeleteAccommodationCommandHandler(IMapper mapper, IAsyncRepository<Accommodation> accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteAccommodationCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteAccommodationCommandValidator(_accommodationRepository);
            var validatorResult = await validator.ValidateAsync(request);

            if (validatorResult.Errors.Count > 0)
                throw new ValidationException(validatorResult);

            var accommodationToDelete = await _accommodationRepository.GetByIdAsync(request.Id);
            await _accommodationRepository.DeleteAsync(accommodationToDelete);

            return Unit.Value;
        }
    }
}
