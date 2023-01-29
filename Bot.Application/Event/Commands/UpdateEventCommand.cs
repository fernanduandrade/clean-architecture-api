using Bot.Application.Common.Interfaces;
using Bot.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using Entites = Bot.Domain.Entities;

namespace Bot.Application.Event.Commands;

public record UpdateEventCommand : IRequest<bool>
{
    public int Id { get; init; }
    public string? Description { get; init; }
    public bool IsActive { get; init; } = false;

    public DateTime DateStart { get; init; }
    public DateTime ExpireAt { get; init; }
}

public class UpdateEventCommandHandle : IRequestHandler<UpdateEventCommand, bool>
{
    private readonly IAppContext _context;

    public UpdateEventCommandHandle(IAppContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var entity = new Entites.Event
        {
            DateStart = request.DateStart,
            ExpireAt = request.ExpireAt,
            Description = request.Description,
            IsActive = request.IsActive,
            Id = request.Id,
        };

        entity.AddDomainEvent(new EventCreatedEvent(entity));
        _context.Events.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        

        return true;
    }
}