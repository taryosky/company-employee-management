using Contracts;

using Entities;

using System;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private ApplicationContext _applicationContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;

        public RepositoryManager(ApplicationContext context)
        {
            _applicationContext = context;
        }

        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_applicationContext);

                return _companyRepository;
            }
        }

        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_applicationContext);

                return _employeeRepository;
            }
        }

        public void Save()
        {
            _applicationContext.SaveChanges();
        }
    }
}
