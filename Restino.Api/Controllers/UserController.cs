using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restino.Application.DTOs.Authentication;
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
    public class UserController(IMediator mediator) : Controller
    {
        [HttpGet(Name = "SearchUser")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<SearchUserResponse>>> SearchUser([FromQuery] string? userName)
        {
            var searchUserDeatailQuery = new SearchUserQuery { UserName = userName };
            return Ok(await mediator.Send(searchUserDeatailQuery));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{userId}", Name = "GetUserById")]
        public async Task<ActionResult<GetUserDetailsResponse>> GetByIdAsync(string userId)
        {
            var dtos = await mediator.Send(new GetUserDetailsQuery() { UserId = userId });
            return Ok(dtos);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet(Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<GetUserDetailsResponse>>> GetAllUsers()
        {
            var dtos = await mediator.Send(new GetUserListQuery());
            return Ok(dtos);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("[action]/{userId}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(string userId)
        {
            var deleteUserCommand = new DeleteUserCommand() { UserId = userId };
            await mediator.Send(deleteUserCommand);
            return NoContent();
        }

        [HttpPost("[action]/{email}", Name = "SendPasswordResetCode")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> SendPasswordResetCode(string email)
        {
            var sendPasswoedResetCodeQuery = new SendPasswordResetCodeQuery() { Email = email };
            await mediator.Send(sendPasswoedResetCodeQuery);
            return NoContent();
        }

        [HttpPut("[action]/{newPassword}/{email}/{code}", Name = "ChangePassword")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> ChangePassword(string email, string newPassword, string code)
        {
            var changeUserPasswordCommand = new ChangeUserPasswordCommand() { NewPassword = newPassword, Email = email, Token = code };
            await mediator.Send(changeUserPasswordCommand);
            return NoContent();
        }

        [HttpPost("[action]/{email}", Name = "SendTwoFactorAuthCode")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> SendTwoFactorAuthCode(string email)
        {
            var sendPasswoedResetCodeQuery = new SendTwoFactoreCodeQuery() { Email = email };
            await mediator.Send(sendPasswoedResetCodeQuery);
            return NoContent();
        }

        [HttpPut("[action]/{email}/{twoFactorAuthCode}", Name = "AddTwoFactorAuthCode")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> AddTwoFactorAuthCode(string email, string twoFactorAuthCode)
        {
            var sendPasswoedResetCodeQuery = new AddTwoFactorAuthCommand() { Email = email, TwoFactorCode = twoFactorAuthCode };
            await mediator.Send(sendPasswoedResetCodeQuery);
            return NoContent();
        }

        [HttpDelete("[action]/{email}/{twoFactorAuthCode}", Name = "DeleteTwoFactorAuthCode")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteTwoFactorAuthCode(string email, string twoFactorAuthCode)
        {
            var sendPasswoedResetCodeQuery = new DeleteTwoFactorAuthCommand() { Email = email, TwoFactorCode = twoFactorAuthCode };
            await mediator.Send(sendPasswoedResetCodeQuery);
            return NoContent();
        }
    }
}
