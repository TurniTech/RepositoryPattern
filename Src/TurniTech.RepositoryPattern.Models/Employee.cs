using System;
using TurniTech.Common.Contracts;

namespace TurniTech.RepositoryPattern.Models
{
    public class Employee: IBusinessModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public double Salary { get; set; }

        public string City { get; set; }
    }
}
