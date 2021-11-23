using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TurniTech.RepositoryPattern.Data.Contracts;
using TurniTech.RepositoryPattern.Data.Impl;

namespace TurniTech.RepositoryPattern.Data
{
    public static class DependencyInjectionExtensions
    {
        public static void AddData(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(new CosmosClient(""));
            serviceCollection.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
