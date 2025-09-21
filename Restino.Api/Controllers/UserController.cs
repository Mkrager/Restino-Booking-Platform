using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restino.Application.Contracts;
using Restino.Application.Features.User.Commands.DeleteUser;
using Restino.Application.Features.User.Queries.GetUserDetails;
using Restino.Application.Features.User.Queries.GetUserList;
using Restino.Application.Features.User.Queries.SearchUser;

namespace Restino.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(IMediator mediator, ICurrentUserService currentUserService) : ControllerBase
    {
        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchUser([FromQuery] string? userName)
        {
            var query = new SearchUserQuery { UserName = userName };
            var result = await mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var result = await mediator.Send(new GetUserDetailsQuery
            {
                UserId = userId
            });

            return Ok(result);
        }

        [HttpGet("details")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserDetails()
        {
            var result = await mediator.Send(new GetUserDetailsQuery
            {
                UserId = currentUserService.UserId
            });

            return Ok(result);
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await mediator.Send(new GetUserListQuery());
            return Ok(result);
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string userId)
        {
            await mediator.Send(new DeleteUserCommand { UserId = userId });
            return NoContent();
        }
    }
}