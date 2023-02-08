using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Xml;
using Dapper;
using Newtonsoft.Json;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class RolesStructureRepo : IRolesStructureRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public RolesStructureRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public Task<RolesStructureListData> GetRoleStructureDetails(AdminRoleStructure AdminRoleStructure)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", AdminRoleStructure.UserID },
                    { "@ID", AdminRoleStructure.ID },
                    { "@Mode", AdminRoleStructure.Mode }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.ROLESSTRUCTURE_GETSTRUCTUREDETAILS,
                    Parameters = param
                };

                var result = _databaseOperations.GetFirstRecordAsync<RolesStructureListData>(queryInfo);

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

        public Task<IEnumerable<RolesStructureListData>> GetRoleStructureList(AdminRoleStructure AdminRoleStructure)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", AdminRoleStructure.UserID },
                    { "@Mode", AdminRoleStructure.Mode },
                    { "@StructureName", AdminRoleStructure.SearchText.Trim() }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.ROLESSTRUCTURE_GETSTRUCTURELIST,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<RolesStructureListData>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AddEditStructure(RolesStructureParam structure)
        {
            try
            {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ID", structure.ID, DbType.Int32);
                dynamicParameters.Add("@UserID", structure.UserID, DbType.Int32);
                dynamicParameters.Add("@StructureName", structure.Name, DbType.String);
                dynamicParameters.Add("@PathCount", structure.PathCount, DbType.Int32);
                dynamicParameters.Add("@IsPublic", structure.IsPublic, DbType.Int32);
                dynamicParameters.Add("@JSONData", structure.ResponseJSON, DbType.String);
                dynamicParameters.Add("@UT_V2RoleStructureLevelMap", structure.RoleStructureLevelMapData.ToDataTable().AsTableValuedParameter("[dbo].[UT_V2RoleStructureLevelMap]"));
                dynamicParameters.Add("@UT_V2RoleCompetencyLevelMap", structure.RoleCompetencyLevelMapData.ToDataTable().AsTableValuedParameter("[dbo].[UT_V2RoleCompetencyLevelMap]"));
                dynamicParameters.Add("@UT_V2RoleCompetencyLPMap", structure.RoleCompetencyLPMapData.ToDataTable().AsTableValuedParameter("[dbo].[UT_V2RoleCompetencyLPMap]"));
                //dynamicParameters.Add("IsSuccess", 0, DbType.Int32, ParameterDirection.Output);

                string procedurename = structure.ID > 0 ? StoredProcedures.ROLESSTRUCTURE_UPDATESTRUCTURE : StoredProcedures.ROLESSTRUCTURE_CREATESTRUCTURE;

                Query query = new Query()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = procedurename,
                    Data = dynamicParameters
                };

                return await _databaseOperations.ExecuteScalarAsyncInteger(query);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProcedureReturnParameters> AddStructure(RolesStructureParam structure)
        {
            try
            {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ID", structure.ID, DbType.Int32);
                dynamicParameters.Add("@UserID", structure.UserID, DbType.Int32);
                dynamicParameters.Add("@StructureName", structure.Name, DbType.String);
                dynamicParameters.Add("@PathCount", structure.PathCount, DbType.Int32);
                dynamicParameters.Add("@IsPublic", structure.IsPublic, DbType.Int32);
                dynamicParameters.Add("@JSONData", structure.ResponseJSON, DbType.String);
                dynamicParameters.Add("@UT_V2RoleStructureLevelMap", structure.RoleStructureLevelMapData.ToDataTable().AsTableValuedParameter("[dbo].[UT_V2RoleStructureLevelMap]"));
                dynamicParameters.Add("@UT_V2RoleCompetencyLevelMap", structure.RoleCompetencyLevelMapData.ToDataTable().AsTableValuedParameter("[dbo].[UT_V2RoleCompetencyLevelMap]"));
                dynamicParameters.Add("@UT_V2RoleCompetencyLPMap", structure.RoleCompetencyLPMapData.ToDataTable().AsTableValuedParameter("[dbo].[UT_V2RoleCompetencyLPMap]"));
                //dynamicParameters.Add("IsSuccess", 0, DbType.Int32, ParameterDirection.Output);

                string procedurename = StoredProcedures.ROLESSTRUCTURE_ADDSTRUCTURE;

                Query query = new Query()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = procedurename,
                    Data = dynamicParameters
                };

                return await _databaseOperations.GetFirstRecordAsync<ProcedureReturnParameters>(query);
            }
            catch
            {
                throw;
            }
        }

        public Task<int> DeleteStructure(DeleteStructure structure)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@ID", structure.ID },
                    { "@UserID", structure.UserID }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.ROLESSTRUCTURE_DELETESTRUCTURE,
                    Parameters = param
                };

                return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<LearningPathRoleMapping>> GetRoleStructureLearningPath(AdminRoleStructure AdminRoleStructure)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", AdminRoleStructure.UserID },
                    { "@Mode", AdminRoleStructure.Mode },
                    { "@ID", AdminRoleStructure.ID },
                };

                string procedureName = AdminRoleStructure.Mode == 1 ? StoredProcedures.GET_LEARNING_PATHS_BY_ACCOUNTID :  StoredProcedures.USERPROFILE_GETLPBYROLEID;

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = procedureName,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<LearningPathRoleMapping>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AddEditUserRoleCompetencyMap(UserRoleCompetencyMapParam userRoleCompetencyMapParam)
        {
            try
            {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ID", userRoleCompetencyMapParam.ID, DbType.Int32);
                dynamicParameters.Add("@UserID", userRoleCompetencyMapParam.UserID, DbType.Int32);
                dynamicParameters.Add("@RoleId", userRoleCompetencyMapParam.RoleId, DbType.String);
                dynamicParameters.Add("@TargetRoleId", userRoleCompetencyMapParam.TargetRoleId, DbType.Int32);
                dynamicParameters.Add("@RoleStructureId", userRoleCompetencyMapParam.RoleStructureId, DbType.Int32);
                dynamicParameters.Add("@UT_V2UserCompetencyMap", userRoleCompetencyMapParam.UserCompetencyMapData.ToDataTable().AsTableValuedParameter("[dbo].[UT_V2UserCompetencyMap]"));

                string procedurename = userRoleCompetencyMapParam.ID > 0 ? StoredProcedures.USERPROFILE_ADDCOMPETENCYMAP : StoredProcedures.USERPROFILE_EDITCOMPETENCYMAP;

                Query query = new Query()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = procedurename,
                    Data = dynamicParameters
                };

                return await _databaseOperations.ExecuteScalarAsyncInteger(query);
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserRoleCompetency> GetUserRoleCompetencyMap(AdminRoleStructure AdminRoleStructure)
        {
            var conn = _databaseOperations.CreateConnection();
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", AdminRoleStructure.UserID },
                    { "@Mode", AdminRoleStructure.Mode },
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.USERPROFILE_GETUSERROLECOMPETENCYMAP,
                    Parameters = param
                };

                var results = await _databaseOperations.GetMultipleResultAsync(conn, queryInfo);

                var userRoleMap = results.Read<UserRoleMap>().ToList();
                var userRoleCompetencyMap = results.Read<UserCompetencyMapList>().ToList();

                UserRoleCompetency userRoleCompetency = new()
                {
                    UserRoleMap = userRoleMap,
                    UserCompetencyMapList = userRoleCompetencyMap
                };

                return userRoleCompetency;
            }
            catch
            {
                throw;
            }
        }
        
    }
}