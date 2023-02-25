using Bot.Application.Common.Models;
using Bot.Application.Event.Commands;
using Bot.Application.Event.Queries;
using Bot.Application.Event.DTO;
using Microsoft.AspNetCore.Mvc;
using Bot.Application.Common;

namespace Bot.Presentation.Controllers;

public class EventController : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<PaginatedList<EventDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(PaginatedList<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResult<PaginatedList<EventDTO>>>> GetEventsWithPagination([FromQuery] GetEventsWithPaginationQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResult<int>), StatusCodes.Status201Created)]
    public async Task<ActionResult<ApiResult<int>>> Create(CreateEventCommand command)
    {
        var result = await Mediator.Send(command);
        if(result.Errors is not null) {
            return BadRequest(result);
        }
        return Created("", result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(ApiResult<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<bool>>> Update([FromBody]UpdateEventCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
 
    [HttpGet("find-active")]
    [ProducesResponseType(typeof(ApiResult<EventDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<EventDTO>>> GetActiveEvent([FromQuery]GetActiveEventQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResult<EventDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<EventDTO>>> GetByEventId([FromQuery]GetEventByIdQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(ApiResult<EventDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<EventDTO>>> DeleteEvent([FromQuery]DeleteEventCommand query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

}