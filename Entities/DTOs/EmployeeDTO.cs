using System;

namespace Entities.DTOs
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
