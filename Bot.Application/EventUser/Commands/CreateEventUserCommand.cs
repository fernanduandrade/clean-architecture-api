using AutoMapper;
using Bot.Application.Common.Interfaces;
using Entities = Bot.Domain.Entities;
using Bot.Domain.Events;
using MediatR;
using Domain.Events;

namespace Bot.Application.EventUser.Commands;

public record CreateEventUserCommand : IRequest<bool>
{
    public string? UserDiscordId { get; init; }
    public int EventId { get; init; }
}

public class CreateEventUserCommandHandle : IRequestHandler<CreateEventUserCommand, bool>
{
    private readonly IAppContext _context;
    private readonly IMapper _mapper;

    public CreateEventUserCommandHandle(IAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateEventUserCommand request, CancellationToken cancellationToken)
    {
        Entities.EventUser entity = new()
        {
            FkEvent = request.EventId,
            FkUser = request.UserDiscordId
        };

        entity.AddDomainEvent(new EventUserCreatedEvent(entity));

        _context.EventUsers.Add(entity);
        var result = await _context.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;


    }
}

