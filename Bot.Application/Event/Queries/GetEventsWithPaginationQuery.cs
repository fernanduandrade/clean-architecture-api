using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bot.Application.Common.Interfaces;
using Bot.Application.Common.Mapping;
using Bot.Application.Common.Models;
using Bot.Application.Event.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Bot.Application.Event.Queries;

public record GetEventsWithPaginationQuery : IRequest<PaginatedList<EventDTO>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 1;
}

public class GetEventsWithPaginationQueryHandle : IRequestHandler<GetEventsWithPaginationQuery, PaginatedList<EventDTO>>
{
    private readonly IAppContext _context;
    private readonly IMapper _mapper;

    public GetEventsWithPaginationQueryHandle(IAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<EventDTO>> Handle(GetEventsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Events
            .ProjectTo<EventDTO>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        return result;
    }
}
