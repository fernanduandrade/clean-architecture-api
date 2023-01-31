using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using Bot.Domain.Events;
using MediatR;
using Entites = Bot.Domain.Entities;

namespace Bot.Application.Event.Commands;

public record CreateEventCommand : IRequest<ApiResult<int>>
{
    public string? Description { get; init; }
    public bool IsActive { get; init; } = false;

    public DateTime DateStart { get; init; }
    public DateTime ExpireAt { get; init; }
}

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, ApiResult<int>>
{
    private readonly IAppContext _appContext;

    public CreateEventCommandHandler(IAppContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<ApiResult<int>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
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


        return new ApiResult<int>(entity.Id, message: "Operação realizada com sucesso");
    }
}
    
