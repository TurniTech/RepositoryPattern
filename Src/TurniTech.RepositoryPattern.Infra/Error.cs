using System;
using System.Collections.Generic;
using System.Text;

namespace TurniTech.RepositoryPattern.Infra
{
    /// <summary>
    /// Error object
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
    }
}
