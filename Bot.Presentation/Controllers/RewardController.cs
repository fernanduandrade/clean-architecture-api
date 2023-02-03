using Bot.Application.Common;
using Bot.Application.Quiz.DTO;
using Bot.Application.Reward.Commands;
using Bot.Application.Reward.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Bot.Presentation.Controllers
{
    public class RewardController : BaseController
    {
        [HttpPost("claim-reward")]
        [ProducesResponseType(typeof(ApiResult<ClaimRewardDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResult<ClaimRewardDTO>>> ClaimReward(ClaimRewardCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
