using System.Data;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class CommonRepo : ICommonRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public CommonRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public Task<IEnumerable<Plans>> GetAllPlans(int PlanCode)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@PlanCode", PlanCode }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetPlanDetails,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<Plans>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CategoryDetails> GetCategories(int CategoryID)
        {
            var conn = _databaseOperations.CreateConnection();
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@CategoryID", CategoryID }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetCategories,
                    Parameters = param
                };

                var results = await _databaseOperations.GetMultipleResultAsync(conn, queryInfo);

                if (results != null)
                {
                    try
                    {

                        var getMasterCategories = results.Read<GetMasterCategories>().ToList();
                        var getCategories = results.Read<GetCategories>().ToList();
                        CategoryDetails categoryDetails = new()
                        {
                            getMasterCategories = getMasterCategories,
                            getCategories = getCategories,
                        };

                        return categoryDetails;
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        public Task<IEnumerable<CourseTitles>> GetCourseTitles(int CategoryID, int SubCategoryID)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@CategoryID", CategoryID },
                    { "@SubCategoryID", SubCategoryID }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetCourseTitles,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<CourseTitles>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<IndividualPlans>> GetIndividualPlans(string CountryCode)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@CountryCode", CountryCode }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetIndividualPlans,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<IndividualPlans>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<GetSubCategoriesBasedOnCategoryID>> GetSubCategoriesBasedOnCategoryID(int CategoryID)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@intCategoryID", CategoryID }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetSubCategoriesBasedOnCategoryID,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<GetSubCategoriesBasedOnCategoryID>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<GetTopicsBasedOnCategoryID>> GetTopicsBasedOnCategoryID(int CategoryID)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@CategoryID", CategoryID }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetTopicsBasedOnCategoryID,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<GetTopicsBasedOnCategoryID>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
