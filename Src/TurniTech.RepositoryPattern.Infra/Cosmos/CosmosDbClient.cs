using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TurniTech.RepositoryPattern.Infra.Exceptions;

namespace TurniTech.RepositoryPattern.Infra.Cosmos
{
    public class CosmosDbClient : ICosmosDbClient
    {
        /// <summary>
        /// Database name
        /// </summary>
        private readonly string databaseId;

        /// <summary>
        /// Collection Name
        /// </summary>
        private readonly string containerId;

        /// <summary>
        /// Document Client
        /// </summary>
        private readonly CosmosClient cosmosClient;

        /// <summary>
        /// The container
        /// </summary>
        private Container container;

        /// <summary>
        /// Initializes a new instance of the <see cref="CosmosDbClient" /> class.
        /// </summary>
        /// <param name="databaseName">database name</param>
        /// <param name="cosmosClient">cosmos client</param>
        /// <param name="containerId">The container identifier.</param>
        /// <exception cref="System.ArgumentNullException">
        /// databaseName
        /// or
        /// containerId
        /// or
        /// cosmosClient
        /// </exception>
        /// <exception cref="ArgumentNullException">databaseName
        /// or
        /// cosmosClient
        /// or
        /// containerId</exception>
        public CosmosDbClient(string databaseName, CosmosClient cosmosClient, string containerId)
        {
            this.databaseId = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
            this.containerId = containerId ?? throw new ArgumentNullException(nameof(containerId));
            this.cosmosClient = cosmosClient ?? throw new ArgumentNullException(nameof(cosmosClient));

            this.Initialize();
        }

        /// <summary>
        /// Initializes the container.
        /// </summary>
        private void Initialize()
        {
            this.container = this.cosmosClient.GetContainer(this.databaseId, this.containerId);
        }

        /// <summary>
        /// Create DocumentAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <param name="partitionKey">The partition key.</param>
        /// <param name="itemRequestOptions">The item request options.</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>
        /// the item
        /// </returns>
        /// <exception cref="EntityAlreadyExistsException">Item already exists in partition={partitionKey}</exception>
        /// <exception cref="TooManyRequestsException">Too many requests.</exception>
        public async Task<ItemResponse<T>> CreateItemAsync<T>(T item, PartitionKey? partitionKey = null, ItemRequestOptions itemRequestOptions = null, CancellationToken cancellationToken = default)
        {
            return await this.container.CreateItemAsync<T>(item, partitionKey, itemRequestOptions, cancellationToken);
        }

