using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TurniTech.Common.Contracts;
using TurniTech.Common.Exceptions;
using TurniTech.RepositoryPattern.Infra.Storage;

namespace TurniTech.RepositoryPattern.Infra.Cosmos
{
    //public abstract class CosmosDbRepository<T> : IRepository<T>, IContainerContext<T> where T : IEntity, new()
    //{
    //    /// <summary>
    //    /// Cosmos DB factory
    //    /// </summary>
    //    private readonly ICosmosClientFactory cosmosClientFactory;

    //    /// <summary>
    //    /// The identity serice
    //    /// </summary>
    //    //private readonly IIdentityService identitySerice;

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="CosmosDbRepository{T}" /> class.
    //    /// </summary>
    //    /// <param name="cosmosClientFactory">The cosmos client factory.</param>
    //    protected CosmosDbRepository(ICosmosClientFactory cosmosClientFactory)//, IIdentityService identityService)
    //    {
    //        this.cosmosClientFactory = cosmosClientFactory;
    //        //this.identitySerice = identityService;
    //    }

    //    /// <summary>
    //    /// Gets the Container Id
    //    /// </summary>
    //    public abstract string ContainerId { get; }

    //    /// <summary>
    //    /// Add the entity
    //    /// </summary>
    //    /// <param name="entity">the entity</param>
    //    /// <param name="cancellationToken">Cancellation token</param>
    //    /// <returns>
    //    /// added entity
    //    /// </returns>
    //    /// <exception cref="EntityAlreadyExistsException">Item already exists in partition</exception>
    //    /// <exception cref="TooManyRequestsException">Too many requests.</exception>
    //    public async Task<T> AddItemAsync(T entity, CancellationToken cancellationToken = default)
    //    {
    //        try
    //        {
    //            entity.Id = GenerateId(entity);
    //            //entity.CreatedBy = identitySerice.ObjectId;
    //            //entity.CreatedOn = DateTime.UtcNow;
    //            var cosmosClient = this.cosmosClientFactory.GetClient(this.ContainerId);
    //            var response = await cosmosClient.CreateItemAsync<T>(entity, ResolvePartitionKey(entity), cancellationToken: cancellationToken).ConfigureAwait(true);

    //            return response.Resource;
    //        }
    //        catch (CosmosException ce)
    //        {
    //            if (ce.StatusCode == HttpStatusCode.Conflict)
    //            {
    //                throw new EntityAlreadyExistsException($"Item already exists in partition", ce);
    //            }
    //            else if (ce.StatusCode == HttpStatusCode.TooManyRequests)
    //            {
    //                throw new TooManyRequestsException("Too many requests.");
    //            }

    //            throw;
    //        }
    //    }

    //    /// <summary>
    //    /// Deletes the item asynchronous.
    //    /// </summary>
    //    /// <param name="entity">The entity.</param>
    //    /// <param name="cancellationToken">Cancellation token</param>
    //    /// <returns>The status true/false</returns>
    //    /// <exception cref="EntityNotFoundException">Item not found with id='{entity.Id}' in partition</exception>
    //    /// <exception cref="TooManyRequestsException">Too many requests.</exception>
    //    public async Task<bool> DeleteItemAsync(T entity, CancellationToken cancellationToken = default)
    //    {
    //        try
    //        {
    //            var cosmosClient = this.cosmosClientFactory.GetClient(this.ContainerId);
    //            var response = await cosmosClient.DeleteItemAsync<T>(entity.Id, ResolvePartitionKey(entity), cancellationToken: cancellationToken).ConfigureAwait(true);
    //            return response.StatusCode.IsSuccessStatusCode();
    //        }
    //        catch (CosmosException ce)
    //        {
    //            if (ce.StatusCode == HttpStatusCode.NotFound)
    //            {
    //                throw new EntityNotFoundException($"Item not found with id='{entity.Id}' in partition", ce);
    //            }
    //            else if (ce.StatusCode == HttpStatusCode.TooManyRequests)
    //            {
    //                throw new TooManyRequestsException("Too many requests.");
    //            }

