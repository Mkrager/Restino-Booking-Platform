using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restino.Application.Features.PasswordReset.Commands.ResetPassword;
using Restino.Application.Features.PasswordReset.Queries.SendPasswoedResetCode;

namespace Restino.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController(IMediator mediator) : Controller
    {
        [HttpPost("reset-code")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendPasswordResetCode([FromBody] SendPasswordResetCodeQuery query)
        {
            await mediator.Send(query);
            return NoContent();
        }

        [HttpPut("change")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangePassword([FromBody] ResetPasswordCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}