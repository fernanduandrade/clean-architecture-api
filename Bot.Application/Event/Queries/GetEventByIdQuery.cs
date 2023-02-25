using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using Bot.Application.Event.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bot.Application.Event.Queries;

public record GetEventByIdQuery : IRequest<ApiResult<EventDTO>>
{
    public int Id {get; init;}
}

public class GetEventByIdQueryHandle : IRequestHandler<GetEventByIdQuery, ApiResult<EventDTO>>
{
    private readonly IAppContext _context;
    private readonly IMapper _mapper;

    public GetEventByIdQueryHandle(IAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResult<EventDTO>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Events
            .AsNoTracking()
            .Where(@event => @event.Id == request.Id)
            .ProjectTo<EventDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return new ApiResult<EventDTO>(result, message: "Operação realizada com sucesso");
    }
}
