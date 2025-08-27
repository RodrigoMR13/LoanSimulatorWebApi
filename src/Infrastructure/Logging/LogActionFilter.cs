using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Logging
{
    public class LogActionFilter(ILogger<LogActionFilter> logger) : IActionFilter
    {
        private readonly ILogger<LogActionFilter> _logger = logger;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var endpoint = context.HttpContext.GetEndpoint()?.DisplayName;
            _logger.LogInformation($"Requisição recebida para o Endpoint: {endpoint}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var endpoint = context.HttpContext.GetEndpoint()?.DisplayName;
            _logger.LogInformation($"Resposta enviada para o Endpoint: {endpoint}");
        }
    }

}