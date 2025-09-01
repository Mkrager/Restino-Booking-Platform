using FluentValidation;
using Restino.Application.Contracts.Application;
using Restino.Application.Contracts.Persistance;

namespace Restino.Application.Features.Accommodations.Commands.UpdateAccommodation
{
    public class UpdateAccommodationCommandValidator : AbstractValidator<UpdateAccommodationCommand>
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IPermissionService _permissionService;
        public UpdateAccommodationCommandValidator(IAccommodationRepository accommodationRepository, IPermissionService permissionService)
        {
            _accommodationRepository = accommodationRepository;
            _permissionService = permissionService;

            RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0);

            RuleFor(e => e)
                .MustAsync(AccommodationNameAndCategoryUnique)
                .WithMessage("An accommodation with the same name and date already exists");
            RuleFor(e => e)
                .MustAsync(CheckUserPermissionAsync)
                .WithMessage("Permission required");

        }

        private async Task<bool> AccommodationNameAndCategoryUnique(UpdateAccommodationCommand e,
            CancellationToken cancellationToken)
        {
            return !await _accommodationRepository.IsAccommodationNameAndCategoryUniqueForUpdateAsync(e.Name, e.CategoryId, e.Id);
        }
        private async Task<bool> CheckUserPermissionAsync(UpdateAccommodationCommand e,
            CancellationToken cancellationToken)
        {
            var entity = await _accommodationRepository.GetByIdAsync(e.Id);

            return _permissionService.HasUserPermission(entity, e.UserId, e.UserRole);
        }

    }
}
