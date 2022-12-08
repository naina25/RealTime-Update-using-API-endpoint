using EmployeeDataAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EmployeeDataAPI.Data
{
    public class EmployeeContext:DbContext, IEmployee
    {
            public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
            {
            }
        public DbSet<Employee> Employees  => Set<Employee>();
    }

    public interface IEmployee
    {
        DbSet<Employee> Employees { get; }
    }
}
