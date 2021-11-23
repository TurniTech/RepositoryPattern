using Microsoft.Azure.Cosmos;
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
        public EmployeeRepository(CosmosClient cosmosClient) : base(cosmosClient)
        {
        }

        protected override string DatabaseName => "turnitech";

        protected override string ContainerId => "Employees";

        public override PartitionKey? ResolvePartitionKey(EmployeeEntity entity)
        {
            return base.ResolvePartitionKey(entity.City);
        }

        protected override EmployeeEntity Convert(Employee businessModel)
        {
            return new EmployeeEntity()
            {
                Id = businessModel.Id,
                Name = businessModel.Name,
                City = businessModel.City,
                Salary = businessModel.Salary
            };
        }

        protected override Employee Convert(EmployeeEntity dataModel)
        {
            return new Employee()
            {
                Id = dataModel.Id,
                Name = dataModel.Name,
                City = dataModel.City,
                Salary = dataModel.Salary
            };
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
