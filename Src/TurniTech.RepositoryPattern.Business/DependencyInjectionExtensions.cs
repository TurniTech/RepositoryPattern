using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TurniTech.RepositoryPattern.Business.Contracts;
using TurniTech.RepositoryPattern.Business.Impl;

namespace TurniTech.RepositoryPattern.Business
{
    public static class DependencyInjectionExtensions
    {
        public static void AddBusiness(this IServiceCollection serviceCollection)
        {
            
            serviceCollection.AddScoped<IEmployeeManager, EmployeeManager>();
        }
    }
}
