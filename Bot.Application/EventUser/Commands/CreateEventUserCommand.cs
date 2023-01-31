using AutoMapper;
using Bot.Application.Common.Interfaces;
using Entities = Bot.Domain.Entities;
using MediatR;
using Domain.Events;
using Bot.Application.Common;

namespace Bot.Application.EventUser.Commands;

public record CreateEventUserCommand : IRequest<ApiResult<bool>>
{
    public string? UserDiscordId { get; init; }
    public int EventId { get; init; }
}

public class CreateEventUserCommandHandle : IRequestHandler<CreateEventUserCommand, ApiResult<bool>>
{
    private readonly IAppContext _context;
    private readonly IMapper _mapper;

    public CreateEventUserCommandHandle(IAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResult<bool>> Handle(CreateEventUserCommand request, CancellationToken cancellationToken)
    {
        Entities.EventUser entity = new()
        {
            FkEvent = request.EventId,
            FkUser = request.UserDiscordId
        };

        entity.AddDomainEvent(new EventUserCreatedEvent(entity));

        _context.EventUsers.Add(entity);
        var result = await _context.SaveChangesAsync(cancellationToken);

        return new ApiResult<bool>(result > 0, "Operação realizada com sucesso.");


    }
}

