using Bot.Application.Common;
using Bot.Application.Common.Models;
using Bot.Application.Quiz.Commands;
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

    [HttpPost]
    [ProducesResponseType(typeof(ApiResult<int>), StatusCodes.Status201Created)]
    public async Task<ActionResult<ApiResult<int>>> CreateUserEvent([FromBody] CreateQuizCommand command)
    {
        var result = await Mediator.Send(command);
        return Created("", result);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(ApiResult<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<bool>>> DeleteUserEvent([FromQuery] DeleteQuizCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<PaginatedList<QuizDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(PaginatedList<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResult<PaginatedList<QuizDTO>>>> GetEventsUser([FromQuery] GetQuizPaginatedQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResult<QuizDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResult<QuizDTO>>> GetEventsUserById([FromQuery] GetQuizByIdQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(ApiResult<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<bool>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResult<bool>>> UpdateEventsUser([FromQuery] UpdateQuizCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}
