using Bot.Application.Common.Interfaces;
using Entities = Bot.Domain.Entities;
using MediatR;

namespace Bot.Application.EventUser.Commands;

public record DeleteEventUserCommand : IRequest<bool>
{
    public int Id { get; set; }
    public int FkEvent { get; set; }
    public string UserId { get; set; }
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
        Entities.EventUser entity = new()
        {
            Id = request.Id,
            FkEvent = request.FkEvent,
            FkUser = request.UserId
        };

        _context.EventUsers.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
