using System;
using System.Collections.Generic;
using System.Text;

namespace TurniTech.RepositoryPattern.Infra.Storage
{
    public interface IEntity
    {
        string Id { get; set; }
    }
}
