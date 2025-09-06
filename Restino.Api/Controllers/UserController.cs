using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restino.Application.Features.User.Commands.AddTwoFactorAuth;
using Restino.Application.Features.User.Commands.ChangeUserPassword;
using Restino.Application.Features.User.Commands.DeleteTwofActorAuth;
using Restino.Application.Features.User.Commands.DeleteUser;
using Restino.Application.Features.User.Queries.GetUserDetails;
using Restino.Application.Features.User.Queries.GetUserList;
using Restino.Application.Features.User.Queries.SearchUser;
using Restino.Application.Features.User.Queries.SendPasswoedResetCode;
using Restino.Application.Features.User.Queries.SendTwoFactoreCode;

namespace Restino.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
        public async Task<IActionResult> GetById(string userId)
        {
            var result = await mediator.Send(new GetUserDetailsQuery { UserId = userId });
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

        [HttpPost("password/reset-code")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendPasswordResetCode([FromBody] SendPasswordResetCodeQuery query)
        {
            await mediator.Send(query);
            return NoContent();
        }

        [HttpPut("password/change")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("2fa/send")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendTwoFactorAuthCode([FromBody] SendTwoFactoreCodeQuery query)
        {
            await mediator.Send(query);
            return NoContent();
        }

        [HttpPost("2fa/add")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTwoFactorAuth([FromBody] AddTwoFactorAuthCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("2fa/remove")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTwoFactorAuth([FromBody] DeleteTwoFactorAuthCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}