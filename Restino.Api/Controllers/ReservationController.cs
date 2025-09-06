using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restino.Application.Contracts;
using Restino.Application.Features.Reservations.Commands.CreateReservation;
using Restino.Application.Features.Reservations.Commands.DeleteReservation;
using Restino.Application.Features.Reservations.Queries.GetReservatioDetails;
using Restino.Application.Features.Reservations.Queries.GetReservationList;
using Restino.Application.Features.Reservations.Queries.GetUserReservations;

namespace Restino.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController(IMediator mediator, ICurrentUserService currentUserService) : Controller
    {
        [Authorize]
        [HttpGet(Name = "GetAllReservation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<ReservationListVm>>> GetAllReservations()
        {
            var dtos = await mediator.Send(new GetReservationListQuery());
            return Ok(dtos);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{reservationsId}", Name = "GetReservationById")]
        public async Task<ActionResult<ReservationDetailsVm>> GetReservationById(Guid reservationsId)
        {
            var getReservationDetailsQuery = new GetReservationDetailQuery() { Id = reservationsId };
            return Ok(await mediator.Send(getReservationDetailsQuery));
        }

        [Authorize]
        [HttpPost(Name = "AddReservation")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateReservationCommand createReservationCommand)
        {
            var response = await mediator.Send(createReservationCommand);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}", Name = "DeleteReservation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var userId = currentUserService.UserId;
            var userRole = currentUserService.UserRole;

            var deleteReservationCommand = new DeleteReservationCommand() 
            { 
                Id = id, 
                UserId = userId, 
                UserRole = userRole
            };

            await mediator.Send(deleteReservationCommand);
            return NoContent();
        }

        [Authorize]
        [HttpGet("{userId}/user", Name = "GetUserReservations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<GetUserReservationListVm>>> GetUserReservations(string userId)
        {
            var dtos = await mediator.Send(new GetUserReservationListQuery { UserId = userId });
            return Ok(dtos);
        }
    }
}
