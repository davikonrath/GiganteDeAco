using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception != null)
        {
            context.Result = new ObjectResult("Ocorreu um erro inesperado no servidor.")
            {
                StatusCode = 500
            };

            _logger.LogError(context.Exception, "Erro inesperado.");
        }
    }
}
