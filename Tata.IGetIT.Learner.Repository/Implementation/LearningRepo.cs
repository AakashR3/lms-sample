using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class LearningRepo : ILearningRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public LearningRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public Task<IEnumerable<UserHistoryGridData>> GetUserHistoryGridData(UserHistory UserHistory)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@userid", UserHistory.UserID },
                    { "@ItemType", UserHistory.ItemType }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.LEARNING_GETHISTORY,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<UserHistoryGridData>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<MyLearningGridData>> GetMyLearningGridData(MyLearning MyLearning)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@userid", MyLearning.UserID },
                    { "@ItemType", MyLearning.ItemType },
                    { "@Favorites", MyLearning.Favorites },
                    { "@SearchText", MyLearning.SearchText },
                    { "@CategoryID", MyLearning.CategoryID },
                    { "@Status", MyLearning.Status }
                };

                string procedureName = MyLearning.ItemType == 1 ? StoredProcedures.LEARNING_GETMYLEARNINGCOURSE : StoredProcedures.LEARNING_GETMYLEARNINGASSESSMENT ;

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = procedureName,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<MyLearningGridData>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<string> AddFavoriteItem(AddRemoveFavorite AddRemoveFavorite)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@userid", AddRemoveFavorite.UserID },
                    { "@itemid", AddRemoveFavorite.ItemID },
                    { "@type", AddRemoveFavorite.Type }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.LEARNING_ADDFAVORITEITEM,
                    Parameters = param
                };

                var result = _databaseOperations.ExecuteScalarAsyncString(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<string> RemoveFavoriteItem(AddRemoveFavorite AddRemoveFavorite)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@userid", AddRemoveFavorite.UserID },
                    { "@itemid", AddRemoveFavorite.ItemID },
                    { "@type", AddRemoveFavorite.Type }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.LEARNING_REMOVEFAVORITEITEM,
                    Parameters = param
                };

                var result = _databaseOperations.ExecuteScalarAsyncString(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<LearningPathList>> GetMyLearningPath(MyLearning MyLearning)
        {
            try
            {
                var param = new Dictionary<string, object>();
                param.Add("@UserID", MyLearning.UserID);
                param.Add("@SearchText", MyLearning.SearchText);
                param.Add("@Status", MyLearning.Status);
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.LEARNING_GETLEARNINGPATH,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<LearningPathList>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<DownloadCertificateInfo> GetDownloadCertificate(DownloadCertificate downloadCertificate)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@ID", downloadCertificate.EventID },
                    { "@ItemID", downloadCertificate.ItemID },
                    { "@UserID", downloadCertificate.UserID },
                    { "@IDType", downloadCertificate.IDType },
                    { "@Mode", downloadCertificate.Mode }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.LEARNING_DOWNLOADCERTIFICATE,
                    Parameters = param
                };

                var result = _databaseOperations.GetFirstRecordAsync<DownloadCertificateInfo>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

    }
}