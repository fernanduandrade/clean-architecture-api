using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using Bot.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Entites = Bot.Domain.Entities;

namespace Bot.Application.Event.Commands;

public record UpdateEventCommand : IRequest<ApiResult<bool>>
{
    public int Id { get; init; }
    public string? Description { get; init; }
    public bool IsActive { get; init; } = false;

    public DateTime DateStart { get; init; }
    public DateTime ExpireAt { get; init; }
}

public class UpdateEventCommandHandle : IRequestHandler<UpdateEventCommand, ApiResult<bool>>
{
    private readonly IAppContext _context;

    public UpdateEventCommandHandle(IAppContext context)
    {
        _context = context;
    }

    public async Task<ApiResult<bool>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context
            .Events
            .AsNoTracking()
            .FirstOrDefaultAsync(evt => evt.Id == request.Id);

        if(entity is null)
        {
            return new ApiResult<bool>(false, "Falha ao executar a operação.");
        }

        var newEntity = new Entites.Event
        {
            DateStart = request.DateStart,
            ExpireAt = request.ExpireAt,
            Description = request.Description,
            IsActive = request.IsActive,
            Id = request.Id,
        };

        newEntity.AddDomainEvent(new EventCreatedEvent(newEntity));
        _context.Events.Entry(newEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return new ApiResult<bool>(true, "Operação concluida com sucesso");
    }
}