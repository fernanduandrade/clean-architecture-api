using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using Bot.Application.Common.Mapping;
using Bot.Application.Common.Models;
using Bot.Application.Event.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bot.Application.Event.Queries;

public record GetEventsWithPaginationQuery : IRequest<ApiResult<PaginatedList<EventDTO>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 1;
}

public class GetEventsWithPaginationQueryHandle : IRequestHandler<GetEventsWithPaginationQuery, ApiResult<PaginatedList<EventDTO>>>
{
    private readonly IAppContext _context;
    private readonly IMapper _mapper;

    public GetEventsWithPaginationQueryHandle(IAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResult<PaginatedList<EventDTO>>> Handle(GetEventsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Events
            .AsNoTracking()
            .ProjectTo<EventDTO>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return new ApiResult<PaginatedList<EventDTO>>(result, message: "Operação realizada com sucesso");
    }
}
