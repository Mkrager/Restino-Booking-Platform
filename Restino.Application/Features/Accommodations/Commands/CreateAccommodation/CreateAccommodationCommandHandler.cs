using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Persistance;
using Restino.Domain.Entities;

namespace Restino.Application.Features.Accommodations.Commands.CreateAccommodation
{
    public class CreateAccommodationCommandHandler : IRequestHandler<CreateAccommodationCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Accommodation> _accommodationRepository;

        public CreateAccommodationCommandHandler(IMapper mapper, IAsyncRepository<Accommodation> accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(CreateAccommodationCommand request, CancellationToken cancellationToken)
        {
            var accommodation = _mapper.Map<Accommodation>(request);

            accommodation = await _accommodationRepository.AddAsync(accommodation);

            return accommodation.Id;
        }
    }
}