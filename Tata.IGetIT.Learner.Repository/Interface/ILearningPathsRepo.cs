using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface ILearningPathsRepo
    {
        public Task<IEnumerable<SubscriptionLearningPath>> GetLearningPathsByManagerAsync(int UserID);
    }
}
