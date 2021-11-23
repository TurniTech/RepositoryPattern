using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TurniTech.Common.Exceptions
{
    public class BusinessException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="eventCategory">The event category.</param>
        public BusinessException(string eventCategory)
        {
            this.EventCategory = eventCategory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="eventCategory">The event category.</param>
        public BusinessException(string message, string eventCategory) : base(message)
        {
            this.EventCategory = eventCategory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="eventCategory">The event category.</param>
        public BusinessException(string message, Exception innerException, string eventCategory) : base(message, innerException)
        {
            this.EventCategory = eventCategory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        /// <param name="eventCategory">The event category.</param>
        protected BusinessException(SerializationInfo info, StreamingContext context, string eventCategory) : base(info, context)
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
