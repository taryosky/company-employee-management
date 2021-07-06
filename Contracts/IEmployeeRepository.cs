using Entities.Models;
using Entities.RequestFeatures;

using System;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        PagedList<Employee> GetEmployees(Guid companyId, EmployeeParameters employeeParameters);

        Employee GetEmployee(Guid companyId, Guid Id);

        void CreateEmployeeForCompany(Guid CompanyId, Employee employee);

        void DeleteEmployee(Employee employee);
    }
}