    //            throw;
    //        }
    //    }

    //    /// <summary>
    //    /// Generate the id
    //    /// </summary>
    //    /// <param name="entity">the entity</param>
    //    /// <returns>
    //    /// guid
    //    /// </returns>
    //    public virtual string GenerateId(T entity) => Guid.NewGuid().ToString();

    //    /// <summary>
    //    /// Get Item By Id Async
    //    /// </summary>
    //    /// <param name="id">Entity identifier</param>
    //    /// <param name="cancellationToken">Cancellation token</param>
    //    /// <returns>
    //    /// the item object
    //    /// </returns>
    //    /// <exception cref="EntityNotFoundException">Item not found with id='{id}' in partition={partitionKey}</exception>
    //    /// <exception cref="TooManyRequestsException">Too many requests.</exception>
    //    public async Task<T> GetItemByIdAsync(string id, CancellationToken cancellationToken = default)
    //    {
    //        T entity = default;
    //        var partitionKey = this.ResolvePartitionKey(entity);
    //        try
    //        {
    //            var cosmosClient = this.cosmosClientFactory.GetClient(this.ContainerId);
    //            var response = await cosmosClient.ReadItemAsync<T>(id, partitionKey, cancellationToken: cancellationToken).ConfigureAwait(true);

    //            return response.Resource;
    //        }
    //        catch (CosmosException ce)
    //        {
    //            if (ce.StatusCode == System.Net.HttpStatusCode.NotFound)
    //            {
    //                throw new EntityNotFoundException($"Item not found with id='{id}' in partition={partitionKey}", ce);
    //            }
    //            else if (ce.StatusCode == HttpStatusCode.TooManyRequests)
    //            {
    //                throw new TooManyRequestsException("Too many requests.");
    //            }

    //            throw;
    //        }
    //    }

    //    /// <summary>
    //    /// Gets the items asynchronous.
    //    /// </summary>
    //    /// <param name="predicate">The predicate.</param>
    //    /// <param name="allowSynchronousQueryExecution">if set to <c>true</c> [allow synchronous query execution].</param>
    //    /// <returns>the list of items</returns>
    //    public async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate, bool allowSynchronousQueryExecution = false, CancellationToken cancellationToken = default)
    //    {
    //        var cosmosClient = this.cosmosClientFactory.GetClient(this.ContainerId);
    //        return await cosmosClient.QueryItemsAsync<T>(predicate, allowSynchronousQueryExecution, cancellationToken: cancellationToken).ConfigureAwait(true);
    //    }

    //    /// <summary>
    //    /// Gets the items asynchronous.
    //    /// </summary>
    //    /// <param name="predicate">The predicate.</param>
    //    ///<param name="count">No of elements to return</param>
    //    /// <param name="allowSynchronousQueryExecution">if set to <c>true</c> [allow synchronous query execution].</param>
    //    /// <returns>the list of items</returns>
    //    public async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderByPredicate, int pageNumber, int pageSize, bool allowSynchronousQueryExecution = false, CancellationToken cancellationToken = default)
    //    {
    //        var cosmosClient = this.cosmosClientFactory.GetClient(this.ContainerId);
    //        return await cosmosClient.QueryItemsAsync<T>(predicate, orderByPredicate, pageNumber, pageSize, allowSynchronousQueryExecution, cancellationToken: cancellationToken).ConfigureAwait(true);
    //    }

    //    /// <summary>
    //    /// Gets count of the items asynchronous.
    //    /// </summary>
    //    /// <param name="predicate">The predicate.</param>
    //    /// <param name="allowSynchronousQueryExecution">if set to <c>true</c> [allow synchronous query execution].</param>
    //    /// <returns>the count of items</returns>
    //    public async Task<long> GetItemsCountAsync(Expression<Func<T, bool>> predicate, bool allowSynchronousQueryExecution = false, CancellationToken cancellationToken = default)
    //    {
    //        var cosmosClient = this.cosmosClientFactory.GetClient(this.ContainerId);
    //        return await cosmosClient.GetItemsCountAsync<T>(predicate, allowSynchronousQueryExecution, cancellationToken: cancellationToken).ConfigureAwait(true);
    //    }

