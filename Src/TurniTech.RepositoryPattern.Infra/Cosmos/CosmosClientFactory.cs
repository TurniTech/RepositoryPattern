using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace TurniTech.RepositoryPattern.Infra.Cosmos
{
    public class CosmosClientFactory : ICosmosClientFactory
    {
        /// <summary>
        /// The database identifier
        /// </summary>
        private readonly string databaseId;

        /// <summary>
        /// The container ids
        /// </summary>
        private readonly List<string> containerIds;

        /// <summary>
        /// The cosmos client
        /// </summary>
        private readonly CosmosClient cosmosClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CosmosClientFactory{T}"/> class.
        /// </summary>
        /// <param name="databaseId">The database identifier.</param>
        /// <param name="containerIds">The container ids.</param>
        /// <param name="cosmosClient">The cosmos client.</param>
        public CosmosClientFactory(string databaseId, List<string> containerIds, CosmosClient cosmosClient)
        {
            this.databaseId = databaseId ?? throw new ArgumentNullException(nameof(databaseId));
            this.containerIds = containerIds ?? throw new ArgumentNullException(nameof(containerIds));
            this.cosmosClient = cosmosClient ?? throw new ArgumentNullException(nameof(cosmosClient));
        }

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <param name="containerId">The container identifier.</param>
        /// <returns>
        /// The cosmos client
        /// </returns>
        /// <exception cref="System.ArgumentException">Unable to find container: {containerId}</exception>
        public ICosmosDbClient GetClient(string containerId)
        {
            if (!this.containerIds.Contains(containerId))
            {
                throw new ArgumentException($"Unable to find container: {containerId}");
            }

            return new CosmosDbClient(this.databaseId, this.cosmosClient, containerId);
        }

        /// <summary>
        /// Ensure DBSetup Async
        /// </summary>
        /// <returns>the task</returns>
        public void EnsureDbSetupAsync()
        {
            var database = this.cosmosClient.GetDatabase(this.databaseId);

            foreach (var container in this.containerIds)
            {
                database.GetContainer(container);
            }
        }
    }
}
