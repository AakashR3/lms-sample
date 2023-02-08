using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface ILearningService
    {
        public Task<IEnumerable<UserHistoryGridData>> GetUserHistoryGridData(UserHistory UserHistory);
        public Task<IEnumerable<MyLearningGridData>> GetMyLearningGridData(MyLearning MyLearning);
        public Task<string> AddFavoriteItem(AddRemoveFavorite AddRemoveFavorite);
        public Task<string> RemoveFavoriteItem(AddRemoveFavorite AddRemoveFavorite);
        public Task<IEnumerable<LearningPathDetailedList>> GetMyLearningPath(MyLearning MyLearning);
        public Task<DownloadCertificateInfo> GetDownloadCertificate(DownloadCertificate downloadCertificate);
    }
}