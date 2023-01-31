using Bot.Application.Common.Interfaces;
using Entities = Bot.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Bot.Application.Common;

namespace Bot.Application.EventUser.Commands;

public record DeleteEventUserCommand : IRequest<ApiResult<bool>>
{
    public int Id { get; set; }
}

public class DeleteEventUserCommandHandler : IRequestHandler<DeleteEventUserCommand, ApiResult<bool>>
{
    private readonly IAppContext _context;
    public DeleteEventUserCommandHandler(IAppContext context)
    {
        _context = context;
    }

    public async Task<ApiResult<bool>> Handle(DeleteEventUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EventUsers.FirstOrDefaultAsync(
            eventUser => eventUser.Id == request.Id);

        if (entity is null) return new ApiResult<bool>(false, "Erro ao deletar o registro");

        _context.EventUsers.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResult<bool>(true, "Operação concluida com sucesso.");
    }
}
