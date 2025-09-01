using FluentValidation;

namespace Restino.Application.Features.Accommodations.Queries.SearchAccommodationList
{
    public class SearchAccommodationQueryValidator : AbstractValidator<SearchAccommodationListQuery>
    {
        public SearchAccommodationQueryValidator()
        {
            RuleFor(a => a.SearchString)
            .NotEmpty()
            .WithMessage("Accommodation name must not be empty.")
            .MinimumLength(3)
            .WithMessage("Accommodation name must be at least 3 characters long.");
        }
    }
}
