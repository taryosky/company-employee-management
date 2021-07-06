using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class EmployeeForCreationDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }
    }
}
