using Tata.IGetIT.Learner.Repository.Models;
namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public  interface ILearningPathsService
    {
        public Task<IEnumerable<SubscriptionLearningPath>> GetLearningPathsByManagerAsync(int UserID);
    }
}
