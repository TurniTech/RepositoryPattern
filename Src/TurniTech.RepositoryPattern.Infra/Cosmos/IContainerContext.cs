using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;
using TurniTech.RepositoryPattern.Infra.Storage;

namespace TurniTech.RepositoryPattern.Infra.Cosmos
{
    public interface IContainerContext<in T> where T : IEntity
    {
        /// <summary>
        /// Gets the Container name
        /// </summary>
        string ContainerId { get; }

        /// <summary>
        /// Generate the id
        /// </summary>
        /// <param name="entity">the entity</param>
        /// <returns>guid</returns>
        string GenerateId(T entity);

        /// <summary>
        /// Resolve PartitionKey
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Resolved PartitionKey
        /// </returns>
        PartitionKey ResolvePartitionKey(T entity);
    }
}
