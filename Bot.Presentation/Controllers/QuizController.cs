using Bot.Application.Common;
using Bot.Application.Quiz.DTO;
using Bot.Application.Quiz.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Bot.Presentation.Controllers;

public class QuizController : BaseController
{
    [HttpGet("get-by-event-id")]
    [ProducesResponseType(typeof(ApiResult<List<QuizDTO>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<List<QuizDTO>>>> GetAll([FromQuery]GetAllQuizesByEventIdQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}
