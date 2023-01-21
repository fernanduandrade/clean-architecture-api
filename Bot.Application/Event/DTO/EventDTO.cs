using AutoMapper;
using Bot.Application.Common.Mapping;
using Entities = Bot.Domain.Entities;

namespace Bot.Application.Event.DTO;

public record EventDTO : IMapFrom<Entities.Event>
{
    public int Id { get; init; }
    public string? Description { get; init; }
    public bool IsActive { get; init; }
    public DateTime DateStart { get; init; }
    public DateTime ExpireAt { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Entities.Event, EventDTO>();
    }
}
