using Bot.Application.Common;
using Bot.Application.Common.Models;
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
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResult<bool>), StatusCodes.Status201Created)]
    public async Task<ActionResult<ApiResult<bool>>> CreateUserEvent([FromBody] CreateEventUserCommand command)
    {
        var result = await Mediator.Send(command);
        return Created("", result);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(ApiResult<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<bool>>> DeleteUserEvent([FromQuery] DeleteEventUserCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<PaginatedList<EventUserDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(PaginatedList<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResult<PaginatedList<EventUserDTO>>>> GetEventsUser([FromQuery] GetUserEventPaginatedQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResult<EventUserDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResult<EventUserDTO>>> GetEventsUserById([FromQuery] GetUserEventPaginatedQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(ApiResult<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<bool>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResult<bool>>> UpdateEventsUser([FromQuery] UpdateEventUserCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}
