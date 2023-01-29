using AutoMapper;
using Bot.Application.Common.Mapping;
using Bot.Application.Event.DTO;
using Entities = Bot.Domain.Entities;

namespace Bot.Application.Quiz.DTO;

public record QuizDTO : IMapFrom<Entities.Quiz>
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public string? Answer { get; init; }
    public string? Question { get; init; }
    public string? Hint { get; init; }
    public bool HasNextQuestion { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Entities.Quiz, QuizDTO>();
    }
}
