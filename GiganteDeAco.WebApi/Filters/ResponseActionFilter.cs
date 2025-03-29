using GiganteDeAco.Contracts.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ResponseActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            var response = objectResult.Value as Response;

            if (response != null && !response.IsValid())
            {
                objectResult.StatusCode = response.Notificacoes?.FirstOrDefault()?.Codigo; ;
                objectResult.Value = response;
            }
        }
    }
}
