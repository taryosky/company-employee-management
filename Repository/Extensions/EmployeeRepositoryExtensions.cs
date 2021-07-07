using Entities.Models;

using System;
using System.Linq;

namespace Repository.Extensions
{
    public static class EmployeeRepositoryExtensions
    {
        public static IQueryable<Employee>
            FilterEmployees(this IQueryable<Employee> source, uint minAge, uint maxAge) =>
            source.Where(x => x.Age >= minAge && x.Age <= maxAge);

        public static IQueryable<Employee> SearchEmployees(this IQueryable<Employee> source, string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return source;

            var lowercaseQuery = query.Trim().ToLower();
            return source.Where(x => x.Name.ToLower().Contains(lowercaseQuery));
        }

        public static IQueryable<Employee> Sort(this IQueryable<Employee> employees, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return employees.OrderBy(x => x.Name);

            return employees;
        }
    }
}
