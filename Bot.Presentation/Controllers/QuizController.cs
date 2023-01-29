using Bot.Application.Quiz.DTO;
using Bot.Application.Quiz.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Bot.Presentation.Controllers;

public class QuizController : BaseController
{
    [HttpGet("get-by-event-id")]
    [ProducesResponseType(typeof(List<QuizDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<List<QuizDTO>>> GetAll([FromQuery]GetAllQuizesByEventIdQuery query)
    {
        return await Mediator.Send(query);
    }
}
