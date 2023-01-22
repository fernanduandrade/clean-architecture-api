using AutoMapper;
using Bot.Application.Common.Interfaces;
using Bot.Application.EventUser.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bot.Application.EventUser.Queries;

public record GetUserCompleteEventQuery : IRequest<UserCompleteEventDTO>
{
    public string? UserDiscordId { get; set; }
}

public class GetUserCompleteEventQueryHandle : IRequestHandler<GetUserCompleteEventQuery, UserCompleteEventDTO>
{
    private readonly IAppContext _appContext;

    public GetUserCompleteEventQueryHandle(IAppContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<UserCompleteEventDTO> Handle(GetUserCompleteEventQuery request, CancellationToken cancellationToken)
    {
            var result = await _appContext.EventUsers
            .Where(evtUser => evtUser.FkUser == request.UserDiscordId)
            .FirstOrDefaultAsync(cancellationToken);

        if(result is null)
        {
            return new UserCompleteEventDTO { HasCompleteEvent = false };
        }
        return new UserCompleteEventDTO { HasCompleteEvent = true };
    }
}
