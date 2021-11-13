using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TurniTech.RepositoryPattern.Infra.Business;
using TurniTech.RepositoryPattern.Infra.Storage;

namespace TurniTech.RepositoryPattern.Infra.Cosmos
{
    public abstract class BaseCosmosRepository<T, U> : IRepository<T>
        where T : IBusinessModel, new()
        where U : IEntity, new()
    {

        /// <summary>
        /// The method to convert the businessModel object to Data Model object
        /// </summary>
        /// <param name="businessModel">The business Model object</param>
        /// <returns>The Data Model object</returns>
        protected abstract U Convert(T businessModel);

        /// <summary>
        /// The method to convert the Data Model object to business Model object
        /// </summary>
        /// <param name="dataModel">The Data Model object</param>
        /// <returns>The business Model object</returns>
        protected abstract T Convert(U dataModel);

        /// <summary>
        /// Method to Covert Expression<Func<typeparamref name="T"/>, bool> to Expression<Func<typeparamref name="U"/>, bool>
        /// </summary>
        /// <param name="businessPredicate">The business Model predicate</param>
        /// <returns>The Data Model predicate</returns>
        protected abstract Expression<Func<U, bool>> Convert(Expression<Func<T, bool>> businessPredicate);

        /// <summary>
        /// Method to Covert IQueryable<typeparamref name="T"/> to IQueryable<typeparamref name="U"/>
        /// </summary>
        /// <param name="businessQueryable">The Business Model queryable interface</param>
        /// <returns>The Data Model queryable interface</returns>
        protected abstract IQueryable<U> Convert(IQueryable<T> businessQueryable);

        /// <summary>
        /// The cosmos db database name
        /// </summary>
        protected abstract string DatabaseName { get; }

        /// <summary>
        /// The cosmos db collection or container name
        /// </summary>
        protected abstract string CollectionName { get; }

        /// <summary>
        /// Resolve the partition key from Data object
        /// </summary>
        /// <param name="entity">The data object</param>
        /// <returns>The partition key value</returns>
        public virtual string ResolvePartitionKey(U entity)
        {
            return null;
        }

        /// <inheritdoc/>
        public async Task<T> AddItemAsync(T entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<T> GetItemByIdAsync(string id, string partitionKeyValue)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate, SearchOptions searchOptions)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetItemsAsync(IOrderedQueryable<T> orderedQueryable)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<int> GetItemsCountAsync(IOrderedQueryable<T> orderedQueryable)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IOrderedQueryable<T> CreateDocumentQuery(SearchOptions searchOptions)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<T> ReplaceItemAsync(string id, T entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteItemAsync(string id, string partitionKey)
        {
            throw new NotImplementedException();
        }
    }
}
