using MediatR;

namespace Restino.Application.Features.Accommodations.Commands.DeleteAccommodation
{
    public class DeleteAccommodationCommand : IRequest
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public string UserRole {  get; set; } = string.Empty;
    }
}
