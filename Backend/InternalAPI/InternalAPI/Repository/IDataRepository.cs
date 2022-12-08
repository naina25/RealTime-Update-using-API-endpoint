using InternalAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternalAPI.Repository
{
    public interface IDataRepository
    {
       List<Employee> GetAllEmployees();
    }
}
