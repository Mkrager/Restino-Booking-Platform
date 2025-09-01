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
    public class AccommodationController(IMediator mediator, ICurrentUserService currentUserService) : Controller
    {
        [HttpGet(Name = "GetAllAccommodations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AccommodationListVm>>> GetAllAccommodations([FromQuery] bool isAccommodationHot)
        {
            var dtos = await mediator.Send(new GetAccommodationListQuery() { IsAccommodationHot = isAccommodationHot });
            return Ok(dtos);
        }

        [Authorize]
        [HttpGet("[action]/{userId}", Name = "GetUserAccommodations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AccommodationListVm>>> GetUserAccommodations(string? userId)
        {
            var dtos = await mediator.Send(new GetUserAccommodationListQuery() { UserId = userId });
            return Ok(dtos);
        }

        [HttpGet("{AccommodationId}", Name = "GetAccommodationById")]
        public async Task<ActionResult<AccommodationDetailsVm>> GetAccommodationById(Guid AccommodationId)
        {
            var getAccommodationDeatailQuery = new GetAccommodationDetailsQuery() { Id = AccommodationId };
            return Ok(await mediator.Send(getAccommodationDeatailQuery));
        }


        [HttpGet("search", Name = "SearchAccommodation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AccommodationListVm>>> SearchAccommodation([FromQuery] string? searchString)
        {
            var query = new SearchAccommodationListQuery { SearchString = searchString };
            return Ok(await mediator.Send(query));
        }

        [Authorize]
        [HttpPost(Name = "AddAccommodation")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateAccommodationCommand createAccommodationCommand)
        {
            var response = await mediator.Send(createAccommodationCommand);
            return Ok(response);
        }

        [Authorize]
        [HttpPut(Name = "UpdateAccommdation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult> Update([FromBody] UpdateAccommodationCommand
            updateAccommodationCommand)
        {
            var userId = currentUserService.UserId;
            var userRole = currentUserService.UserRole;

            updateAccommodationCommand.UserId = userId;
            updateAccommodationCommand.UserRole = userRole;

            var response = await mediator.Send(updateAccommodationCommand);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}", Name = "DeleteAccommodation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var userId = currentUserService.UserId;
            var userRole = currentUserService.UserRole;

            var deleteAccommodationCommand = new DeleteAccommodationCommand() { Id = id, UserId = userId, UserRole = userRole };
            await mediator.Send(deleteAccommodationCommand);
            return NoContent();
        }
    }
}
