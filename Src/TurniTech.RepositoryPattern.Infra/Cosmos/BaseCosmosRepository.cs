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
using TurniTech.Common.Contracts;
using TurniTech.Common.Exceptions;
using TurniTech.RepositoryPattern.Infra.Storage;

namespace TurniTech.RepositoryPattern.Infra.Cosmos
{
    public abstract class BaseCosmosRepository<T, U> : IRepository<T>
        where T : IBusinessModel, new()
        where U : IEntity, new()
    {
        private readonly CosmosClient cosmosClient;
        private Container container;

        public BaseCosmosRepository(CosmosClient cosmosClient)
        {
            this.cosmosClient = cosmosClient ?? throw new ArgumentNullException(nameof(cosmosClient));
            this.Initialize();
        }

        /// <summary>
        /// Initializes the container.
        /// </summary>
        private void Initialize()
        
        {
            this.container = this.cosmosClient.GetContainer(this.DatabaseName, this.ContainerId);
        }

        public virtual string GenerateId(T businessModel) => Guid.NewGuid().ToString();

        /// <summary>
        /// The method to convert the businessModel object to Entity object
        /// </summary>
        /// <param name="businessModel">The business Model object</param>
        /// <returns>The Entity object</returns>
        protected abstract U Convert(T businessModel);

        /// <summary>
        /// The method to convert the Entity object to business Model object
        /// </summary>
        /// <param name="entity">The entity object</param>
        /// <returns>The business Model object</returns>
        protected abstract T Convert(U entity);

        /// <summary>
        /// Method to Covert Expression<Func<typeparamref name="T"/>, bool> to Expression<Func<typeparamref name="U"/>, bool>
        /// </summary>
        /// <param name="businessPredicate">The business Model predicate</param>
        /// <returns>The Entity Model predicate</returns>
        protected abstract Expression<Func<U, bool>> Convert(Expression<Func<T, bool>> businessPredicate);

        /// <summary>
        /// Method to Covert IQueryable<typeparamref name="T"/> to IQueryable<typeparamref name="U"/>
        /// </summary>
        /// <param name="businessQueryable">The Business Model queryable interface</param>
        /// <returns>The Entity queryable interface</returns>
        protected abstract IQueryable<U> Convert(IQueryable<T> businessQueryable);

        /// <summary>
        /// The cosmos db database name
        /// </summary>
        protected abstract string DatabaseName { get; }

        /// <summary>
        /// The cosmos db container name
        /// </summary>
        protected abstract string ContainerId { get; }

        /// <summary>
        /// Resolve the partition key from Data object
        /// </summary>
        /// <param name="entity">The data object</param>
        /// <returns>The partition key value</returns>
        public virtual PartitionKey? ResolvePartitionKey(U entity)
        {
            return null;
        }

        public virtual PartitionKey ResolvePartitionKey(string partitionKey)
        {
            return new PartitionKey(partitionKey);
        }

        public async Task<T> AddItemAsync(T model, CancellationToken cancellationToken = default)
        {
            try
            {
                model.Id = GenerateId(model);
                var entity = this.Convert(model);
                //entity.CreatedBy = identitySerice.ObjectId;
                //entity.CreatedOn = DateTime.UtcNow;
                var response = await this.container.CreateItemAsync<U>(entity, ResolvePartitionKey(entity), cancellationToken: cancellationToken).ConfigureAwait(false);
                
                return this.Convert(response.Resource);
            }
            catch (CosmosException ce)
            {
                if (ce.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new EntityAlreadyExistsException($"Item already exists in partition", ce);
                }
                else if (ce.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    throw new TooManyRequestsException("Too many requests.");
                }

                throw new StorageException("CosmosStorage Exception occurred.", ce);
            }
        }

        public async Task<T> GetItemByIdAsync(string id, string partitionKey, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await container.ReadItemAsync<U>(id, ResolvePartitionKey(partitionKey), cancellationToken: cancellationToken).ConfigureAwait(false);

                return this.Convert(response.Resource);
            }
            catch (CosmosException ce)
            {
                if (ce.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException($"Item not found with id='{id}' in partition={partitionKey}", ce);
                }
                else if (ce.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    throw new TooManyRequestsException("Too many requests.");
                }

                throw new StorageException("CosmosStorage Exception occurred.", ce);
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate, SearchOptions searchOptions, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async IAsyncEnumerator<T> GetItemsAsync(IOrderedQueryable<T> orderedQueryable, CancellationToken cancellationToken = default)
        {
            var entityQuery = this.Convert(orderedQueryable);
            var iterator = entityQuery.ToFeedIterator();

            while (iterator.HasMoreResults)
            {
                foreach (var item in await iterator.ReadNextAsync(cancellationToken))
                {
                    yield return this.Convert(item);
                }
            }
        }

        public async Task<int> GetItemsCountAsync(IOrderedQueryable<T> orderedQueryable, CancellationToken cancellationToken = default)
        {
            var entityQuery = this.Convert(orderedQueryable);
            return await entityQuery.CountAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<T> ReplaceItemAsync(string id, T model, CancellationToken cancellationToken = default)
        {
            
            try
            {
                var entity = this.Convert(model);
                //entity.UpdatedBy = identitySerice.ObjectId;
                //entity.UpdatedOn = DateTime.UtcNow;
                var response = await container.ReplaceItemAsync(entity, id, ResolvePartitionKey(entity), cancellationToken: cancellationToken).ConfigureAwait(false);
                return this.Convert(response.Resource);
            }
            catch (CosmosException ce)
            {
                if (ce.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException($"Item not found with id='{model.Id}' in a partition", ce);
                }
                else if (ce.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new EntityAlreadyExistsException($"Item already exists in the partition", ce);
                }
                else if (ce.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    throw new TooManyRequestsException("Too many requests.");
                }

                throw new StorageException("CosmosStorage Exception occurred.", ce);
            }
        }

        public async Task<bool> DeleteItemAsync(string id, string partitionKey, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await container.DeleteItemAsync<T>(id, ResolvePartitionKey(partitionKey), cancellationToken: cancellationToken).ConfigureAwait(false);
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (CosmosException ce)
            {
                if (ce.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException($"Item not found with id='{id}' in partition'{partitionKey}'", ce);
                }
                else if (ce.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    throw new TooManyRequestsException("Too many requests.");
                }

                throw new StorageException("CosmosStorage Exception occurred.", ce);
            }
        }

        public IOrderedQueryable<T> CreateDocumentQuery(SearchOptions searchOptions)
        {
            return this.container.GetItemLinqQueryable<T>(searchOptions.AllowAsyncQueryExecution, searchOptions.ContinuationToken, new QueryRequestOptions() { MaxItemCount = searchOptions.PageSize, PartitionKey = ResolvePartitionKey(searchOptions.PartitionKey)});
        }
    }
}
