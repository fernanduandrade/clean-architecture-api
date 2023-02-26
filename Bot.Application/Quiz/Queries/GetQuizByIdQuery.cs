using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using Bot.Application.Quiz.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bot.Application.Quiz.Queries;

public record GetQuizByIdQuery : IRequest<ApiResult<QuizDTO>>
{
    public int Id {get; init;}
}

public class GetQuizByIdQueryHandler : IRequestHandler<GetQuizByIdQuery, ApiResult<QuizDTO>>
{
    private readonly IAppContext _context;
    private readonly IMapper _mapper;

    public GetQuizByIdQueryHandler(IAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResult<QuizDTO>> Handle(GetQuizByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Quizes
            .AsNoTracking()
            .Where(quiz => quiz.Id == request.Id)
            .ProjectTo<QuizDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return new ApiResult<QuizDTO>(result, "Operação concluida com sucesso");
    }
}
