using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Company
    {
        [Column("CompanyId")]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
