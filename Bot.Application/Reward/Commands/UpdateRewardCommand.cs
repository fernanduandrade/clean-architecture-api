using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using MediatR;
using Entities = Bot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Events.Reward;

namespace Bot.Application.Reward.Commands;

public record UpdateRewardCommand : IRequest<ApiResult<bool>>
{
    public int Id {get; init;}
    public int Coin { get; init; }
    public int Expirience { get; init; }
    public bool Claimed { get; init; }
    public string? Role { get; init; }
    public int FkEvent { get; init; }
    public bool ParticipantReward { get; init; }
}

public class UpdateRewardCommandHandler : IRequestHandler<UpdateRewardCommand, ApiResult<bool>>
{
    private readonly IAppContext _context;

    public UpdateRewardCommandHandler(IAppContext context)
    {
        _context = context;
    }

    public async Task<ApiResult<bool>> Handle(UpdateRewardCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Rewards
        .AsNoTracking()
        .FirstOrDefaultAsync(reward => reward.Id == request.Id);

        if(entity is null)
        {
            return new ApiResult<bool>(false, "Falha ao executar a operação.");
        }

        var newEntity = new Entities.Reward
        {
            Id = request.Id,
            Claimed = request.Claimed,
            Coin = request.Coin,
            FkEvent = request.FkEvent,
            Expirience = request.Expirience,
            Role = request.Role,
            ParticipantReward = request.ParticipantReward,
        };

        newEntity.AddDomainEvent(new RewardCreatedEvent(newEntity));
        _context.Rewards.Entry(newEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return new ApiResult<bool>(true, "Operação concluida com sucesso");
    }
}