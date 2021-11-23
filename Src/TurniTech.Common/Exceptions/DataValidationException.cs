using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TurniTech.Common.Exceptions
{
    [Serializable]
    public class DataValidationException : BusinessException
    {
        const string eventCategory = "DataValidation";
        /// <summary>
        /// Initializes a new instance of the <see cref="DataValidationException"/> class
        /// </summary>
        public DataValidationException()
            : base(eventCategory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataValidationException"/> class
        /// </summary>
        /// <param name="message">error message</param>
        public DataValidationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataValidationException"/> class
        /// </summary>
        /// <param name="message">error message</param>
        /// <param name="innerException">inner exception</param>
        public DataValidationException(string message, Exception innerException)
            : base(message, innerException, eventCategory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataValidationException"/> class
        /// </summary>
        /// <param name="responseData">errors list</param>
        public DataValidationException(ResponseData responseData) : base(eventCategory)
        {
            this.ErrorResponse = responseData;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataValidationException"/> class
        /// </summary>
        /// <param name="info">serialization info</param>
        /// <param name="context">streaming context</param>
        protected DataValidationException(SerializationInfo info, StreamingContext context) : base(info, context, eventCategory)
        {
            if (info != null)
            {
                if (info.GetValue("Result", typeof(ResponseData)) is ResponseData errorResponse)
                {
                    this.ErrorResponse = errorResponse;
                }
            }
        }

        /// <summary>
        /// Gets the Errors property
        /// </summary>
        public ResponseData ErrorResponse { get; }

        /// <summary>
        /// Get the serialized object data
        /// </summary>
        /// <param name="info">serialization info</param>
        /// <param name="context">streaming context</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                info.AddValue("Result", this.ErrorResponse);
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "DataValidationException: " + this.ErrorResponse.ToString();
        }
    }
}
