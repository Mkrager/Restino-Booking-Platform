using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restino.Application.Contracts;
using Restino.Application.Features.TwoFactor.Commands.AddTwoFactorAuth;
using Restino.Application.Features.TwoFactor.Commands.DeleteTwofActorAuth;
using Restino.Application.Features.TwoFactor.Queries.SendTwoFactoreCode;

namespace Restino.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TwoFactorController(IMediator mediator, ICurrentUserService currentUserService) : Controller
    {
        [HttpPost("send")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendTwoFactorAuthCode([FromBody] SendTwoFactoreCodeQuery query)
        {
            await mediator.Send(query);
            return NoContent();
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTwoFactorAuth([FromBody] AddTwoFactorAuthCommand command)
        {
            command.UserId = currentUserService.UserId;

            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("remove")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTwoFactorAuth([FromBody] DeleteTwoFactorAuthCommand command)
        {
            command.UserId = currentUserService.UserId;

            await mediator.Send(command);
            return NoContent();
        }
    }
}