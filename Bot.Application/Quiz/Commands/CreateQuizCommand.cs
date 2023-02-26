using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using Bot.Domain.Events;
using MediatR;
using Entities = Bot.Domain.Entities;

namespace Bot.Application.Quiz.Commands;

public record CreateQuizCommand : IRequest<ApiResult<int>>
{
    public string? Title { get; set; }
    public string? Question { get; set; }
    public string? Answer { get; set; }
    public string? Hint { get; set; }
    public int FkEvent { get; set; }
    public bool HasNextQuestion { get; set; }
}

public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, ApiResult<int>>
{
    private readonly IAppContext _context;

    public CreateQuizCommandHandler(IAppContext context)
    {
        _context = context;
    }

    public async Task<ApiResult<int>> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
    {
        var entity = new Entities.Quiz
        {
            Answer = request.Answer,
            FkEvent = request.FkEvent,
            Hint = request.Hint,
            Question = request.Question,
            Title = request.Title,
            HasNextQuestion = request.HasNextQuestion
        };

        entity.AddDomainEvent(new QuizCreatedEvent(entity));
        _context.Quizes.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);


        return new ApiResult<int>(entity.Id, message: "Operação realizada com sucesso");
    }
}