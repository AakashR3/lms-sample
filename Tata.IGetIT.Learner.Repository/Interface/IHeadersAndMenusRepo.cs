using Dapper;
using Tata.IGetIT.Learner.Repository.Models;
using static Dapper.SqlMapper;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface IHeadersAndMenusRepo
    {
        public Task<IEnumerable<HeadersAndMenu_UserMenu>> MenuItems(int UserID);
        public Task<HeadersAndMenu_UserPoints> Points(int UserID);
        public Task<int> UserCartCount(int UserID);
        public Task<TrialUserDetails> CheckTrialUser(int UserID);
        public Task<IEnumerable<HeadersAndMenu_UserNotifications>> UserNotification(int UserID);
        public Task<NotificationsCourseReleases> AllUserNotification(int UserID);
        public Task<IEnumerable<HeadersAndMenu_MoreActions_Subscription_Child>> GetSubscriptions(int UserID,string Currency);
        public Task<IEnumerable<HeadersAndMenu_MoreActions_Favorites>> GetFavorites(int UserID);
    }
}
