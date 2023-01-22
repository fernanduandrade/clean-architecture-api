using Bot.Application.Common.Models;
using Bot.Application.Event.Commands;
using Bot.Application.Event.Queries;
using Bot.Application.Event.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Bot.Presentation.Controllers;

public class EventController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<EventDTO>>> GetEventsWithPagination([FromQuery] GetEventsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateEventCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet("find-active")]
    public async Task<ActionResult<EventDTO>> GetActiveEvent([FromQuery]GetActiveEventQuery query)
    {
        return await Mediator.Send(query);
    }

}