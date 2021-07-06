using Contracts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System;
using System.Linq;

namespace CodeMazeApp.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        private readonly ILoggerManager _logger;

        public ValidationFilterAttribute(ILoggerManager logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];
            var parameters = context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;
            if (parameters == null)
            {
                _logger.LogError($"Object sent from client is null. Controller: {controller}, Action: {action}");
                context.Result = new BadRequestObjectResult($"Object is null. Controller: {controller}, action {action}");
                return;
            }

            if (!context.ModelState.IsValid)
            {
                _logger.LogError(($"Data are not valid"));
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
    }
}
