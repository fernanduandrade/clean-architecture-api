using AutoMapper;
using Bot.Application.Common;
using Bot.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Bot.Application.Quiz.DTO;
using Bot.Application.Common.Models;
using Bot.Application.Common.Mapping;

namespace Bot.Application.Quiz.Queries;

public record GetQuizPaginatedQuery : IRequest<ApiResult<PaginatedList<QuizDTO>>>
{
    public int PageNumber {get; init;} = 1;
    public int PageSize {get; init; } = 1;
}

public class GetQuizPaginatedQueryHandler : IRequestHandler<GetQuizPaginatedQuery, ApiResult<PaginatedList<QuizDTO>>>
{
    private readonly IAppContext _context;
    private readonly IMapper _mapper;


    public GetQuizPaginatedQueryHandler(IAppContext context, IMapper mapper)
    {   
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResult<PaginatedList<QuizDTO>>> Handle(GetQuizPaginatedQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Events
            .AsNoTracking()
            .ProjectTo<QuizDTO>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return new ApiResult<PaginatedList<QuizDTO>>(result, message: "Operação realizada com sucesso");
    }
}