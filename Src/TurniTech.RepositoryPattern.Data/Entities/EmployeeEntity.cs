using System;
using System.Collections.Generic;
using System.Text;
using TurniTech.RepositoryPattern.Infra.Storage;

namespace TurniTech.RepositoryPattern.Data.Entities
{
    public class EmployeeEntity : IEntity
    {
        public string Id { get; set; }
    }
}
