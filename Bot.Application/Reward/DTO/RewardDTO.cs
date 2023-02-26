using AutoMapper;
using Bot.Application.Common.Mapping;
using Entities = Bot.Domain.Entities;

namespace Bot.Application.Reward.DTO;

public record RewardDTO : IMapFrom<Entities.Reward>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Entities.Reward, RewardDTO>();
    }

    public int Id {get; init;}
    public int Coin { get; init; }
    public int Expirience { get; init; }
    public bool Claimed { get; init; }
    public string? Role { get; init; }
    public int FkEvent { get; init; }
    public bool ParticipantReward { get; init; }
}
