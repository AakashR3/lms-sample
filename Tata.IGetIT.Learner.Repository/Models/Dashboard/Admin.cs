namespace Tata.IGetIT.Learner.Repository.Models
{
    public class TopAssessmentParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<TopAssessments> topAssessments { get; set; }
    }
    public class TopAssessments
    {
        public int AssessmentID { get; set; }
        public string Title { get; set; }
        public int TimesTaken { get; set; }
    }

    public class TopCourseParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<TopCourses> topCourses { get; set; }
    }
    public class TopCourses
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Launches { get; set; }
    }
    public class Topusers
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float TotalTime { get; set; }
        public string ShortName { get; set; }
    }
    public class LoginMonths
    {
        public int LoginMonth { get; set; }
        public int LoginYear { get; set; }
        public int LoginNum { get; set; }
    }
    public class AdminDashboardDetailParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public UserScoreCards userScoreCards { get; set; }
        public IEnumerable<CurrentSubscription> currentSubscriptions { get; set; }        
    }
    public class UserScoreCards
    {
        public int ActiveUsers { get; set; }
        public int InactiveUsers { get; set; }
        public int ActiveSubscription { get; set; }
        public int InactiveSubscription { get; set; }
    }
    public class CurrentSubscriptionParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<CurrentSubscription> currentSubscriptions { get; set; }
    }
    public class CurrentSubscription
    {
        public int FulfillmentID { get; set; }
        public string BundleName { get; set; }
        public int TotalQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public string ExpireDate { get; set; }
        public int IsExpired { get; set; }
    }
    public class DownloadUserReport
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserStatus { get; set; }
        public string UserType { get; set; }
        public int AccountID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public int EmployeeType { get; set; }
        public string CompanyName { get; set; }
        public string Industry { get; set; }
        public string CustomerType { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Platform { get; set; }
        public string AgreeToTerms { get; set; }
        public string SecondTermsAcceptance { get; set; }
        public string ShipAddress1 { get; set; }
        public string ShipAddress2 { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public string ConnectTime { get; set; }
        public string DisconnectTime { get; set; }
        public float TTime { get; set; }
        public int TotalLogins { get; set; }

    }
}
 		