using FluentValidation;
using MediatR;

namespace Bot.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, ApiResult<TResponse>>
    where TRequest : IRequest<TResponse>
    where TResponse : ApiResult<TResponse>?
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator)
    {
        _validator = validator;
    }

    public async Task<ApiResult<TResponse>> Handle(TRequest request, RequestHandlerDelegate<ApiResult<TResponse>> next, CancellationToken cancellationToken)
    {
        if(_validator is null) return await next();

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if(validationResult.IsValid) return await next();

        var errors = validationResult.Errors.ConvertAll(
            validationFalire => validationFalire.ErrorMessage
        ).ToArray();

        return new ApiResult<TResponse>("erro no payload", errors);
    }
}
