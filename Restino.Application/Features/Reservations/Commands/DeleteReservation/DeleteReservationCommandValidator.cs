using FluentValidation;
using Restino.Application.Contracts.Application;
using Restino.Application.Contracts.Persistance;
using Restino.Domain.Entities;

namespace Restino.Application.Features.Reservations.Commands.DeleteReservation
{
    public class DeleteReservationCommandValidator : AbstractValidator<DeleteReservationCommand>
    {
        private readonly IAsyncRepository<Reservation> _reservationRepository;
        private readonly IPermissionService _permissionService;
        public DeleteReservationCommandValidator(IAsyncRepository<Reservation> reservationRepository, IPermissionService permissionService)
        {
            _reservationRepository = reservationRepository;
            _permissionService = permissionService;

            RuleFor(e => e)
                .MustAsync(CheckUserPermissionAsync)
                .WithMessage("Permission required");
        }
        private async Task<bool> CheckUserPermissionAsync(DeleteReservationCommand e,
            CancellationToken cancellationToken)
        {
            var entity = await _reservationRepository.GetByIdAsync(e.Id);

            return _permissionService.HasUserPermission(entity, e.UserId, e.UserRole);
        }
    }
}
