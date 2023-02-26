using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Entites = Bot.Domain.Entities;

namespace Bot.Application.EventUser.Commands;

public record UpdateEventUserCommand : IRequest<ApiResult<bool>>
{
    public int Id { get; init; }
    public int FkEvent { get; init; }
    public string? UserId { get; init; }
}

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventUserCommand, ApiResult<bool>>
{
    private readonly IAppContext _context;

    public UpdateEventCommandHandler(IAppContext context)
    {
        _context = context;
    }

    public async Task<ApiResult<bool>> Handle(UpdateEventUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context
            .Events
            .AsNoTracking()
            .FirstOrDefaultAsync(evtUser => evtUser.Id == request.Id);

        if(entity is null)
        {
            return new ApiResult<bool>(false, "Falha ao executar a operação.");
        }

        var newEntity = new Entites.EventUser
        {
            FkEvent = request.FkEvent,
            FkUser = request.UserId,
            Id = request.Id,
        };

        newEntity.AddDomainEvent(new EventUserCreatedEvent(newEntity));
        _context.EventUsers.Entry(newEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return new ApiResult<bool>(true, "Operação concluida com sucesso");
    }
}
