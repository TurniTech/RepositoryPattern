using System;
using System.Collections.Generic;
using System.Text;

namespace TurniTech.Common.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExistsException"/> class.
        /// </summary>
        public EntityAlreadyExistsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">the message</param>
        public EntityAlreadyExistsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">the message</param>
        /// <param name="innerException">inner exception</param>
        public EntityAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
