using System;
using System.Threading.Tasks;
using TurniTech.RepositoryPattern.Business.Contracts;
using TurniTech.RepositoryPattern.Data.Contracts;
using TurniTech.RepositoryPattern.Models;

namespace TurniTech.RepositoryPattern.Business.Impl
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            return await this.employeeRepository.AddItemAsync(employee);
        }

        public async Task<Employee> GetEmployee(string employeeId, string city)
        {
            return await this.employeeRepository.GetItemByIdAsync(employeeId, city);
        }
    }
}
