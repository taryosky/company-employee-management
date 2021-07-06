using Entities.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace Entities.Configurations
{
    public class CompanyEntityConfigurarion : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData(
                    new Company
                    {
                        Name = "Jane limited",
                        Address = "No 3, Asajon wan, Lagos",
                        Id = new Guid("24ad3a3b-463e-3cad-3bb1-26dca2197bad"),
                        Country = "Nigeria"
                    },
                    new Company
                    {
                        Name = "Tare Group",
                        Address = "No 3, Awongan, Yenagoa",
                        Id = new Guid("24ad3a3b-4aae-3cad-3bb1-26dca2197bad"),
                        Country = "Nigeria"
                    }
                );
        }
    }
}
