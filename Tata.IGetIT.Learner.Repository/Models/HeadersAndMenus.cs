using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Tata.IGetIT.Learner.Repository.Models
{

    public class NotificationsCourseReleases
    {
        public IEnumerable<ReleaseDetails> ReleaseDetails { get; set; }
        public HeadersAndMenu_UserNotificationDetails headersAndMenu_UserNotificationDetails { get; set; }

    }
    public class TrialUserDetails
    {
        public int Result { get; set; }
        public int RemainingDays { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

    }
    public class ReleaseDetails
    {
        public int ReleaseNotificationID { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
       
    public class HeadersAndMenu_MoreActions_Subscription_Parent
    {
        public int UserID { get; set; }
        public int AccountID { get; set; }
        public int ActiveCount { get; set; }
        public int ExpiredCount { get; set; }
        public IEnumerable<HeadersAndMenu_MoreActions_Subscription_Child> ExpiredSubscriptions { get; set; }
        public IEnumerable<HeadersAndMenu_MoreActions_Subscription_Child> ActiveSubscriptions { get; set; }
    }
    public class HeadersAndMenu_MoreActions_Subscription_Child
    {
        public int? UserID { get; set; }
        public int? AccountID { get; set; }
        public int SubscriptionID { get; set; }
        public int FulfillmentID { get; set; }
        public string SalesOrderDetailID { get; set; }
        public DateTime ExpireDate { get; set; }
        public string SubscriptionName { get; set; }
        public int? CourseCount { get; set; }
        public int? AssessmentCount { get; set; }
        public string Status { get; set; }
        public float Price { get; set; }
        public string Image { get; set; }
        public string Currency { get; set; }
        public string InvoiceType { get; set; }

    }
    public class HeadersAndMenu_MoreActions_Favorites
    {
        public int ItemID { get; set; }
        public string Title { get; set; }
        public DateTime LastAccess { get; set; }
        public string Progress { get; set; }
        public string SubCategory { get; set; }
        public string Type { get; set; }
        public int Submit { get; set; }
        public int MinPassGrade { get; set; }
        public int FavNum { get; set; }
        public string InitialGraphic { get; set; }
    }
    public class HeadersAndMenu_UserNotificationDetails
    {
        public string NotificationsCount { get; set; }
        public IEnumerable<HeadersAndMenu_UserNotifications> Notifications { get; set; }
    }
    public class HeadersAndMenu_UserNotifications
    {
        public int NotificationID { get; set; }
        public string Message { get; set; }
        public string Link { get; set; }
        public string NotificationCount { get; set; }
    }
    public class HeadersAndMenu_UserCartCount
    {
        public int CartCount { get; set; }
    }
    public class HeadersAndMenu_UserMenu
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
    }
    public class HeadersAndMenu_UserPoints
    {
        public int Point { get; set; }
    }
}
