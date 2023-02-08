namespace Tata.IGetIT.Learner.Repository.Models
{
    public class Notification
    {
        public int AccountID { get; set; }
        public int UserID { get; set; }
        public int NotificationTypeId { get; set; }
    }

    public class NotificationData
    {        
        public IEnumerable<UserNotificationDetails> UserNotificationDetails { get; set; }
        public IEnumerable<UsersPendingProfileCompletion> UsersPendingProfileCompletion { get; set; }
        public IEnumerable<UsersDashboardMetrics> UsersDashboardMetrics { get; set; }
    }

    public class UsersPendingProfileCompletion
    {
        public int UserId { get; set; }
        public int ProfilePercentage { get; set; }
    }

    public class UsersDashboardMetrics
    {
        public int UserID { get; set; }
        public int InProgressCourses { get; set; }
        public int CompletedCourses { get; set; }
        public int NumAssms { get; set; }
        public string TimeSpent { get; set; }
    }

    public class UserNotificationDetails
    {
        public int UserID { get; set; }
        public int AccountID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}