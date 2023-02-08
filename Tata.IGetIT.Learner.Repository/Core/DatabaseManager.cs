using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tata.IGetIT.Learner.Repository.Models.Configurations;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;
using static Dapper.SqlMapper;

namespace Tata.IGetIT.Learner.Repository.Core
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly DBConfig _dbConfig;

        public IDbConnection CreateConnection()
        {
            var conn = new SqlConnection(_dbConfig.ActualConnectionString);
            conn.Open();
            return conn;
        }

        private CommandDefinition GetCommandDefinition(QueryInfo queryInfo)
        {
            var qParams = new DynamicParameters();

            queryInfo.Parameters.ToList().ForEach(x =>
            {
                qParams.Add(x.Key, x.Value);
            });

            return new CommandDefinition(queryInfo.QueryText, qParams, null, null, GetCommandType(queryInfo.queryType));
        }

        private CommandType GetCommandType(QueryType queryType)
        {
            return queryType switch
            {
                QueryType.StoredProcedure => CommandType.StoredProcedure,
                QueryType.DirectText => CommandType.Text,
                _ => CommandType.TableDirect
            };
        }

        public DatabaseManager(IOptions<DBConfig> dbConfig)
        {
            _dbConfig = dbConfig.Value;
        }

        public DatabaseManager()
        {
        }

        public async Task<GridReader> GetMultipleResultAsync(IDbConnection conn, QueryInfo queryInfo)
        {
            //var conn = CreateConnection();
            try
            {
                var cmd = GetCommandDefinition(queryInfo);
                var result = await conn.QueryMultipleAsync(cmd);
                return result;
            }
            catch
            {
                throw;
            }
            //finally
            //{
            //    if (conn.State != ConnectionState.Closed)
            //    {
            //        //TODO: DB Connection should be closed, for testing reason is commented. It needs to be addressed.
            //        //conn.Close();
            //    }
            //}
        }
        public async Task<IEnumerable<T>> GetMultipleRecords<T>(QueryInfo queryInfo)
        {
            var conn = CreateConnection();
            try
            {
                var cmd = GetCommandDefinition(queryInfo);
                var result = await conn.QueryAsync<T>(cmd);
                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        public async Task<T> GetFirstRecordAsync<T>(QueryInfo queryInfo)
        {
            var conn = CreateConnection();
            try
            {
                var cmd = GetCommandDefinition(queryInfo);
                var result = await conn.QueryFirstOrDefaultAsync<T>(cmd);
                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }


        public async Task<int> ExecuteNonQueryAsync(QueryInfo queryInfo)
        {
            var conn = CreateConnection();
            try
            {
                var cmd = GetCommandDefinition(queryInfo);
                var result = await conn.ExecuteAsync(cmd);
                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        public async Task<int> ExecuteNonQueryAsync(Query param)
        {
            var conn = CreateConnection();
            try
            {
                var cmd = new CommandDefinition(param.QueryText, param.Data, null, null, CommandType.StoredProcedure);
                var result = await conn.ExecuteAsync(cmd);
                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        public async Task<int> ExecuteScalarAsyncInteger(Query param)
        {
            var conn = CreateConnection();
            try
            {
                var cmd = new CommandDefinition(param.QueryText, param.Data, null, null, CommandType.StoredProcedure);
                var result = await conn.ExecuteScalarAsync<int>(cmd);
                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        public async Task<T> GetFirstRecordAsync<T>(Query param)
        {
            var conn = CreateConnection();
            try
            {
                var cmd = new CommandDefinition(param.QueryText, param.Data, null, null, CommandType.StoredProcedure);
                var result = await conn.QueryFirstOrDefaultAsync<T>(cmd);
                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }

        }
        public async Task<GridReader> GetMultipleResultAsync(IDbConnection conn, Query param)
        {
            var cmd = new CommandDefinition(param.QueryText, param.Data, null, null, CommandType.StoredProcedure);
            return await conn.QueryMultipleAsync(cmd);
        }

        public async Task<bool> ExecuteScalarAsyncBool(QueryInfo queryInfo)
        {
            var conn = CreateConnection();
            try
            {
                var cmd = GetCommandDefinition(queryInfo);
                var result = await conn.ExecuteScalarAsync<bool>(cmd);
                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        public async Task<int> ExecuteScalarAsyncInteger(QueryInfo queryInfo)
        {
            var conn = CreateConnection();
            try
            {
                var cmd = GetCommandDefinition(queryInfo);
                var result = await conn.ExecuteScalarAsync<int>(cmd);
                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        public async Task<string> ExecuteScalarAsyncString(QueryInfo queryInfo)
        {
            var conn = CreateConnection();
            try
            {
                var cmd = GetCommandDefinition(queryInfo);
                var result = await conn.ExecuteScalarAsync<string>(cmd);
                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

    }
}
