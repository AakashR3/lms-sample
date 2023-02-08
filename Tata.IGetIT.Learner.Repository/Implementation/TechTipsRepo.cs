using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class TechTipsRepo : ITechTipsRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public TechTipsRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public Task<IEnumerable<TechTips>> GetTechTips(int UserID, int CategoryID, int SubCategoryID, int TopicID,
            int Filter, string SearchTag, int SearchInID, int SearchInTag, int SearchInTitle, int SearchInContent)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", UserID },
                    { "@CategoryID", CategoryID },
                    { "@SubCategoryID", SubCategoryID },
                    { "@TopicID", TopicID },
                    { "@Filter", Filter },
                    { "@SearchTag", SearchTag },
                    { "@SearchInID", SearchInID },
                    { "@SearchInTag", SearchInTag },
                    { "@SearchInTitle", SearchInTitle },
                    { "@SearchInContent", SearchInContent }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GET_TECH_TIPS,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<TechTips>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<TopicsByCategory>> GetTopicsByCategoryID(int CategoryID)
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
                    QueryText = StoredProcedures.GET_TOPICSByCategory,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<TopicsByCategory>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }

        }

        public Task<IEnumerable<Topics>> GetTopicsBySubCategoryID(int SubCategoryID)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@SubCategoryID", SubCategoryID }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GET_TOPICS,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<Topics>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
