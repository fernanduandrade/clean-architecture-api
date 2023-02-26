using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using Domain.Events.Reward;
using MediatR;
using Entities = Bot.Domain.Entities;

namespace Bot.Application.Reward.Commands;

public record CreateRewardCommand : IRequest<ApiResult<int>>
{
    public int Coin { get; init; }
    public int Expirience { get; init; }
    public bool Claimed { get; init; }
    public string? Role { get; init; }
    public int FkEvent { get; init; }
    public bool ParticipantReward { get; init; }
}

public class CreateRewardCommandHandler : IRequestHandler<CreateRewardCommand, ApiResult<int>>
{
    private readonly IAppContext _context;

    public CreateRewardCommandHandler(IAppContext context)
    {
        _context = context;
    }

    public async Task<ApiResult<int>> Handle(CreateRewardCommand request, CancellationToken cancellationToken)
    {
        var entity = new Entities.Reward
        {
            Claimed = request.Claimed,
            Coin = request.Coin,
            FkEvent = request.FkEvent,
            Expirience = request.Expirience,
            Role = request.Role,
            ParticipantReward = request.ParticipantReward,
        };

        entity.AddDomainEvent(new RewardCreatedEvent(entity));
        _context.Rewards.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);


        return new ApiResult<int>(entity.Id, message: "Operação realizada com sucesso");
    }
}