using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class CompetencyRepo : ICompetencyRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public CompetencyRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public Task<CompetencyListData> GetCompetencyDetails(AdminCompetency AdminCompetency)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", AdminCompetency.UserID },
                    { "@ID", AdminCompetency.ID },
                    { "@Mode", AdminCompetency.Mode }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.COMPETENCY_GETCOMPETENCYDETAILS,
                    Parameters = param
                };

                var result = _databaseOperations.GetFirstRecordAsync<CompetencyListData>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<CompetencyListData>> GetCompetencyList(AdminCompetency AdminCompetency)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", AdminCompetency.UserID },
                    { "@CompetencyType", AdminCompetency.CompetencyType},
                    { "@Mode", AdminCompetency.Mode }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.COMPETENCY_GETCOMPETENCYLIST,
                    Parameters = param
                };

                // COMPETENCY

                var result = _databaseOperations.GetMultipleRecords<CompetencyListData>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<int> AddEditCompetency(Competency competency)
        {
            var param = new Dictionary<string, object>
            {
                { "@ID", competency.ID },
                { "@CompetencyTypeId", competency.CompetencyTypeId },
                { "@CompetencyName", competency.CompetencyName },
                { "@UserID", competency.UserID },
                { "@IsPublic", competency.IsPublic },
                { "@IsMandatory", competency.IsMandatory }
            };

            string procedurename = competency.ID > 0 ? StoredProcedures.COMPETENCY_UPDATECOMPETENCY : StoredProcedures.COMPETENCY_CREATECOMPETENCY;

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = procedurename,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<int> DeleteCompetency(DeleteCompetency competency)
        {
            var param = new Dictionary<string, object>
            {
                  { "@ID", competency.ID },
                  { "@UserID", competency.UserID }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.COMPETENCY_DELETECOMPETENCY,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<IEnumerable<CompetencyLevel>> GetCompetencyLevel(AdminCompetency AdminCompetency)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", AdminCompetency.UserID },
                    { "@Mode", AdminCompetency.Mode }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.COMPETENCY_GETCOMPETENCYLEVELLIST,
                    Parameters = param
                };

                // COMPETENCY

                var result = _databaseOperations.GetMultipleRecords<CompetencyLevel>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<CompetencyType>> GetCompetencyType(AdminCompetency AdminCompetency)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", AdminCompetency.UserID },
                    { "@Mode", AdminCompetency.Mode }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.COMPETENCY_GETCOMPETENCYTYPE,
                    Parameters = param
                };

                // COMPETENCY

                var result = _databaseOperations.GetMultipleRecords<CompetencyType>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}