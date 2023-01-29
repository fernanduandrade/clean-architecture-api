using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bot.Application.Common.Interfaces;
using Bot.Application.Event.DTO;
using Bot.Application.Quiz.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bot.Application.Quiz.Queries;

public record GetAllQuizesByEventIdQuery : IRequest<List<QuizDTO>>
{
    public int EventId { get; init; }
}

public class GetAllQuizesByEventIdQueryHandle : IRequestHandler<GetAllQuizesByEventIdQuery, List<QuizDTO>>
{
    private readonly IAppContext _context;
    private readonly IMapper _mapper;

    public GetAllQuizesByEventIdQueryHandle(IAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<QuizDTO>> Handle(GetAllQuizesByEventIdQuery request, CancellationToken cancellationToken)
    {
        var quizes = await _context.Quizes
            .Where(quiz => quiz.FkEvent == request.EventId)
            .ProjectTo<QuizDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return quizes;


    }
}