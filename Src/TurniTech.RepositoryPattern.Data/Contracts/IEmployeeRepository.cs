using System;
using System.Collections.Generic;
using System.Text;
using TurniTech.RepositoryPattern.Infra.Storage;
using TurniTech.RepositoryPattern.Models;

namespace TurniTech.RepositoryPattern.Data.Contracts
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
    }
}