    //    /// <summary>
    //    /// Resolve PartitionKey
    //    /// </summary>
    //    /// <param name="entity">The entity.</param>
    //    /// <returns>
    //    /// Resolved PartitionKey
    //    /// </returns>
    //    public virtual PartitionKey ResolvePartitionKey(T entity) => new PartitionKey(string.Empty);

    //    /// <summary>
    //    /// Updates the item asynchronous.
    //    /// </summary>
    //    /// <param name="entity">The entity.</param>
    //    /// <param name="cancellationToken">Cancellation token</param>
    //    /// <returns>The updated item</returns>
    //    /// <exception cref="EntityNotFoundException">Item not found with id='{entity.Id}' in partition={partitionKey}</exception>
    //    /// <exception cref="EntityAlreadyExistsException">Item already exists in partition={partitionKey}</exception>
    //    /// <exception cref="TooManyRequestsException">Too many requests.</exception>
    //    public async Task<T> UpdateItemAsync(T entity, CancellationToken cancellationToken = default)
    //    {
    //        var partitionKey = this.ResolvePartitionKey(entity);
    //        try
    //        {
    //            //entity.UpdatedBy = identitySerice.ObjectId;
    //            //entity.UpdatedOn = DateTime.UtcNow;
    //            var cosmosClient = this.cosmosClientFactory.GetClient(this.ContainerId);
    //            var response = await cosmosClient.ReplaceItemAsync(entity.Id, entity, partitionKey, cancellationToken: cancellationToken).ConfigureAwait(true);
    //            return response.Resource;
    //        }
    //        catch (CosmosException ce)
    //        {
    //            if (ce.StatusCode == System.Net.HttpStatusCode.NotFound)
    //            {
    //                throw new EntityNotFoundException($"Item not found with id='{entity.Id}' in partition={partitionKey}", ce);
    //            }
    //            else if (ce.StatusCode == HttpStatusCode.Conflict)
    //            {
    //                throw new EntityAlreadyExistsException($"Item already exists in partition={partitionKey}", ce);
    //            }
    //            else if (ce.StatusCode == HttpStatusCode.TooManyRequests)
    //            {
    //                throw new TooManyRequestsException("Too many requests.");
    //            }

    //            throw;
    //        }
    //    }

    //    /// <summary>
    //    /// Upserts the entity
    //    /// </summary>
    //    /// <param name="entity">the entity</param>
    //    /// <param name="cancellationToken">Cancellation token</param>
    //    /// <returns>the Item</returns>
    //    public async Task<T> UpsertItemAsync(T entity, CancellationToken cancellationToken = default)
    //    {
    //        var partitionKey = this.ResolvePartitionKey(entity);
    //        try
    //        {
    //            //entity.UpdatedBy = identitySerice.ObjectId;
    //            //entity.UpdatedOn = DateTime.UtcNow;
    //            var cosmosClient = this.cosmosClientFactory.GetClient(this.ContainerId);
    //            var response = await cosmosClient.UpsertItemAsync(entity, partitionKey, cancellationToken: cancellationToken).ConfigureAwait(true);
    //            return response.Resource;
    //        }
    //        catch (CosmosException ce)
    //        {
    //            if (ce.StatusCode == HttpStatusCode.Conflict)
    //            {
    //                throw new EntityAlreadyExistsException($"Item already exists in partition={partitionKey}", ce);
    //            }
    //            else if (ce.StatusCode == HttpStatusCode.TooManyRequests)
    //            {
    //                throw new TooManyRequestsException("Too many requests.");
    //            }

    //            throw;
    //        }
    //    }
    //}
}
