using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using Bot.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Entities = Bot.Domain.Entities;

namespace Bot.Application.Quiz.Commands;

public record UpdateQuizCommand : IRequest<ApiResult<bool>>
{
    public int Id { get; init; }
    public int FkEvent {get; init;}
    public string? Title { get; init; }
    public string? Answer { get; init; }
    public string? Question { get; init; }
    public string? Hint { get; init; }
    public bool HasNextQuestion { get; init; }
}

public class UpdateQuizCommandHandler : IRequestHandler<UpdateQuizCommand, ApiResult<bool>>
{
    private readonly IAppContext _context;

    public UpdateQuizCommandHandler(IAppContext context)
    {
        _context = context;
    }

    public async Task<ApiResult<bool>> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context
            .Quizes
            .AsNoTracking()
            .FirstOrDefaultAsync(quiz => quiz.Id == request.Id);

        if(entity is null)
        {
            return new ApiResult<bool>(false, "Falha ao executar a operação.");
        }

        var newEntity = new Entities.Quiz
        {
            Answer = request.Answer,
            Question = request.Question,
            Title = request.Title,
            Hint = request.Hint,
            FkEvent = request.FkEvent,
            Id = request.Id,
            HasNextQuestion = request.HasNextQuestion
        };

        newEntity.AddDomainEvent(new QuizCreatedEvent(newEntity));
        _context.Quizes.Entry(newEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return new ApiResult<bool>(true, "Operação concluida com sucesso");
    }
}