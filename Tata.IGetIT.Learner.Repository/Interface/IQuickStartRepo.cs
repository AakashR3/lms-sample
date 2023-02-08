using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface IQuickStartRepo
    {        
        public Task<IEnumerable<QuickStartGridData>> GetQuickStartGridData(QuickStartGrid quickStartGrid);
        public Task<IEnumerable<Categories>> GetCategories(QuickStartGrid quickStartGrid);
        public Task<IEnumerable<SubCategories>> GetSubCategories(QuickStartGrid quickStartGrid);
        public Task<int> GetNotificationFlag(QuickStartNotification quickStartNotification);
        public Task<int> UpdateNotificationFlag(QuickStartNotification quickStartNotification);
    }
}
