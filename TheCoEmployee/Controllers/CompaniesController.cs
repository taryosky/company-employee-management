using AutoMapper;

using CodeMazeApp.ActionFilters;
using CodeMazeApp.ModelBinders;

using Contracts;

using Entities.DTOs;
using Entities.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TheCoEmmployee.ActionFilters;

namespace CodeMazeApp.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private IRepositoryManager _repository;
        private ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpOptions]
        public async Task<IActionResult> GetCompanyOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            await Task.CompletedTask;
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<CompanyDTO>> GetCompanies()
        {
            try
            {
                var companies = _repository.Company.GetAllCompanies(asNoTracking: false);
                var companiesDTO = _mapper.Map<IEnumerable<CompanyDTO>>(companies);
                return Ok(companiesDTO);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _repository.Company.GetCompany(id);

            if (company == null || !ModelState.IsValid)
            {
                _logger.LogInfo($"Company with {id} dowes not exist");
                return NotFound();
            }

            var companyDTO = _mapper.Map<CompanyDTO>(company);
            return Ok(companyDTO);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto model)
        {
            if (model == null)
            {
                return BadRequest("Please provide company information");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is not valie");
                return UnprocessableEntity(ModelState);
            }

            var companyEntity = _mapper.Map<Company>(model);
            _repository.Company.CreateCompany(companyEntity);
            _repository.Save();

            var companyToReturn = _mapper.Map<CompanyDTO>(companyEntity);
            return CreatedAtAction($"GetCompany", new { Id = companyEntity.Id }, companyToReturn);
        }

        [HttpGet("collection/({Ids})", Name = "GetCompanyCollection")]
        public IActionResult GetCompanyCollection([FromRoute] [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<Guid> Ids)
        {
            if (Ids == null) return BadRequest("Please provide company Ids");
            var companiesFromDb = _repository.Company.GetCompaniesById(Ids);
            if (companiesFromDb.Count() != Ids.Count()) return NotFound("SOme Companies do not exist");

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDTO>>(companiesFromDb);
            return Ok(companiesToReturn);
        }

        [HttpPost("collection")]
        public IActionResult CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> models)
        {
            if (models == null) return BadRequest("No company information found");
            var companiesToCreate = _mapper.Map<IEnumerable<Company>>(models);
            foreach (var company in companiesToCreate)
            {
                _repository.Company.CreateCompany(company);
            }

            _repository.Save();
            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDTO>>(companiesToCreate);
            var ids = string.Join(',', companiesToReturn.Select(x => x.Id));
            return CreatedAtAction("GetCompanyCollection", new { ids }, companiesToReturn);
        }

        [HttpDelete("{Id}")]
        [ServiceFilter(typeof(ValidateCompanyExitsAttribute))]
        public IActionResult DeleteCompany(Guid Id)
        {
            var companyToDelete = HttpContext.Items["company"] as Company;
            _repository.Company.DeleteCompany(companyToDelete);
            _repository.Save();
            return NoContent();
        }

        [HttpPut("{Id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateCompanyExitsAttribute))]
        public IActionResult UpdateCompany(Guid Id, [FromBody] CompanyForCreationDto company)
        {
            var companyEntity = HttpContext.Items["company"] as Company;

            _mapper.Map(company, companyEntity);
            _repository.Save();

            return NoContent();
        }
    }
}
