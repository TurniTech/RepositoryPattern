using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TurniTech.RepositoryPattern.Infra.Cosmos.Extensions
{
    internal class CosmosAsyncQueryableProvider : IQueryProvider
    {
        private readonly IQueryProvider _provider;

        public CosmosAsyncQueryableProvider(IQueryProvider provider) => _provider = provider;

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) =>
            new CosmosAsyncQueryable<TElement>(_provider.CreateQuery<TElement>(expression));

        public IQueryable CreateQuery(Expression expression) => CreateQuery<object>(expression);

        public object Execute(Expression expression) => _provider.Execute(expression);

        public TResult Execute<TResult>(Expression expression) => _provider.Execute<TResult>(expression);

    }
}
