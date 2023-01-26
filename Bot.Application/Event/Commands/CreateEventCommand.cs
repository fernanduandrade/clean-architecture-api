using Bot.Application.Common.Interfaces;
using Bot.Domain.Events;
using MediatR;
using Entites = Bot.Domain.Entities;

namespace Bot.Application.Event.Commands;

public record CreateEventCommand : IRequest<int>
{
    public string? Description { get; init; }
    public bool IsActive { get; init; } = false;

    public DateTime DateStart { get; init; }
    public DateTime ExpireAt { get; init; }
}

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, int>
{
    private readonly IAppContext _appContext;

    public CreateEventCommandHandler(IAppContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<int> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var entity = new Entites.Event
        {
            Description = request.Description,
            DateStart = request.DateStart,
            ExpireAt = request.ExpireAt,
        };

        entity.AddDomainEvent(new EventCreatedEvent(entity));
        _appContext.Events.Add(entity);
        await _appContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
    
