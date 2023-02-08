using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models.Database.DTO
{

    public class QueryInfo
    {
        /// <summary>
        /// /
        /// </summary>
        public string QueryText { get; set; }

        public Dictionary<string, object> Parameters { get; set; }

        public QueryType queryType { get; set; }

    }

    public class Query
    {
        public string QueryText { get; set; }
        public DynamicParameters Data { get; set; }
        public QueryType queryType { get; set; }

    }

    /// <summary>
    /// Query Type needed for command 
    /// </summary>
    public enum QueryType
    {
        /// <summary>
        /// Query Type: Stored procedure
        /// </summary>
        StoredProcedure,

        /// <summary>
        /// Query Type: Direct Text
        /// </summary>
        DirectText
    }
}
