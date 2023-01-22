using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bot.Application.Common.Interfaces;
using Bot.Application.Event.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bot.Application.Event.Queries;

public record GetActiveEventQuery : IRequest<EventDTO>
{}

public class GetActiveEventQueryHandle : IRequestHandler<GetActiveEventQuery, EventDTO>
{
    private readonly IAppContext _context;
    private readonly IMapper _mapper;

    public GetActiveEventQueryHandle(IAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EventDTO> Handle(GetActiveEventQuery request, CancellationToken cancellationToken)
    {
        return await _context.Events
            .Where(@event => @event.IsActive)
            .ProjectTo<EventDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

    }
}