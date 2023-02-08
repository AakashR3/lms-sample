
namespace Tata.IGetIT.Learner.Service.Implementation
{
    /// <summary>
    /// Learning paths service
    /// </summary>
    public class LearningPathsService : ILearningPathsService
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly ILearningPathsRepo _learningPathsRepo;

        /// <summary>
        /// Learning paths service constructor
        /// </summary>
        /// <param name="learningPathsRepo">learningPathsRepo</param>
        /// <exception cref="ArgumentNullException"></exception>
        public LearningPathsService(ILearningPathsRepo learningPathsRepo)
        {
            _learningPathsRepo = learningPathsRepo ?? throw new ArgumentNullException(nameof(ILearningPathsRepo));
        }

        /// <summary>
        /// Get learning paths by manager
        /// </summary>
        /// <param name="UserID">User Id</param>
        /// <returns></returns>
        public async Task<IEnumerable<SubscriptionLearningPath>> GetLearningPathsByManagerAsync(int UserID)
        {
            return await _learningPathsRepo.GetLearningPathsByManagerAsync(UserID);
        }
    }
}
