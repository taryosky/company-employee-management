using Contracts;

using Entities;
using Entities.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationContext context) : base(context)
        {
        }

        public IEnumerable<Employee> GetEmployees(Guid companyId)
        {
            return FindByCondition(x => x.CompanyId.Equals(companyId)).ToList();
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
