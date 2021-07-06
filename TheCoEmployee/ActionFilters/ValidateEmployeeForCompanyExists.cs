using Contracts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System;
using System.Threading.Tasks;

namespace TheCoEmmployee.ActionFilters
{
    public class ValidateEmployeeForCompanyExists : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateEmployeeForCompanyExists(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var companyId = (Guid)context.ActionArguments["companyId"];

            var company = _repository.Company.GetCompany(companyId);
            if (company == null)
            {
                _logger.LogError("Company for employee was not found");
                context.Result = new NotFoundResult();
                return;
            }

            var employeeId = (Guid)context.ActionArguments["Id"];

            var employee = _repository.Employee.GetEmployee(companyId, employeeId);
            if (employee == null)
            {
                _logger.LogError("Employee does not exist");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("employee", employee);
                await next();
            }
        }
    }
}
