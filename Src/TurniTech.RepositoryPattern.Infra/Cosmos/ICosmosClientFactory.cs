using System;
using System.Collections.Generic;
using System.Text;

namespace TurniTech.RepositoryPattern.Infra.Cosmos
{
    public interface ICosmosClientFactory
    {
        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <param name="containerId">The container identifier.</param>
        /// <returns>
        /// The cosmos client
        /// </returns>
        /// <exception cref="System.ArgumentException">Unable to find container: {containerId}</exception>
        ICosmosDbClient GetClient(string containerId);
    }
}
