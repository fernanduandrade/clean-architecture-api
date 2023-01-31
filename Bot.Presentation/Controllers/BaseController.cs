using Bot.Presentation.Tools;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bot.Presentation.Controllers;

[ValidateModelState]
[ApiController]
[Route("api/v{version:ApiVersion}/[controller]")]
public abstract class BaseController : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator =>
        _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}