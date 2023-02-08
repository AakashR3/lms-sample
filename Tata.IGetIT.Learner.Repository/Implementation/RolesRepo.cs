using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class RolesRepo : IRolesRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public RolesRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public Task<RolesListData> GetRoleDetails(AdminRole AdminRole)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", AdminRole.UserID },
                    { "@ID", AdminRole.ID },
                    { "@Mode", AdminRole.Mode }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.ROLESKILLCOMPETENCY_GETROLEDETAILS,
                    Parameters = param
                };

                var result = _databaseOperations.GetFirstRecordAsync<RolesListData>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<RolesListData>> GetRolesList(AdminRole AdminRole)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", AdminRole.UserID },
                    { "@Mode", AdminRole.Mode }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.ROLESKILLCOMPETENCY_GETROLESLIST,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<RolesListData>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<int> AddEditRole(MultipleRoles role)
        {
            var param = new Dictionary<string, object>
            {
                { "@ID", role.ID },
                { "@UserID", role.UserID },
                { "@RoleName", role.RoleName },
                { "@TotalExpMonths", role.TotalExpMonths },
                { "@IsPublic", role.IsPublic },
                { "@Status", role.Status }
            };

            string procedurename = role.ID > 0 ? StoredProcedures.ROLESKILLCOMPETENCY_UPDATEROLE : StoredProcedures.ROLESKILLCOMPETENCY_CREATEROLE;

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = procedurename,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<int> DeleteRole(DeleteRole role)
        {
            var param = new Dictionary<string, object>
            {
                  { "@ID", role.ID },
                  { "@UserID", role.UserID }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.ROLESKILLCOMPETENCY_DELETEROLE,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }
    }
}