using Contracts;

using Entities;
using Entities.Models;
using Entities.RequestFeatures;

using Repository.Extensions;

using System;
using System.Linq;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationContext context) : base(context)
        {
        }

        /// <summary>
        /// Takes the company Id and the pagination parameter and returns a paginated list of employees from the company
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="employeeParameters"></param>
        /// <returns></returns>
        public PagedList<Employee> GetEmployees(Guid companyId, EmployeeParameters employeeParameters)
        {
            var employees = FindByCondition(x => x.CompanyId.Equals(companyId))
                .FilterEmployees(employeeParameters.MinAge, employeeParameters.MaxAge)
                .SearchEmployees(employeeParameters.SearchTerm)
                .OrderBy(x => x.Name)
                .ToList();

            //Paginate the employees from database
            return PagedList<Employee>.Paginate(employees, employeeParameters.PageNumber, employeeParameters.PageSize);
        }

        public Employee GetEmployee(Guid companyId, Guid Id)
        {
            return FindByCondition(x => x.CompanyId == companyId && x.Id == Id).FirstOrDefault();
        }

        public void CreateEmployeeForCompany(Guid CompanyId, Employee employee)
        {
            employee.CompanyId = CompanyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }
    }
}
