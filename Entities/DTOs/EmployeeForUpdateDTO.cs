using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class EmployeeForUpdateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(18, int.MaxValue, ErrorMessage = "Age cannot be less than 18 yrs")]
        public int Age { get; set; }
    }
}