        /// <summary>
        /// Delete the item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="partitionKey">The partition key.</param>
        /// <param name="itemRequestOptions">The item request options.</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>
        /// The item
        /// </returns>
        /// <exception cref="EntityNotFoundException">Item not found with id='{id}' in partition={partitionKey}</exception>
        /// <exception cref="TooManyRequestsException">Too many requests.</exception>
        public async Task<ItemResponse<T>> DeleteItemAsync<T>(string id, PartitionKey partitionKey, ItemRequestOptions itemRequestOptions = null, CancellationToken cancellationToken = default)
        {
            ItemResponse<T> result;
            try
            {
                result = await this.container.DeleteItemAsync<T>(id, partitionKey, itemRequestOptions, cancellationToken);
            }
            catch (CosmosException ce)
            {
                if (ce.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException($"Item not found with id='{id}' in partition={partitionKey}", ce);
                }
                else if (ce.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    throw new TooManyRequestsException("Too many requests.");
                }

                throw;
            }

            return result;
        }

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
        public async Task<IEnumerable<T>> QueryItemsAsync<T>(Expression<Func<T, bool>> predicate, bool allowSynchronousQueryExecution = false, CancellationToken cancellationToken = default)
        {
            List<T> result = new List<T>();
            var feedIterator = this.container.GetItemLinqQueryable<T>(allowSynchronousQueryExecution).Where(predicate).ToFeedIterator();
            while (feedIterator.HasMoreResults)
            {
                foreach (var item in await feedIterator.ReadNextAsync(cancellationToken))
                {
                    result.Add(item);
                }
            }
            return result;
        }

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
        public async Task<bool> SearchByFeedIterator<T>(Expression<Func<T, bool>> predicate, Func<List<T>, bool> condition, bool allowSynchronousQueryExecution = false, CancellationToken cancellationToken = default)
        {
            List<T> result = new List<T>();
            var feedIterator = this.container.GetItemLinqQueryable<T>(allowSynchronousQueryExecution).Where(predicate).ToFeedIterator();
            while (feedIterator.HasMoreResults)
            {
                foreach (var item in await feedIterator.ReadNextAsync(cancellationToken))
                {
                    result.Add(item);
                }
                if (condition(result) == true)
                    return true;
            }
            return false;
        }

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
        public async Task<long> GetItemsCountAsync<T>(Expression<Func<T, bool>> predicate, bool allowSynchronousQueryExecution = false, CancellationToken cancellationToken = default)
        {
            var result = await this.container.GetItemLinqQueryable<T>(allowSynchronousQueryExecution).Where(predicate).CountAsync();
            return result;
        }

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
        public async Task<IEnumerable<T>> QueryItemsAsync<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderByPredicate, int pageNumber, int pageSize, bool allowSynchronousQueryExecution = false, CancellationToken cancellationToken = default)
        {
            List<T> result = new List<T>();
            int resultsToSkip = (pageNumber - 1) * pageSize;
            var feedIterator = this.container.GetItemLinqQueryable<T>(allowSynchronousQueryExecution, null).Where(predicate).OrderByDescending(orderByPredicate).Skip(resultsToSkip).Take(pageSize).ToFeedIterator();
            while (feedIterator.HasMoreResults)
            {
                foreach (var item in await feedIterator.ReadNextAsync(cancellationToken))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// Reads the item asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="partitionKey">The partition key.</param>
        /// <param name="itemRequestOptions">The item request options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The item response
        /// </returns>
        /// <exception cref="EntityNotFoundException">Item not found with id='{id}' in partition={partitionKey}</exception>
        /// <exception cref="TooManyRequestsException">Too many requests.</exception>
        public async Task<ItemResponse<T>> ReadItemAsync<T>(string id, PartitionKey partitionKey, ItemRequestOptions itemRequestOptions = null, CancellationToken cancellationToken = default)
        {
            return await this.container.ReadItemAsync<T>(id, partitionKey, itemRequestOptions, cancellationToken);
        }

        /// <summary>
        /// Replaces the item asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="item">The item.</param>
        /// <param name="partitionKey">The partition key.</param>
        /// <param name="itemRequestOptions">The item request options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The item response
        /// </returns>
        /// <exception cref="EntityNotFoundException">Item not found with id='{id}' in partition={partitionKey}</exception>
        /// <exception cref="EntityAlreadyExistsException">Item already exists in partition={partitionKey}</exception>
        /// <exception cref="TooManyRequestsException">Too many requests.</exception>
        public async Task<ItemResponse<T>> ReplaceItemAsync<T>(string id, T item, PartitionKey? partitionKey = null, ItemRequestOptions itemRequestOptions = null, CancellationToken cancellationToken = default)
        {
            return await this.container.ReplaceItemAsync<T>(item, id, partitionKey, itemRequestOptions, cancellationToken);
        }

        /// <summary>
        /// Upsert the item
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="partitionKey">The partition key.</param>
        /// <param name="itemRequestOptions">The item request options.</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>
        /// The item
        /// </returns>
        public async Task<ItemResponse<T>> UpsertItemAsync<T>(T item, PartitionKey? partitionKey = null, ItemRequestOptions itemRequestOptions = null, CancellationToken cancellationToken = default)
        {
            return await this.container.UpsertItemAsync<T>(item, partitionKey, itemRequestOptions, cancellationToken);
        }
    }
}
