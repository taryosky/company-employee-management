using Entities.Models;

using Microsoft.EntityFrameworkCore;

using System;

namespace Entities.Configurations
{
    public class EmployeeEntityConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Employee> builder)
        {
            //builder.HasData(
            //        new Employee
            //        {
            //            Name = "Clement Azibataram",
            //            Age = 70,
            //            CompanyId = new Guid("24ad3a3b-463e-3cad-3bb1-26dca2197bad")
            //        },
            //        new Employee
            //        {
            //            Name = "richard Aborugbene",
            //            Age = 78,
            //            CompanyId = new Guid("24ad3a3b-4aae-3cad-3bb1-26dca2197bad")
            //        },
            //        new Employee
            //        {
            //            Name = "Chucks Onuh",
            //            Age = 90,
            //            CompanyId = new Guid("24ad3a3b-463e-3cad-3bb1-26dca2197bad")
            //        }
            //    );
        }
    }
}
