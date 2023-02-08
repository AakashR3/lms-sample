using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;
using static Dapper.SqlMapper;

namespace Tata.IGetIT.Learner.Repository.Core
{
    /// <summary>
    /// Core database operations only to be interacted with repos
    /// </summary>
    public interface IDatabaseManager
    {
        /// <summary>
        /// Returns multiple records against the query
        /// </summary>
        /// <typeparam name="T">Output type of each record</typeparam>
        /// <param name="queryInfo">Required Query info</param>
        /// <returns>Enumerable of defined record type</returns>
        Task<IEnumerable<T>> GetMultipleRecords<T>(QueryInfo queryInfo);
        IDbConnection CreateConnection();

        Task<GridReader> GetMultipleResultAsync(IDbConnection conn, QueryInfo queryInfo);
        Task<GridReader> GetMultipleResultAsync(IDbConnection conn, Query param);
        /// <summary>
        /// Returns first record that satisfies the query
        /// </summary>
        /// <typeparam name="T">Output type of the record</typeparam>
        /// <param name="queryInfo">Required Query info</param>
        /// <returns>Defined output record, null if not condition matches</returns>
        Task<T> GetFirstRecordAsync<T>(QueryInfo queryInfo);
        Task<int> ExecuteNonQueryAsync(QueryInfo queryInfo);
        Task<bool> ExecuteScalarAsyncBool(QueryInfo queryInfo);
        Task<int> ExecuteScalarAsyncInteger(QueryInfo queryInfo);
        Task<string> ExecuteScalarAsyncString(QueryInfo queryInfo);
        Task<int> ExecuteNonQueryAsync(Query param);
        Task<int> ExecuteScalarAsyncInteger(Query param);
        Task<T> GetFirstRecordAsync<T>(Query param);
    }
}
