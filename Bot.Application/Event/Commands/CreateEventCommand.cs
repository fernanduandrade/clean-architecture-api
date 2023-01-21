using Bot.Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Entites = Bot.Domain.Entities;

namespace Bot.Application.Event.Commands;

public record CreateEventCommand : IRequest<int>
{
    public string? Description { get; set; }
    public bool IsActive { get; set; } = false;
    public int FkReward { get; set; }
    public DateTime DateStart { get; set; } = DateTime.Now;
    public DateTime ExpireAt { get; set; } = DateTime.Now.AddHours(2);
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
            FkReward = request.FkReward,
        };

        entity.AddDomainEvent(new EventCreatedEvent(entity));

        await _appContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
    
