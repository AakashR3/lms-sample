using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class CommonCourseCatalogParent
    {
        public CommonCourseCatalog CommonCourseCatalog { get; set; }
        public IEnumerable<CommonCourseCatalogTOC> CommonCourseCatalogTOC { get; set; }
    }
    public class CommonCourseCatalog
    {
        public string Title { get; set; }
        public int CourseID { get; set; }
        public string InitialGraphic { get; set; }
        public string OnlineHours { get; set; }
        public string Overview { get; set; }
        public string Prerequisites { get; set; }
        public string IntendedAudience { get; set; }
        public string SkillLevel { get; set; }
        public string AverageRating { get; set; }
        public string TotalLessons { get; set; }

    }
    public class CommonCourseCatalogTOC
    {
        public int Page { get; set; }
        public int DocID { get; set; }
        public int DocSequence { get; set; }
        public string DocTitle { get; set; }
        public int UnitID { get; set; }
        public int UnitSeq { get; set; }
        public string InitialGraphic { get; set; }
    }
    public class CourseCatalog
    {
        public string Title { get; set; }
        public int CourseID { get; set; }
        public string CreationDate { get; set; }
        public string InitialGraphic { get; set; }
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public int SubCategoryID { get; set; }
        public string OnlineHours { get; set; }
        public string AssmTitle { get; set; }
        public int AssessmentID { get; set; }
        public string Overview { get; set; }
        public string Prerequisites { get; set; }
        public string IntendedAudience { get; set; }
        public string FavoriteNum { get; set; }
        public string Filename { get; set; }
        public string CourseTime { get; set; }
        public string AccessDate { get; set; }
        public string Access { get; set; }
        public string SkillLevel { get; set; }
        public int SkillID { get; set; }
        public int StatusID { get; set; }
        public string Tags { get; set; }
        public string Custom { get; set; }
        public int AccountID { get; set; }
        public string BlobLocation { get; set; }
        public string IsAicc { get; set; }
        public int TotalPoint { get; set; }
        public int CurrentPoint { get; set; }
        public string Email { get; set; }
        public string LinkedInPublish { get; set; }
        public string AverageRating { get; set; }
        public int Enrolled { get; set; }
        public string SubscriptionAction { get; set; }
        public float LearningProgress { get; set; }
        public string tTime { get; set; }
        public int LessonsTotal { get; set; }
        public int LessonsCompleted { get; set; }
        public string RecommendedCourses { get; set; }

    }

    public class MasterCourseCategories
    {
        public int CatalogCategoryID { get; set; }
        public string CatalogCategoryName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImageFileName { get; set; }
        public int CourseCount { get; set; }
    }
    public class SubCourseCategories
    {
        public int ItemID { get; set; }
        public string Title { get; set; }
        public int New { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int Access { get; set; }
        public int SubCategoryID { get; set; }
        public string Name { get; set; }
        public int InitialGraphic { get; set; }
        public int FavNum { get; set; }
        public string progress { get; set; }
        public string Type { get; set; }
    }
    public class CatalogCoursesInputs
    {
        public int TopicID { get; set; }
        public int CatagoryID { get; set; }
        public int SubCategoryID { get; set; }
        public int SkillLevelID { get; set; }
        public int Rating { get; set; }
        public string SearchText { get; set; }
    }
        public class CatalogCoursesParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<CatalogCourses> catalogCourses { get; set; }
    }
    public class CatalogCourses
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string InitialGraphic { get; set; }
        public string CreationDate { get; set; }
        public string ReleaseDate { get; set; }
        public int AccountID { get; set; }
        public int StatusID { get; set; }
        public int Custom { get; set; }
        public int SubCategoryID { get; set; }
        public int OnlineHours { get; set; }
        public int FeaturedCourse { get; set; }
        public string IntendedAudience { get; set; }
        public string Prerequisites { get; set; }
        public string Overview { get; set; }
        public int ContentType { get; set; }
        public int Mobileready { get; set; }
        public int SkillID { get; set; }
        public int CategoryID { get; set; }
        public string SkillLevel { get; set; }
        public float AverageRating { get; set; }
        public int TotalLessons { get; set; }
        public int CourseTopicID { get; set; }
        public string TopicName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }

    }

    public class AssessmentsParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<Assessments> Assessments { get; set; }
    }
    public class Assessments
    {
        public int AssessmentID { get; set; }
        public string Title { get; set; }
        public int TimeLimitMinutes { get; set; }
        public int NumberOfQuestions { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName  { get; set; }
        public string InitialGraphic { get; set; }
        
        /*
        public int ItemID { get; set; }
        public string Title { get; set; }
        public int New { get; set; }
        public string AssessmentType { get; set; }
        public int NumberOfQuestions { get; set; }
        public int Access { get; set; }
        public int MinPassGrade { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryID { get; set; }
        public string Name { get; set; }
        public string FavNum { get; set; }
        public string Type { get; set; }
        public string AssmImage { get; set; }
        public string Progress { get; set; }
        public string Overview { get; set; }
        public int TimeLimitMinutes { get; set; }
        */
    }
    public class CourseTableOfContent
    {
        public int DocID { get; set; }
        public int DocSequence { get; set; }
        public string DocTitle { get; set; }
        public int UnitID { get; set; }
        public int UnitSeq { get; set; }
        public string UnitTitle { get; set; }
        public int Page { get; set; }
        public int Type { get; set; }
        public int Template { get; set; }
        public int BookmarkID { get; set; }
        public int TrackID { get; set; }
        public int ShowPage { get; set; }
        public string InitialGraphic { get; set; }
        
    }
}
