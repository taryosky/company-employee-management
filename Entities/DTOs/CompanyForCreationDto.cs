using Entities.DTOs;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class CompanyForCreationDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

        public IEnumerable<EmployeeForCreationDto> Employees { get; set; }
    }
}
