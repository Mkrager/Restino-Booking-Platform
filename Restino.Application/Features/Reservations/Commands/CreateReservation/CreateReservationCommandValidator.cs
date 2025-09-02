using FluentValidation;
using Restino.Application.Contracts.Application;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Services;

namespace Restino.Application.Features.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IReservationService _reservationService;
        public CreateReservationCommandValidator(IReservationRepository reservationRepository, IReservationService reservationService)
        {
            _reservationRepository = reservationRepository;
            _reservationService = reservationService;

            RuleFor(e => e.GuestsCount)
                .NotEmpty().WithMessage("{PropertyName} must be greater than 0")
                .GreaterThan(0).WithMessage("{PropertyName} must be a positive number");

            RuleFor(e => e.CustomerNote)
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(e => e)
                .MustAsync(GuestsCountMustBeWithinCapacity)
                .WithMessage("Guest count exceeds accommodation capacity");

            RuleFor(e => e)
                .MustAsync(HasNoOverlapAsync)
                .WithMessage("The selected dates overlap with an existing reservation");

            RuleFor(e => e)
                .Must(IsDateRangeValid)
                .WithMessage("Invalid date range");
        }

        private async Task<bool> GuestsCountMustBeWithinCapacity(CreateReservationCommand e, CancellationToken cancellationToken)
        {
            return !await _reservationRepository.IsGuestsCountWithinCapacityAsync(e.GuestsCount, e.AccommodationId);
        }
        private async Task<bool> HasNoOverlapAsync(CreateReservationCommand e, CancellationToken cancellationToken)
        {
            return !await _reservationRepository.IsDateRangeValidAsync(e.CheckInDate, e.CheckOutDate, e.AccommodationId);
        }
        private bool IsDateRangeValid(CreateReservationCommand e)
        {
            return !_reservationService.IsDateRangeValid(e.CheckInDate, e.CheckOutDate);
        }
    }
}
