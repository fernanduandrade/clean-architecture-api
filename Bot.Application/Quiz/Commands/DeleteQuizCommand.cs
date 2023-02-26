using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bot.Application.Quiz.Commands;

public record DeleteQuizCommand : IRequest<ApiResult<bool>>
{
    public int Id {get; init;}
}

public class DeleteQuizCommandHandler : IRequestHandler<DeleteQuizCommand, ApiResult<bool>>
{
    private readonly IAppContext _context;

    public DeleteQuizCommandHandler(IAppContext context)
    {
        _context = context;
    }

    public async Task<ApiResult<bool>> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Events.FirstOrDefaultAsync(
            quiz => quiz.Id == request.Id);

        if (entity is null) return new ApiResult<bool>(false, "Erro ao deletar o registro");

        _context.Events.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResult<bool>(true, "Operação concluida com sucesso.");
    }
}