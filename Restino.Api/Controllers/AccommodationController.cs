using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restino.Application.Contracts;
using Restino.Application.Features.Accommodations.Commands.CreateAccommodation;
using Restino.Application.Features.Accommodations.Commands.DeleteAccommodation;
using Restino.Application.Features.Accommodations.Commands.UpdateAccommodation;
using Restino.Application.Features.Accommodations.Queries.GetAccommodationDetails;
using Restino.Application.Features.Accommodations.Queries.GetAccommodationList;
using Restino.Application.Features.Accommodations.Queries.GetUserAccommodationList;
using Restino.Application.Features.Accommodations.Queries.SearchAccommodationList;

namespace Restino.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccommodationController(IMediator mediator, ICurrentUserService currentUserService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AccommodationListVm>>> GetAllAccommodations([FromQuery] bool isAccommodationHot)
        {
            var dtos = await mediator.Send(new GetAccommodationListQuery { IsAccommodationHot = isAccommodationHot });
            return Ok(dtos);
        }

        [Authorize]
        [HttpGet("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AccommodationListVm>>> GetUserAccommodations()
        {
            var dtos = await mediator.Send(new GetUserAccommodationListQuery { UserId = currentUserService.UserId });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AccommodationDetailsVm>> GetAccommodationById(Guid id)
        {
            var dto = await mediator.Send(new GetAccommodationDetailsQuery { Id = id });
            return Ok(dto);
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<AccommodationListVm>>> SearchAccommodation([FromQuery] string? searchString)
        {
            var dtos = await mediator.Send(new SearchAccommodationListQuery { SearchString = searchString });
            return Ok(dtos);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateAccommodationCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromBody] UpdateAccommodationCommand command)
        {
            command.UserId = currentUserService.UserId;
            command.UserRole = currentUserService.UserRole;

            var response = await mediator.Send(command);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteAccommodationCommand
            {
                Id = id,
                UserId = currentUserService.UserId,
                UserRole = currentUserService.UserRole
            };

            await mediator.Send(command);
            return NoContent();
        }
    }
}