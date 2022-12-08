using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeDataAPI.Models
{
    public class Employee
    {
        [Key]
        [Required]
        public int EmpId { get; set; }
        public string FullName { get; set; } = default!;
        public string Department { get; set; } = default!;
        public double Salary { get; set; }
    }
}
