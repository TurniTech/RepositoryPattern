using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TurniTech.Common.Contracts;

namespace TurniTech.RepositoryPattern.Infra.Storage
{
    public interface IRepository<TModel> where TModel : IBusinessModel, new()
    {
        /// <summary>
        /// Add an item to the persistance store
        /// </summary>
        /// <param name="model">The model object</param>
        /// <returns>The added Entity object</returns>
        Task<TModel> AddItemAsync(TModel model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets an item by id and partition key
        /// </summary>
        /// <param name="id">The item identifier</param>
        /// <param name="partitionKey">The partition key</param>
        /// <returns>The Entity item object</returns>
        Task<TModel> GetItemByIdAsync(string id, string partitionKey, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets an items predicate Experssion
        /// </summary>
        /// <param name="predicate">The Expression predicate</param>
        /// <param name="searchOptions">Search options</param>
        /// <returns>The collection of Entity item objects</returns>
        Task<IEnumerable<TModel>> GetItemsAsync(Expression<Func<TModel, bool>> predicate, SearchOptions searchOptions, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get Items by queryable 
        /// </summary>
        /// <param name="orderedQueryable">The ordered queryable object</param>
        /// <returns>The collection of Entity item objects</returns>
        IAsyncEnumerator<TModel> GetItemsAsync(IOrderedQueryable<TModel> orderedQueryable, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get Items count by queryable 
        /// </summary>
        /// <param name="orderedQueryable">The ordered queryable object</param>
        /// <returns>The count of items</returns>
        Task<int> GetItemsCountAsync(IOrderedQueryable<TModel> orderedQueryable, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates the document query
        /// </summary>
        /// <param name="searchOptions">Search options for querying</param>
        /// <returns>The Queryable object</returns>
        IOrderedQueryable<TModel> CreateDocumentQuery(SearchOptions searchOptions);

        /// <summary>
        /// Replaces an item by entity endentifier
        /// </summary>
        /// <param name="id">The Entity identifier</param>
        /// <param name="model">The Entity object</param>
        /// <returns>The Entity item object</returns>
        Task<TModel> ReplaceItemAsync(string id, TModel model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an items from persistance store
        /// </summary>
        /// <param name="id">The item indentifier</param>
        /// <param name="partitionKey">The partition key</param>
        /// <returns>The true if the operation is successful, otherwise false</returns>
        Task<bool> DeleteItemAsync(string id, string partitionKey, CancellationToken cancellationToken = default);
    }
}
