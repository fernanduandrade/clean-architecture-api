using Bot.Application.Common;
using Bot.Application.EventUser.Commands;
using Bot.Application.EventUser.DTO;
using Bot.Application.EventUser.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Bot.Presentation.Controllers;

public class EventUserController : BaseController
{
    [HttpGet("check-user")]
    [ProducesResponseType(typeof(ApiResult<UserCompleteEventDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<UserCompleteEventDTO>>> CheckUserCompleteEvent([FromQuery] GetUserCompleteEventQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResult<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<bool>>> CreateUserEvent([FromBody] CreateEventUserCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(ApiResult<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<bool>>> DeleteUserEvent([FromQuery] DeleteEventUserCommand command)
    {
        return await Mediator.Send(command);
    }
}
