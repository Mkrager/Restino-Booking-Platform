using FluentValidation;
using Restino.Application.Contracts.Persistance;
using Restino.Domain.Entities;

namespace Restino.Application.Features.Accommodations.Commands.DeleteAccommodation
{
    public class DeleteAccommodationCommandValidator : AbstractValidator<DeleteAccommodationCommand>
    {
        private readonly IAsyncRepository<Domain.Entities.Accommodation> _accommodationRepository;
        public DeleteAccommodationCommandValidator(IAsyncRepository<Domain.Entities.Accommodation> accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;

            RuleFor(e => e)
                .MustAsync(CheckUserPermissionAsync)
                .WithMessage("Permission required");
        }

        private async Task<bool> CheckUserPermissionAsync(DeleteAccommodationCommand e,
            CancellationToken cancellationToken)
        {
            return await _accommodationRepository.CheckUserPermissionAsync(e.UserId, e.Id, e.UserRole);
        }
    }
}
