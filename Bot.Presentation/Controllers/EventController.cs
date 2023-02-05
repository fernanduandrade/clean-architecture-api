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
        return await Mediator.Send(query);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResult<int>), StatusCodes.Status201Created)]
    public async Task<ActionResult<ApiResult<int>>> Create(CreateEventCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut]
    [ProducesResponseType(typeof(ApiResult<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<bool>>> Update([FromBody]UpdateEventCommand command)
    {
        return await Mediator.Send(command);
    }
 
    [HttpGet("find-active")]
    [ProducesResponseType(typeof(ApiResult<EventDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<EventDTO>>> GetActiveEvent([FromQuery]GetActiveEventQuery query)
    {
        return await Mediator.Send(query);
    }

}