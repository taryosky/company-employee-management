using Entities.Models;

using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees(Guid companyId);

        Employee GetEmployee(Guid companyId, Guid Id);

        void CreateEmployeeForCompany(Guid CompanyId, Employee employee);

        void DeleteEmployee(Employee employee);
    }
}
