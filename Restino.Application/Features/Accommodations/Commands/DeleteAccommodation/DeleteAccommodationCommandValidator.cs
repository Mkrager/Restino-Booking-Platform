using FluentValidation;
using Restino.Application.Contracts.Application;
using Restino.Application.Contracts.Persistance;
using Restino.Domain.Entities;

namespace Restino.Application.Features.Accommodations.Commands.DeleteAccommodation
{
    public class DeleteAccommodationCommandValidator : AbstractValidator<DeleteAccommodationCommand>
    {
        private readonly IAsyncRepository<Accommodation> _accommodationRepository;
        private readonly IPermissionService _permissionService;
        public DeleteAccommodationCommandValidator(IAsyncRepository<Accommodation> accommodationRepository, IPermissionService permissionService)
        {
            _accommodationRepository = accommodationRepository;
            _permissionService = permissionService;

            RuleFor(e => e)
                .MustAsync(CheckUserPermissionAsync)
                .WithMessage("Permission required");
        }

        private async Task<bool> CheckUserPermissionAsync(DeleteAccommodationCommand e,
            CancellationToken cancellationToken)
        {
            var entity = await _accommodationRepository.GetByIdAsync(e.Id);

            return _permissionService.HasUserPermission(entity, e.UserId, e.UserRole);
        }
    }
}
