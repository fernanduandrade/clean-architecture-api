using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bot.Application.Event.Commands;

public record DeleteEventCommand : IRequest<ApiResult<bool>>
{
    public int Id {get; init;}
}

public class DeleteEventCommandHandle : IRequestHandler<DeleteEventCommand, ApiResult<bool>>
{
    private readonly IAppContext _context;

    public DeleteEventCommandHandle(IAppContext context)
    {
        _context = context;
    }

    public async Task<ApiResult<bool>> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Events.FirstOrDefaultAsync(
            eventUser => eventUser.Id == request.Id);

        if (entity is null) return new ApiResult<bool>(false, "Erro ao deletar o registro");

        _context.Events.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResult<bool>(true, "Operação concluida com sucesso.");
    }
}