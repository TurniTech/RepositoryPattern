using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace TurniTech.RepositoryPattern.Infra.Cosmos.Extensions
{
    internal class CosmosAsyncQueryable<TResult> : IEnumerable<TResult>, IQueryable<TResult>, IAsyncEnumerable<TResult>
    {
        private readonly IQueryable<TResult> _queryable;

        public CosmosAsyncQueryable(IQueryable<TResult> queryable)
        {
            _queryable = queryable;
            Provider = new CosmosAsyncQueryableProvider(queryable.Provider);
        }

        public Type ElementType => typeof(TResult);

        public Expression Expression => _queryable.Expression;

        public IQueryProvider Provider { get; }

        public IEnumerator<TResult> GetEnumerator() => _queryable.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _queryable.GetEnumerator();

        public async IAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            var iterator = _queryable.ToFeedIterator();

            while (iterator.HasMoreResults)
            {
                foreach (var item in await iterator.ReadNextAsync(cancellationToken))
                {
                    yield return item;
                }
            }
        }
    }
}
