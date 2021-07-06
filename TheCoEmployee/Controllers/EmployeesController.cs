using AutoMapper;

using CodeMazeApp.ActionFilters;

using Contracts;

using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeatures;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;

using TheCoEmmployee.ActionFilters;

namespace CodeMazeApp.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/companies/{CompanyId}/employees")]
    public class EmployeesController : ControllerBase
    {
        private IRepositoryManager _repository;
        private ILoggerManager _logger;
        private IMapper _mapper;

        public EmployeesController(IRepositoryManager repoManager, ILoggerManager logger, IMapper mapper)
        {
            _repository = repoManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployeesForCompany(Guid CompanyId, [FromQuery] EmployeeParameters employeeParameters)
        {
            var employees = _repository.Employee.GetEmployees(CompanyId, employeeParameters);
            if (employees == null)
            {
                _logger.LogInfo($"There no employees in the company with company Id {CompanyId}");
                return NotFound();
            }
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(employees.MetaData));

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
            return Ok(employees);
        }

        [HttpGet("{Id}", Name = "GetEmployeeForCompany")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetEmployeeForCompany(Guid CompanyId, Guid Id)
        {
            var company = _repository.Company.GetCompany(CompanyId);
            if (company == null) return NotFound();
            var employee = _repository.Employee.GetEmployee(CompanyId, Id);
            if (employee == null) return NotFound();
            var employeeToReturn = _mapper.Map<EmployeeDTO>(employee);
            return Ok(employeeToReturn);
        }

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateEmployee(Guid CompanyId, [FromBody] EmployeeForCreationDto model)
        {
            if (model == null)
            {
                return BadRequest("Employee information is null");
            }

            var company = _repository.Company.GetCompany(CompanyId);
            if (company == null)
            {
                return BadRequest($"Company with Id {CompanyId} does not exist");
            }

            var employeeEntity = _mapper.Map<Employee>(model);
            _repository.Employee.CreateEmployeeForCompany(CompanyId, employeeEntity);
            _repository.Save();
            var employeeToReturn = _mapper.Map<EmployeeDTO>(employeeEntity);
            return CreatedAtAction($"GetEmployeeForCompany",
                new { CompanyId = employeeEntity.CompanyId, Id = employeeEntity.Id }, employeeToReturn);
        }

        [HttpDelete("{Id}")]
        [ServiceFilter(typeof(ValidateEmployeeForCompanyExists))]
        public IActionResult DeleteEmployee(Guid CompanyId, Guid Id)
        {
            var employee = HttpContext.Items["employee"] as Employee;
            _repository.Employee.DeleteEmployee(employee);
            _repository.Save();
            return NoContent();
        }

        [HttpPut("{Id}")]
        [ServiceFilter(typeof(ValidateEmployeeForCompanyExists))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult UpdateEmployeeForCompany(Guid CompanyId, Guid Id, [FromBody] EmployeeForUpdateDTO employee)
        {
            var employeeEntity = HttpContext.Items["employee"] as Employee;
            var employeeToUpdate = _mapper.Map(employee, employeeEntity);
            _repository.Save();
            return NoContent();
        }

        [HttpPatch("{Id}")]
        [ServiceFilter(typeof(ValidateEmployeeForCompanyExists))]
        public IActionResult PartiallyPatchEmployeeForCompany(Guid companyId, Guid Id,
            [FromBody] JsonPatchDocument<EmployeeForUpdateDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("There is no patch doc from the client");
                return BadRequest();
            }

            var employeeEntity = HttpContext.Items["employee"] as Employee;

            var employeeToPatch = _mapper.Map<EmployeeForUpdateDTO>(employeeEntity);
            patchDoc.ApplyTo(employeeToPatch, ModelState);
            TryValidateModel(employeeToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid patchDoc");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(employeeToPatch, employeeEntity);
            _repository.Save();

            return NoContent();
        }
    }
}
