using static Dapper.SqlMapper;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface IHeadersAndMenusService
    {
        public Task<IEnumerable<HeadersAndMenu_UserMenu>> MenuItems(int UserID, List<string> ErrorsMessages);
        public Task<HeadersAndMenu_UserPoints> Points(int UserID, List<string> ErrorsMessages);
        public Task<int> UserCartCount(int UserID, List<string> ErrorsMessages);
        public Task<TrialUserDetails> CheckTrialUser(int UserID, List<string> ErrorsMessages);
        public Task<HeadersAndMenu_UserNotificationDetails> UserNotification(int UserID, List<string> ErrorsMessages);
        public Task<NotificationsCourseReleases> AllUserNotification(int UserID);
        public Task<HeadersAndMenu_MoreActions_Subscription_Parent> GetSubscriptions(int UserID,string Currency, List<string> ErrorsMessages);
        public Task<IEnumerable<HeadersAndMenu_MoreActions_Favorites>> GetFavorites(int UserID, List<string> ErrorsMessages);
    }
}
