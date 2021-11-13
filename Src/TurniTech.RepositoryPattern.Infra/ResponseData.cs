using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurniTech.RepositoryPattern.Infra
{
    public class ResponseData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseData"/> class.
        /// </summary>
        public ResponseData()
        {
            this.HttpStatusCode = 200;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseData"/> class.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        /// <param name="error">The error.</param>
        public ResponseData(int errorCode, Error error)
        {
            this.HttpStatusCode = errorCode;
            this.Errors = new List<Error>() { error };
        }

        /// <summary>
        /// Gets or sets the HttpStatusCode property
        /// </summary>
        public int HttpStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the Errors property
        /// </summary>
        public IEnumerable<Error> Errors { get; set; }

        /// <summary>
        /// Gets the error response.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="error">The error.</param>
        /// <returns>The Error Response data</returns>
        public static ResponseData GetErrorResponse(int errorCode, Error error)
        {
            return new ResponseData(errorCode, error);
        }

        /// <summary>
        /// Gets the error response.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errors">The errors.</param>
        /// <returns>The Error Response data</returns>
        public static ResponseData GetErrorResponse(int errorCode, IEnumerable<Error> errors)
        {
            return new ResponseData() { Errors = errors, HttpStatusCode = errorCode };
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (this.Errors != null && this.Errors.Count() > 0)
            {
                return "ResponseData.Error.Name: " + this.Errors.First().Name + Environment.NewLine + "ResponseData.Error.Description: " + this.Errors.First().Description;
            }

            return base.ToString();
        }
    }

    /// <summary>
    /// Generic Error Response class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseData<T> : ResponseData
    {
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The result.</value>
        public T Results { get; set; }

        /// <summary>
        /// Gets the success response.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>The Success Response data</returns>
        public static ResponseData<T> GetSuccessResponse(T data)
        {
            return new ResponseData<T>() { Results = data };
        }

        /// <summary>
        /// Gets the success response.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="data">The data.</param>
        /// <returns>
        /// The Success Response data
        /// </returns>
        public static ResponseData<T> GetSuccessResponse(T data, System.Net.HttpStatusCode statusCode)
        {
            return new ResponseData<T>() { Results = data, HttpStatusCode = (int)statusCode };
        }
    }
}
