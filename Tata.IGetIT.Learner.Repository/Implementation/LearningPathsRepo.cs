using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class LearningPathsRepo : ILearningPathsRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public LearningPathsRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public async Task<IEnumerable<SubscriptionLearningPath>> GetLearningPathsByManagerAsync(int UserID)
        {

            var param = new Dictionary<string, object>
            {
                { "@UserID", UserID }
            };
            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GET_LEARNING_PATHS_BY_MANAGER,
                Parameters = param
            };

            return await _databaseOperations.GetMultipleRecords<SubscriptionLearningPath>(queryInfo);

        }

    }
}
