using System;
using System.Collections.Generic;
using System.Text;
using TurniTech.Common.Contracts;

namespace TurniTech.RepositoryPattern.Data.Entities
{
    public class EmployeeEntity : IEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public double Salary { get; set; }

        public string City { get; set; }
    }
}
