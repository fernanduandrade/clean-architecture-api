using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using Bot.Application.Reward.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bot.Application.Reward.Queries;

public record GetQuizByIdQuery : IRequest<ApiResult<RewardDTO>>
{
    public int Id {get; init;}
}

public class GetQuizByIdQueryHandler : IRequestHandler<GetQuizByIdQuery, ApiResult<RewardDTO>>
{
    private readonly IAppContext _context;
    private readonly IMapper _mapper;

    public GetQuizByIdQueryHandler(IAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResult<RewardDTO>> Handle(GetQuizByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Quizes
            .AsNoTracking()
            .Where(quiz => quiz.Id == request.Id)
            .ProjectTo<RewardDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return new ApiResult<RewardDTO>(result, "Operação concluida com sucesso");
    }
}
