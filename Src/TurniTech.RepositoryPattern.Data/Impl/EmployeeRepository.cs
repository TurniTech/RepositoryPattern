using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TurniTech.RepositoryPattern.Data.Contracts;
using TurniTech.RepositoryPattern.Data.Entities;
using TurniTech.RepositoryPattern.Infra.Cosmos;
using TurniTech.RepositoryPattern.Models;

namespace TurniTech.RepositoryPattern.Data.Impl
{
    public class EmployeeRepository : BaseCosmosRepository<Employee, EmployeeEntity>, IEmployeeRepository
    {
        protected override string DatabaseName => throw new NotImplementedException();

        protected override string CollectionName => throw new NotImplementedException();

        protected override EmployeeEntity Convert(Employee businessModel)
        {
            throw new NotImplementedException();
        }

        protected override Employee Convert(EmployeeEntity dataModel)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<EmployeeEntity, bool>> Convert(Expression<Func<Employee, bool>> businessPredicate)
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<EmployeeEntity> Convert(IQueryable<Employee> businessQueryable)
        {
            throw new NotImplementedException();
        }
    }
}
