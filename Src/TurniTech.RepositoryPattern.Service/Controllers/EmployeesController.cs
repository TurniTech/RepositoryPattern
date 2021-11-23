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

        [HttpPost]
        public async Task<Employee> Get(Employee employee)
        {
            return await this.employeeManager.AddEmployee(employee);
        }
    }
}
