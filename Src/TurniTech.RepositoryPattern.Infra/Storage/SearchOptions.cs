using System;
using System.Collections.Generic;
using System.Text;

namespace TurniTech.RepositoryPattern.Infra.Storage
{
    public class SearchOptions
    {
        /// <summary>
        /// max number of documents to be sent in one page
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// continuation token for querying delta
        /// </summary>
        public string ContinuationToken { get; set; }
        /// <summary>
        /// specifies whether cross partitionquerying is needed
        /// </summary>
        public bool EnableCrossPartitionQuery { get; set; }

        public bool AllowAsyncQueryExecution { get; set; }

        /// <summary>
        /// The partition key
        /// </summary>
        public string PartitionKey { get; set; }
    }
}
