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
    public class ReservationController(IMediator mediator, ICurrentUserService currentUserService) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ReservationListVm>>> GetAllReservations()
        {
            var dtos = await mediator.Send(new GetReservationListQuery());
            return Ok(dtos);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}", Name = "GetReservationById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReservationDetailsVm>> GetReservationById(Guid id)
        {
            var dto = await mediator.Send(new GetReservationDetailQuery { Id = id });
            return Ok(dto);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateReservationCommand command)
        {
            var reservationId = await mediator.Send(command);
            return Ok(reservationId);
        }


        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await mediator.Send(new DeleteReservationCommand
            {
                Id = id,
                UserId = currentUserService.UserId,
                UserRole = currentUserService.UserRole
            });

            return NoContent();
        }

        [Authorize]
        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<GetUserReservationListVm>>> GetUserReservations(string userId)
        {
            var dtos = await mediator.Send(new GetUserReservationListQuery { UserId = userId });
            return Ok(dtos);
        }
    }
}