using Bot.Application.Event.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Bot.Presentation.Controllers;

public class EventController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateEventCommand command)
    {
        return await Mediator.Send(command);
    }
}