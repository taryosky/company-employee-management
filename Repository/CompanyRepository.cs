using Contracts;

using Entities;
using Entities.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationContext context) : base(context)
        {
        }

        public void CreateCompany(Company Company)
        {
            Create(Company);
        }

        public IEnumerable<Company> GetAllCompanies(bool asNoTracking)
        {
            return FindAll(asNoTracking).OrderBy(c => c.Name).ToList();
        }

        public Company GetCompany(Guid companyId)
        {
            return FindByCondition(x => x.Id.Equals(companyId)).SingleOrDefault();
        }

        public IEnumerable<Company> GetCompaniesById(IEnumerable<Guid> Ids)
        {
            return FindByCondition(x => Ids.Contains(x.Id)).ToList();
        }

        public void DeleteCompany(Company company)
        {
            Delete(company);
        }
    }
}
