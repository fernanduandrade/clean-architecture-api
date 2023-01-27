using Bot.Application.EventUser.Commands;
using Bot.Application.EventUser.DTO;
using Bot.Application.EventUser.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Bot.Presentation.Controllers;

public class EventUserController : BaseController
{
    [HttpGet("check-user")]
    public async Task<ActionResult<UserCompleteEventDTO>> CheckUserCompleteEvent([FromQuery] GetUserCompleteEventQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<bool>> CreateUserEvent([FromBody] CreateEventUserCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteUserEvent([FromBody] DeleteEventUserCommand command)
    {
        return await Mediator.Send(command);
    }
}
