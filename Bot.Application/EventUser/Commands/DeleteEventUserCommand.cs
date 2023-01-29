using Bot.Application.Common.Interfaces;
using Entities = Bot.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bot.Application.EventUser.Commands;

public record DeleteEventUserCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteEventUserCommandHandler : IRequestHandler<DeleteEventUserCommand, bool>
{
    private readonly IAppContext _context;
    public DeleteEventUserCommandHandler(IAppContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteEventUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EventUsers.FirstOrDefaultAsync(
            eventUser => eventUser.Id == request.Id);

        if (entity is null) return false;

        _context.EventUsers.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
