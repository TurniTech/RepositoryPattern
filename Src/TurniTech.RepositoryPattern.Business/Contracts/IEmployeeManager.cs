using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TurniTech.RepositoryPattern.Models;

namespace TurniTech.RepositoryPattern.Business.Contracts
{
    public interface IEmployeeManager
    {

        Task<Employee> AddEmployee(Employee employee);

        Task<Employee> GetEmployee(string employeeId, string city);
    }
}
