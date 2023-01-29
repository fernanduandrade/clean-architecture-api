using Bot.Application.Common.Interfaces;
using Entities = Bot.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Bot.Application.Reward.DTO;
using Bot.Domain.Events;

namespace Bot.Application.Reward.Commands;

public record ClaimRewardCommand : IRequest<ClaimRewardDTO>
{
    public string UserId { get; set; }
    public int FkEvent { get; set; }
}

public class ClaimRewardCommandHandle : IRequestHandler<ClaimRewardCommand, ClaimRewardDTO>
{
    private readonly IAppContext _appContext;

    public ClaimRewardCommandHandle(IAppContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<ClaimRewardDTO> Handle(ClaimRewardCommand request, CancellationToken cancellationToken)
    {
        var avaliableReward = await _appContext.Rewards
            .OrderBy(o => o.Id)
            .FirstOrDefaultAsync(reward =>
                reward.FkEvent == request.FkEvent
             && reward.Claimed == false
            );

        Entities.Reward reward =
            avaliableReward.ParticipantReward
            ? await _appContext.Rewards.FirstOrDefaultAsync(
                reward => reward.FkEvent == request.FkEvent
                && reward.ParticipantReward)
            : avaliableReward;

       
        reward.Claimed = reward.ParticipantReward ? false : true;
        reward.AddDomainEvent(new RewardClaimEvent(reward));
        _appContext.Rewards.Entry(reward).State = EntityState.Modified;

        await _appContext.SaveChangesAsync(cancellationToken);


        var evtUser = new Entities.EventUser
        {
            FkEvent = request.FkEvent,
            FkUser = request.UserId
        };

        _appContext.EventUsers.Add(evtUser);
        await _appContext.SaveChangesAsync(cancellationToken);

        return new ClaimRewardDTO
        {
            Coins = reward.Coin,
            Expirience = reward.Expirience,
            Id = reward.Id,
            Role = reward.Role
        };
    }
}
