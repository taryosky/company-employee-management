using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Employee
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
