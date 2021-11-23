using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TurniTech.RepositoryPattern.Business.Contracts;
using TurniTech.RepositoryPattern.Models;

namespace TurniTech.RepositoryPattern.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeManager employeeManager;

        public EmployeesController(IEmployeeManager employeeManager)
        {
            this.employeeManager = employeeManager;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Employee> Get(string id)
        {
            return await this.employeeManager.GetEmployee(id, "hyd");
        }

        [HttpGet]
        public async Task<Employee> Get()
        {
            return await this.employeeManager.AddEmployee(new Employee() { Name = "test", City = "hyd", Salary = 1000000 });
        }
    }
}
