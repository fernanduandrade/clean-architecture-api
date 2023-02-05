using FluentValidation;
using Bot.Application.Event.Commands;

namespace Bot.Application.Event.Validation;

public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.DateStart).NotEmpty();
    }
}