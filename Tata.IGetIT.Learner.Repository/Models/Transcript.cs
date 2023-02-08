using System;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class TranscriptUserPublicURL
    {
        public TranscriptUserDetails transcriptUserDetails { get; set; }
        public TranscriptUserProfile transcriptUserProfile { get; set; }
        public IEnumerable<TranscriptUserCertificates> transcriptUserCertificates { get; set; }
    }
    public class TranscriptUserProfile
    {
        public string Biography { get; set; }
        public string SocialLinkedIn { get; set; }
        public string SocialGmail { get; set; }
        public string SocialFacebook { get; set; }
    }
    public class TranscriptUserCertificates
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int EventID { get; set; }
        public DateTime CompledDate { get; set; }
        public string InitialGraphic { get; set; }
    }
    public class TranscriptUserDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EmailID { get; set; }
        public string UserType { get; set; }
        public string LearningTime { get; set; }
        public int TotalPoints { get; set; }
        public int CoursePoints { get; set; }
        public int AssessmentPoints { get; set; }
        public int UserID { get; set; }
    }
    public class TranscriptCourseHistory
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public DateTime LastLaunchDate { get; set; }
        public string Progress { get; set; }
        public string TTime { get; set; }
        public int EventID { get; set; }
        public int EndPoint { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public byte[] CategoryImage { get; set; }
        public string CategoryImageFileName { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public byte[] SubCategoryImage { get; set; }
        public string SubCategoryImageFileName { get; set; }
        public int TotalPoint { get; set; }
        public string PointsEarned { get; set; }
        public int LinkedInPublish { get; set; }
    }
    public class TranscriptCourseHistoryChild
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public DateTime LastLaunchDate { get; set; }
        public string Progress { get; set; }
        public string TTime { get; set; }
        public int EventID { get; set; }
        public int EndPoint { get; set; }
        //public byte[] CategoryImage { get; set; }
        //public byte[] SubCategoryImage { get; set; }
        public int TotalPoint { get; set; }
        public string PointsEarned { get; set; }
        public int LinkedInPublish { get; set; }
    }

    public class TranscriptCourseHistoryParentGrid
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<TranscriptCourseHistoryParent> transcriptCourseHistoryParents { get; set; }
    }
    public class TranscriptCourseHistoryParent
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImageFileName { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryImageFileName { get; set; }
        public int SubTotalEndPoints { get; set; }
        public int SubTotal_TotalPoints { get; set; }
        public IEnumerable<TranscriptCourseHistoryChild> TranscriptCourseHistoryChildren { get; set; }
    }

    public class TranscriptAssessmmentHistory
    {
        public int AssessmentID { get; set; }
        public string Title { get; set; }
        public DateTime LastLaunchDate { get; set; }
        public int Progress { get; set; }
        public string tTime { get; set; }
        public int EventID { get; set; }
        public int MinPassGrade { get; set; }
        public int TimesTaken { get; set; }
        public int EndPoint { get; set; }
        public int TotalPoint { get; set; }
        public int BestScore { get; set; }
        public string PointsEarned { get; set; }
        public int LinkedInPublish { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public byte[] CategoryImage { get; set; }
        public string CategoryImageFileName { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public byte[] SubCategoryImage { get; set; }
        public string SubCategoryImageFileName { get; set; }
        public string Overview { get; set; }
        public int TimeLimitMinutes { get; set; }
        public int NumberOfQuestions { get; set; }
        public string AssessmentType { get; set; }
    }

    public class TranscriptAssessmmentHistoryParentGrid
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<TranscriptAssessmmentHistoryParent> transcriptAssessmmentHistoryParents { get; set; }
    }
    public class TranscriptAssessmmentHistoryParent
    {

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImageFileName { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryImageFileName { get; set; }
        public int SubTotalEndPoints { get; set; }
        public int SubTotal_TotalPoints { get; set; }
        public IEnumerable<TranscriptAssessmmentHistoryChild> TranscriptAssessmmentHistoryChildren { get; set; }

    }
    public class TranscriptAssessmmentHistoryChild
    {
        public int AssessmentID { get; set; }
        public string Title { get; set; }
        public DateTime LastLaunchDate { get; set; }
        public int Progress { get; set; }
        public string tTime { get; set; }
        public int EventID { get; set; }
        public int MinPassGrade { get; set; }
        public int TimesTaken { get; set; }
        public int BestScore { get; set; }
        public int EndPoint { get; set; }
        public int TotalPoint { get; set; }
        public string PointsEarned { get; set; }
        public int LinkedInPublish { get; set; }
        public string Overview { get; set; }
        public int TimeLimitMinutes { get; set; }
        public int NumberOfQuestions { get; set; }
        public string AssessmentType { get; set; }
    }

    public class AssessmentProperties
    {
        public int AssessmentID { get; set; }
        public string Title { get; set; }
        public int NumberOfQuestions { get; set; }
        public string AssessmentType { get; set; }
        public string Overview { get; set; }
        public int TimeLimitMinutes { get; set; }
        public string LastLaunchDate { get; set; }
        public string Progress { get; set; }
        public int tTime { get; set; }
        public int EventID { get; set; }
        public int MinPassGrade { get; set; }
        public int TimesTaken { get; set; }
        public int BestScore { get; set; }
        public int EndPoint { get; set; }
        public int TotalPoint { get; set; }
        public string PointsEarned { get; set; }
        public int LinkedInPublish { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        //public byte[] CategoryImage { get; set; }
        public string CategoryImageFileName { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        //public byte[] SubCategoryImage { get; set; }
        public string SubCategoryImageFileName { get; set; }
        public int FavoriteNum { get; set; }
        public string AssessmentAction { get; set; }


    }
}
