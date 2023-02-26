using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using Bot.Application.Common.Mapping;
using Bot.Application.Common.Models;
using Bot.Application.Reward.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bot.Application.Reward.Queries;

public record GetRewardPaginatedQuery : IRequest<ApiResult<PaginatedList<RewardDTO>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 1;
}

public class GetRewardPaginatedQueryHandler : IRequestHandler<GetRewardPaginatedQuery, ApiResult<PaginatedList<RewardDTO>>>
{
    private readonly IAppContext _context;
    private readonly IMapper _mapper;

    public GetRewardPaginatedQueryHandler(IAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResult<PaginatedList<RewardDTO>>> Handle(GetRewardPaginatedQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Events
            .AsNoTracking()
            .ProjectTo<RewardDTO>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return new ApiResult<PaginatedList<RewardDTO>>(result, message: "Operação realizada com sucesso");
    }
}
