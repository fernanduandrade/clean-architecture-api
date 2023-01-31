using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using Bot.Application.EventUser.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bot.Application.EventUser.Queries;

public record GetUserCompleteEventQuery : IRequest<ApiResult<UserCompleteEventDTO>>
{
    public string? UserDiscordId { get; set; }
}

public class GetUserCompleteEventQueryHandle : IRequestHandler<GetUserCompleteEventQuery, ApiResult<UserCompleteEventDTO>>
{
    private readonly IAppContext _appContext;

    public GetUserCompleteEventQueryHandle(IAppContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<ApiResult<UserCompleteEventDTO>> Handle(GetUserCompleteEventQuery request, CancellationToken cancellationToken)
    {
            var result = await _appContext.EventUsers
            .Where(evtUser => evtUser.FkUser == request.UserDiscordId)
            .FirstOrDefaultAsync(cancellationToken);

        if(result is null)
        {
            return new ApiResult<UserCompleteEventDTO>(new UserCompleteEventDTO { HasCompleteEvent = false }, "Usuário não completou o evento.");
        }
        return new ApiResult<UserCompleteEventDTO>(new UserCompleteEventDTO { HasCompleteEvent = true }, "Usuário já realizou este evento.");
    }
}
