using System;
using System.Collections.Generic;
using System.Text;

namespace TurniTech.RepositoryPattern.Infra.Cosmos
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Azure.Cosmos;

    /// <summary>
    /// CosmosDB Client 
    /// </summary>
    public interface ICosmosDbClient
    {
        /// <summary>
        /// Read item Async
        /// </summary>
        /// <param name="id">document Id</param>
        /// <param name="partitionKey">The partition key.</param>
        /// <param name="itemRequestOptions">The item request options.</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>
        /// the item
        /// </returns>
        Task<ItemResponse<T>> ReadItemAsync<T>(string id, PartitionKey partitionKey, ItemRequestOptions itemRequestOptions = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Query items Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">predicate for filtering</param>
        /// <param name="allowSynchronousQueryExecution">if set to <c>true</c> [allow synchronous query execution].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// the item
        /// </returns>
        Task<IEnumerable<T>> QueryItemsAsync<T>(Expression<Func<T, bool>> predicate, bool allowSynchronousQueryExecution = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Check condition by querying feed by feed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">predicate for filtering</param>
        /// <param name="condition">condition to be validated</param>
        /// <param name="allowSynchronousQueryExecution">if set to <c>true</c> [allow synchronous query execution].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// true if condition satisfied else false
        /// </returns>
        Task<bool> SearchByFeedIterator<T>(Expression<Func<T, bool>> predicate, Func<List<T>, bool> condition, bool allowSynchronousQueryExecution = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Count Items Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">predicate for filtering</param>
        /// <param name="allowSynchronousQueryExecution">if set to <c>true</c> [allow synchronous query execution].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// the count
        /// </returns>
        Task<long> GetItemsCountAsync<T>(Expression<Func<T, bool>> predicate, bool allowSynchronousQueryExecution = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create DocumentAsync
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="partitionKey">The partition key.</param>
        /// <param name="itemRequestOptions">The item request options.</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>
        /// the item
        /// </returns>
        Task<ItemResponse<T>> CreateItemAsync<T>(T item, PartitionKey? partitionKey = null, ItemRequestOptions itemRequestOptions = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// QueryItemsAsync.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">predicate for filtering.</param>
        /// <param name="count">Return specified number of elements.</param>
        /// <param name="allowSynchronousQueryExecution">if set to <c>true</c> [allow synchronous query execution].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// the item
        /// </returns>
        Task<IEnumerable<T>> QueryItemsAsync<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderByPredicate, int pageNumber, int pageSize, bool allowSynchronousQueryExecution = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Replace the item
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="item">The item.</param>
        /// <param name="partitionKey">The partition key.</param>
        /// <param name="itemRequestOptions">The item request options.</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>
        /// The item
        /// </returns>
        Task<ItemResponse<T>> ReplaceItemAsync<T>(string id, T item, PartitionKey? partitionKey = null, ItemRequestOptions itemRequestOptions = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Upsert the item
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="item">The item.</param>
        /// <param name="partitionKey">The partition key.</param>
        /// <param name="itemRequestOptions">The item request options.</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>
        /// The item
        /// </returns>
        Task<ItemResponse<T>> UpsertItemAsync<T>(T item, PartitionKey? partitionKey = null, ItemRequestOptions itemRequestOptions = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete the item
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="partitionKey">The partition key.</param>
        /// <param name="itemRequestOptions">The item request options.</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>
        /// The item
        /// </returns>
        Task<ItemResponse<T>> DeleteItemAsync<T>(string id, PartitionKey partitionKey, ItemRequestOptions itemRequestOptions = null, CancellationToken cancellationToken = default);
    }
}
