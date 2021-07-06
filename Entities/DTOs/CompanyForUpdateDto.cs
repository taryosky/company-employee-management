using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class CompanyForUpdateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        private IEnumerable<EmployeeForCreationDto> Employees { get; set; }
    }
}
