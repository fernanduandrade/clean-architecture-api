using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bot.Presentation.Controllers;

public abstract class BaseController : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator =>
        _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}