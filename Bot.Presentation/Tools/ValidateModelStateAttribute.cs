using Bot.Application.Common;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bot.Presentation.Tools;

public class ValidateModelStateAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if(!context.ModelState.IsValid)
        {
            var modelStateEntries = context.ModelState.Values;

            foreach(var modelStateEntry in modelStateEntries)
            {
                foreach(var error in modelStateEntry.Errors) 
                {
                    ApiResult<string> resultObject = new ApiResult<string>(error.ErrorMessage);
                    context.Result = new JsonResult(resultObject);
                }
            }
        }
    }
}
