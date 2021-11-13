using System;
using System.Collections.Generic;
using System.Text;

namespace TurniTech.RepositoryPattern.Infra.Exceptions
{
    public class TooManyRequestsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyRequestsException"/> class.
        /// </summary>
        public TooManyRequestsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyRequestsException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public TooManyRequestsException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyRequestsException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public TooManyRequestsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
