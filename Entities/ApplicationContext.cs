using Entities.Configurations;
using Entities.Models;

using Microsoft.EntityFrameworkCore;

using System;

namespace Entities
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlite("Filename = mydb.sqlite3");
        //    optionsBuilder.appl
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyEntityConfigurarion());
            modelBuilder.ApplyConfiguration(new EmployeeEntityConfiguration());
        }
    }
}
