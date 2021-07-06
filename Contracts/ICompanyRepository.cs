using Entities.Models;

using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies(bool asNoTracking);

        Company GetCompany(Guid companyId);

        void CreateCompany(Company Company);

        IEnumerable<Company> GetCompaniesById(IEnumerable<Guid> Ids);

        void DeleteCompany(Company company);
    }
}
