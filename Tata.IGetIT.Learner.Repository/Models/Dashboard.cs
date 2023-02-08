namespace Tata.IGetIT.Learner.Repository.Models
{
    public class ScoreCard
    {
        public int UserID { get; set; }
    }

    public class LearningPath
    {
        public int UserID { get; set; }
        public int MasterCategoryID { get; set; }
        public int CategoryID { get; set; }
        public int SubcategoryID { get; set; }
        public int PathID { get; set; }
        public int Mode { get; set; }
    }

    public class Dashboard
    {
        public int NumCourses { get; set; }
        public int InProgressCourses { get; set; }
        public int CompletedCourses { get; set; }
        public int NumAssms { get; set; }
        public string TrainingTime { get; set; }
    }

    public class CourseList
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string InitialGraphic { get; set; }
        public int TotalLessons { get; set; }
        public string OnlineHours { get; set; }
        public int Enrolled { get; set; }
        public int Rating { get; set; }
    }

    public class CourseListInProgress
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string tTime { get; set; }
        public int Progress { get; set; }
        public int EventID { get; set; }
        public int LessonsCompleted { get; set; }
        public int LessonsTotal { get; set; }
    }

    public class CategoryList
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }

    }

    public class CatalogListParent
    {
        public int CatalogCategoryID { get; set; }
        public string CatalogCategoryName { get; set; }
        public IEnumerable<CatalogListChild> Items { get; set; }
    }
    public class CatalogListChild
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImageFileName { get; set; }
        public int CourseCount { get; set; }
    }
    public class CatalogList
    {
        public int CatalogCategoryID { get; set; }
        public string CatalogCategoryName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImageFileName { get; set; }
        public int CourseCount { get; set; }
    }

    public class Subscription
    {
        public int UserID { get; set; }
        public string CurrencyCode { get; set; }
    }

    public class SubscriptionList
    {
        public int SubscriptionID { get; set; }
        public string SubscriptionName { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int CourseCount { get; set; }
        public int CategoryID { get; set; }
    }

    public class LearningPathList
    {
        public int SNo { get; set; }
        public int PathID { get; set; }
        public string LearningPathName { get; set; }
        public string LPDuration { get; set; }
        public int CourseCount { get; set; }
        public int CourseSNo { get; set; }
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int LessonsCompleted { get; set; }
        public int LessonsTotal { get; set; }
        public string Duration { get; set; }
        public string Progress { get; set; }
        public int EventID { get; set; }
        public string LastAccess { get; set; }
        public string StartDate { get; set; }
        public string DueDate { get; set; }
        public int Favorite {get; set;}
        public string Type { get; set; }
    }

    public class LearningPathDetails
    {
        public int CourseSNo { get; set; }
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int LessonsCompleted { get; set; }
        public int LessonsTotal { get; set; }
        public string Duration { get; set; }
        public int Progress { get; set; }
        public int EventID { get; set; }        
    }

    public class LearningPathDetailedList
    {
        public int SNo { get; set; }
        public int PathID { get; set; }
        public string LearningPathName { get; set; }
        public string LPDuration { get; set; }
        public int CourseCount { get; set; }        
        public IEnumerable<LearningPathCourseDetails> LearningPathCourseDetails { get; set; }
    }

    public class LearningPathCourseDetails
    {
        public int CourseSNo { get; set; }
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int LessonsCompleted { get; set; }
        public int LessonsTotal { get; set; }
        public string Duration { get; set; }
        public string Progress { get; set; }
        public int EventID { get; set; }
        public string LastAccess { get; set; }
        public string StartDate { get; set; }
        public string DueDate { get; set; }
        public string TimeSpent { get; set; }
        public int Favorite {get; set;}
        public string Type { get; set; }
    }

    public class UpcomingEvents
    {
        public int UserID;
        public string EventPeriod; // T - Today, W - Week, M - Month
    }

    public class UpcomingEventsList
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public string Action { get; set; }
    }

    public class ProfileData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int ProfileCompletionPercentage { get; set; }
        public int TotalPoints { get; set; }
    }

    public class TranscriptList
    {
        public int CoursePoints { get; set; }
        public int AssessmentPoints { get; set; }
    }

    public class PopularRolesList
    {
        public int TotalCount { get; set; }
        public int TotalPercentage { get; set; }
        public string PopularRoleName { get; set; }
    }

    public class TimeSpentMetrics
    {
        public int PerformanceLevel { get; set; }
        public int CompletedCourses { get; set; }
        public int CompletedAssessments { get; set; }
        public string TimeSpent { get; set; }
        public decimal TotalCoursePercentage { get; set; }
        public decimal TotalTimePercentage { get; set; }
        public decimal TotalAssessmentPercentage { get; set; }
        public decimal TotalPerformanceLevelPercentage { get; set; }
    }

    public class TimeSpentGraph
    {
        public string WeekDaysName { get; set; }
        public string WeekDayData { get; set; }
    }
    public class PopularRoleGraph
    {
        public string PopularRoleName { get; set; }
        public string Month { get; set; }
        public string Count { get; set; }
    }
    
    public class HeroSectionDetails
    {
        public HeroSectionNavigation heroSectionNavigation { get; set; }
        public IEnumerable<HeroSectionLearningPathList> heroSectionLearningPathList { get; set; }
    }

    public class HeroSectionNavigation
    {
        public string Navigation { get; set; }
        public string CurrentRole { get; set; }
        public string TargetRole { get; set; }        
    }

    public class HeroSectionLearningPathList
    {
        public int PathID { get; set; }
        public string LearningPathName { get; set; }
        public string PendingTimeDuration { get; set; }
        public int CourseCompleted { get; set; }
        public int TotalCourse { get; set; }
        public int AssessmentCompleted { get; set; }
        public int PathsCompleted { get; set; }
        public string TimeToSpend { get; set; }
    }
}