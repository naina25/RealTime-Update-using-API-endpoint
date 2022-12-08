namespace InternalAPI.Models
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string FullName { get; set; } = default!;
        public string Department { get; set; } = default!;
        public double Salary { get; set; }
    }
}
