using Bot.Application.Common;
using Bot.Application.Common.Models;
using Bot.Application.Reward.Commands;
using Bot.Application.Reward.DTO;
using Bot.Application.Reward.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Bot.Presentation.Controllers
{
    public class RewardController : BaseController
    {
        [HttpPost("claim-reward")]
        [ProducesResponseType(typeof(ApiResult<ClaimRewardDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResult<ClaimRewardDTO>>> ClaimReward(ClaimRewardCommand command)
        {
            var result = await Mediator.Send(command); 
            return Created("",result);
        }

        [HttpPost]
    [ProducesResponseType(typeof(ApiResult<int>), StatusCodes.Status201Created)]
    public async Task<ActionResult<ApiResult<int>>> CreateReward([FromBody] CreateRewardCommand command)
    {
        var result = await Mediator.Send(command);
        return Created("", result);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(ApiResult<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<bool>>> DeleteReward([FromQuery] DeleteRewardCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<PaginatedList<RewardDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(PaginatedList<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResult<PaginatedList<RewardDTO>>>> GetEventsUser([FromQuery] GetRewardPaginatedQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResult<RewardDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResult<RewardDTO>>> GetEventsUserById([FromQuery] GetQuizByIdQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(ApiResult<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<bool>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResult<bool>>> UpdateReward([FromQuery] UpdateRewardCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    }
}
