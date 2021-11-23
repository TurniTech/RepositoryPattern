using System;
using System.Collections.Generic;
using System.Text;

namespace TurniTech.Common
{
    /// <summary>
    /// Error object
    /// </summary>
    public class Error
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public Error(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string? Description { get; set; }
    }
}
