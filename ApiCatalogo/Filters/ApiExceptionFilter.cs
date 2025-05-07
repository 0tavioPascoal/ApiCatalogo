using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiCatalogo.Filters;

public class ApiExceptionFilter : IExceptionFilter {
    private readonly ILogger<ApiExceptionFilter> _logger;
    
    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger;
    }
    
    public void OnException(ExceptionContext context)
    {
      _logger.LogError(context.Exception, "Ocorreu uma exeção nao tratada: Status Code 500"); 
      
      context.Result = new ObjectResult("Um erro ocorreu ao processar a sua requisição")
      {
          StatusCode = StatusCodes.Status500InternalServerError
      };
    }
}