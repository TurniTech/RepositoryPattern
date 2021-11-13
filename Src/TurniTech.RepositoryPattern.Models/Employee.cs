using System;
using TurniTech.RepositoryPattern.Infra.Business;

namespace TurniTech.RepositoryPattern.Models
{
    public class Employee: IBusinessModel
    {
        public string Id { get; set; }
    }
}
