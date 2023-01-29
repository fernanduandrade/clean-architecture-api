using Bot.Application.Reward.Commands;
using Bot.Application.Reward.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Bot.Presentation.Controllers
{
    public class RewardControlle : BaseController
    {
        [HttpPost("claim-reward")]
        public async Task<ActionResult<ClaimRewardDTO>> ClaimReward(ClaimRewardCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
