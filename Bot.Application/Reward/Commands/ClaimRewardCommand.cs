using Bot.Application.Common.Interfaces;
using Entities = Bot.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Bot.Application.Reward.DTO;
using Bot.Domain.Events;
using Bot.Application.EventUser.Commands;

namespace Bot.Application.Reward.Commands;

public record ClaimRewardCommand : IRequest<ClaimRewardDTO>
{
    public string? UserId { get; set; }
    public int FkEvent { get; set; }
}

public class ClaimRewardCommandHandle : IRequestHandler<ClaimRewardCommand, ClaimRewardDTO>
{
    private readonly IAppContext _appContext;
    private readonly IMediator _mediator;
    public ClaimRewardCommandHandle(IAppContext appContext, IMediator mediator)
    {
        _appContext = appContext;
        _mediator = mediator;
    }

    public async Task<ClaimRewardDTO> Handle(ClaimRewardCommand request, CancellationToken cancellationToken)
    {
        var avaliableReward = await _appContext.Rewards
            .OrderBy(o => o.Id)
            .FirstOrDefaultAsync(reward =>
                reward.FkEvent == request.FkEvent
             && reward.Claimed == false
            );

        Entities.Reward? reward =
            avaliableReward!.ParticipantReward
            ? await _appContext.Rewards.FirstOrDefaultAsync(
                reward => reward.FkEvent == request.FkEvent
                && reward.ParticipantReward)
            : avaliableReward;

       
        if(reward is not null)
        {
            reward.Claimed = reward.ParticipantReward ? false : true;
            reward.AddDomainEvent(new RewardClaimEvent(reward));
            _appContext.Rewards.Entry(reward).State = EntityState.Modified;

            await _appContext.SaveChangesAsync(cancellationToken);


            var evtUserCommand = new CreateEventUserCommand
            {
                EventId = request.FkEvent,
                UserDiscordId = request.UserId
            };

            await _mediator.Send(evtUserCommand);

            return new ClaimRewardDTO
            {
                Coins = reward.Coin,
                Expirience = reward.Expirience,
                Id = reward.Id,
                Role = reward.Role
            };
        }

        return new ClaimRewardDTO();
    }
}
