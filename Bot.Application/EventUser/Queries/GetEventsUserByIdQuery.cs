using AutoMapper;
using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using Bot.Application.EventUser.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace Bot.Application.EventUser.Queries;

public record GetEventsUserByIdQuery : IRequest<ApiResult<EventUserDTO>>
{
    public int Id {get; init;}
}

public class GetEventsUserByIdQueryHandle : IRequestHandler<GetEventsUserByIdQuery, ApiResult<EventUserDTO>>
{
    private readonly IAppContext _context;
    private readonly IMapper _mapper;

    public GetEventsUserByIdQueryHandle(IAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResult<EventUserDTO>> Handle(GetEventsUserByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.EventUsers
            .AsNoTracking()
            .Where(evtUser => evtUser.Id == request.Id)
            .ProjectTo<EventUserDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return new ApiResult<EventUserDTO>(result, "Operação concluida com sucesso");
    }
}
