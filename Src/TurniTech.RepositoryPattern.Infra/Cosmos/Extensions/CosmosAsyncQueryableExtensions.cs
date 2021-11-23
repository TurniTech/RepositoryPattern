using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurniTech.RepositoryPattern.Infra.Cosmos.Extensions
{
    internal static class CosmosAsyncQueryableExtensions
    {
        internal static IQueryable<T> ToCosmosAsyncQueryable<T>(this IOrderedQueryable<T> source)
        {
            return new CosmosAsyncQueryable<T>(source);
        }
    }
}
