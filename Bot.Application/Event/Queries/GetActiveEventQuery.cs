using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using Bot.Application.Event.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bot.Application.Event.Queries;

public record GetActiveEventQuery : IRequest<ApiResult<EventDTO>>
{}

public class GetActiveEventQueryHandle : IRequestHandler<GetActiveEventQuery, ApiResult<EventDTO>>
{
    private readonly IAppContext _context;
    private readonly IMapper _mapper;

    public GetActiveEventQueryHandle(IAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResult<EventDTO>> Handle(GetActiveEventQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Events
            .AsNoTracking()
            .Where(@event => @event.IsActive)
            .ProjectTo<EventDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        
        return new ApiResult<EventDTO>(result, "Operação concluida com sucesso.");

    }
}