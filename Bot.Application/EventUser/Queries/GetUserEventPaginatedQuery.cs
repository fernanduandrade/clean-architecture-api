using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using Bot.Application.Common.Mapping;
using Bot.Application.Common.Models;
using Bot.Application.EventUser.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bot.Application.EventUser.Queries;

public record GetUserEventPaginatedQuery : IRequest<ApiResult<PaginatedList<EventUserDTO>>>
{
    public int PageNumber {get; init;} = 1;
    public int PageSize {get; init;} = 50;
}

public class GetUserEventPaginatedQueryHandle : IRequestHandler<GetUserEventPaginatedQuery, ApiResult<PaginatedList<EventUserDTO>>>
{
    private readonly IAppContext _context;
    private readonly IMapper _mapper;

    public GetUserEventPaginatedQueryHandle(IAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResult<PaginatedList<EventUserDTO>>> Handle(GetUserEventPaginatedQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.EventUsers
            .AsNoTracking()
            .ProjectTo<EventUserDTO>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        
        return new ApiResult<PaginatedList<EventUserDTO>>(result, message: "Operação realizada com sucesso");
    }
}
