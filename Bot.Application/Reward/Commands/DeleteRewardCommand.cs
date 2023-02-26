using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bot.Application.Reward.Commands;

public record DeleteRewardCommand : IRequest<ApiResult<bool>>
{
    public int Id {get; init;}
}

public class DeleteRewardCommandHandler : IRequestHandler<DeleteRewardCommand, ApiResult<bool>>
{
    private readonly IAppContext _context;

    public DeleteRewardCommandHandler(IAppContext context)
    {
        _context = context;
    }

    public async Task<ApiResult<bool>> Handle(DeleteRewardCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Rewards.FirstOrDefaultAsync(
            reward => reward.Id == request.Id);

        if (entity is null) return new ApiResult<bool>(false, "Erro ao deletar o registro");

        _context.Rewards.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResult<bool>(true, "Operação concluida com sucesso.");
    }
}
