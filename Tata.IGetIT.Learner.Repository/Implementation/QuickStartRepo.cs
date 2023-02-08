using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class QuickStartRepo : IQuickStartRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public QuickStartRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public Task<IEnumerable<QuickStartGridData>> GetQuickStartGridData(QuickStartGrid quickStartGrid)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@RptUserID", quickStartGrid.UserID },
                    { "@SubCatID", quickStartGrid.SubCategoryID },
                    { "@CatID", quickStartGrid.CategoryID },
                    { "@SearchText", quickStartGrid.SearchText }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.QUICKSTART_GETQUICKSTARTGRIDDATA,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<QuickStartGridData>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<Categories>> GetCategories(QuickStartGrid quickStartGrid)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", quickStartGrid.UserID },
                    { "@Type", quickStartGrid.Type }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.QUICKSTART_GETCATEGORY,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<Categories>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<SubCategories>> GetSubCategories(QuickStartGrid quickStartGrid)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", quickStartGrid.UserID },
                    { "@CategoryID", quickStartGrid.CategoryID },
                    { "@Type", quickStartGrid.Type }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.QUICKSTART_GETSUBCATEGORY,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<SubCategories>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<int> GetNotificationFlag(QuickStartNotification quickStartNotification)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", quickStartNotification.UserID },
                    { "@Flag", quickStartNotification.Flag }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.QUICKSTART_GETQUICKSARTNEWRELEASENOTIFICATION,
                    Parameters = param
                };

                var result = _databaseOperations.GetFirstRecordAsync<int>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<int> UpdateNotificationFlag(QuickStartNotification quickStartNotification)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", quickStartNotification.UserID },
                    { "@NewQuickstartsReleaseFlag", quickStartNotification.NewQuickstartsReleaseFlag }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.QUICKSTART_UPDATEQUICKSARTNEWRELEASENOTIFICATION,
                    Parameters = param
                };

                var result = _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}