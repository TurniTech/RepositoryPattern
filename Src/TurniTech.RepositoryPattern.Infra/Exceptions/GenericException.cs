using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TurniTech.RepositoryPattern.Infra.Exceptions
{
    public class GenericException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EstorageException"/> class.
        /// </summary>
        /// <param name="eventCategory">The event category.</param>
        public GenericException(string eventCategory)
        {
            this.EventCategory = eventCategory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EstorageException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="eventCategory">The event category.</param>
        public GenericException(string message, string eventCategory) : base(message)
        {
            this.EventCategory = eventCategory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EstorageException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="eventCategory">The event category.</param>
        public GenericException(string message, Exception innerException, string eventCategory) : base(message, innerException)
        {
            this.EventCategory = eventCategory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EstorageException"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        /// <param name="eventCategory">The event category.</param>
        protected GenericException(SerializationInfo info, StreamingContext context, string eventCategory) : base(info, context)
        {
            this.EventCategory = eventCategory;
        }

        /// <summary>
        /// Gets the event category.
        /// </summary>
        /// <value>
        /// The event category.
        /// </value>
        public string EventCategory { get; }
    }
}
