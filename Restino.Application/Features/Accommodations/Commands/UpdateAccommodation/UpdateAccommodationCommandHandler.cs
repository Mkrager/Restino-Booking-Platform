using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Exceptions;
using Restino.Domain.Entities;

namespace Restino.Application.Features.Accommodations.Commands.UpdateAccommodation
{
    public class UpdateAccommodationCommandHandler : IRequestHandler<UpdateAccommodationCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAccommodationRepository _accommodationRepository;

        public UpdateAccommodationCommandHandler(IMapper mapper, IAccommodationRepository accommodationRepository)
        {
            _mapper = mapper;
            _accommodationRepository = accommodationRepository;
        }

        public async Task<Unit> Handle(UpdateAccommodationCommand request, CancellationToken cancellationToken)
        {
            var accommodationToUpdate = await _accommodationRepository.GetByIdAsync(request.Id);

            if (accommodationToUpdate == null)
                throw new NotFoundException(nameof(Accommodation), request.Id);

            _mapper.Map(request, accommodationToUpdate, typeof(UpdateAccommodationCommand), typeof(Accommodation));

            await _accommodationRepository.UpdateAsync(accommodationToUpdate);

            return Unit.Value;
        }
    }
}
