using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tata.IGetIT.Learner.Repository.Constants;
using static System.Net.WebRequestMethods;

namespace Tata.IGetIT.Learner.Repository.Models.AccountManagement
{
    public class AssignedLearningCatagories
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
    public class AssignedLearningSubCatagories
    {
        public int SubCategoryID { get; set; }
        public string Name { get; set; }
        public string CateName { get; set; }
        public string Version { get; set; }
    }
    public class UserListByManager
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int UserTypeID { get; set; }
        public string Role { get; set; }
        public string BusinessSite { get; set; }
        public string BusinessGroup { get; set; }
        public string BusinessManager { get; set; }
        public string ADID { get; set; }
        public string ADName { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipCountry { get; set; }
        public DateTime LogInDate { get; set; }
        public int TotalLogins { get; set; }
        public string UsageTime { get; set; }
        public string ShipAddress1 { get; set; }
        public string ShipAddress2 { get; set; }
        public string CompanyName { get; set; }
        public string BundleStatus { get; set; }
        public DateTime CreationDate { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
    }
    public class LearningPathActionsInput
    {
        public string Name { get; set; }
        public int UserID { get; set; }
        public int AccountID { get; set; }
        public string Action { get; set; }
        public int PathID { get; set; }
        public bool ForceOrder { get; set; }
        public string Description { get; set; }
        public int Subcategory { get; set; }
        public string Filters { get; set; }
        public int TypeID { get; set; }
    }

    public class RemoveUserFromLP
    {
        public int UserID { get; set; }
        public int PathID { get; set; }
    }
    public class RemoveGroupFromLP
    {
        public int GroupID { get; set; }
        public int PathID { get; set; }
    }
    public class AssignGroupToLP
    {
        public int GroupID { get; set; }
        public int PathID { get; set; }
    }
    public class LearningPlatformCourseAction
    {
        public string Action { get; set; }
        public int UserID { get; set; }
        public int ID { get; set; }
        public int SourceID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public string Link { get; set; }

    }
    public class LearningPathInput
    {
        public int UserID { get; set; }
        public string Action { get; set; }
        public int PathID { get; set; }
        public int ItemID { get; set; }
        public int ItemType { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? StartDate { get; set; }
        public int ItemSequence { get; set; }
    }
    public class LearningPathItemAction
    {
        public int PathID { get; set; }
        public string SubcatName { get; set; }
        public string Name { get; set; }
        public int CourseID { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string DueDate { get; set; }
        public string StartDate { get; set; }
        public int SubcategoryID { get; set; }
        public int MinPassGrade { get; set; }
        public int ItemSequence { get; set; }
    }
    public class LearningPathItems
    {
        public int PathID { get; set; }
        public string SubcatName { get; set; }
        public string Name { get; set; }
        public int CourseID { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string DueDate { get; set; }
        public string StartDate { get; set; }
        public int SubcategoryID { get; set; }
        public int MinPassGrade { get; set; }
        public int ItemSequence { get; set; }
    }
    public class SetDynamicGroupAttribute
    {
        public int PathID { get; set; }
        public int FieldID { get; set; }
        public string Filter { get; set; }
        public int ConditionID { get; set; }
        public int MainCondID { get; set; }
        public int AttrID { get; set; }
    }
    public class DynamicFieldOptions
    {
        public string Txt { get; set; }
        public string Val { get; set; }
    }
    public class UserListByAccountIDOrGroupIDParent
    {

        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<UserListByAccountIDOrGroupID> UserListByAccountIDOrGroupIDs { get; set; }
    }

    public class UserListByAccountIDOrGroupID
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserTypeID { get; set; }
        public string Role { get; set; }
        public string BusinessSite { get; set; }
        public string BusinessGroup { get; set; }
        public string BusinessManager { get; set; }
        public string ADID { get; set; }
        public string ADName { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipCountry { get; set; }
        public string LogInDate { get; set; }
        public int TotalLogins { get; set; }
        public decimal UsageTime { get; set; }
        public float CourseTime { get; set; }
        public float AssessmentTime { get; set; }
        public string LearningTime { get; set; }
        public string ShipAddress1 { get; set; }
        public string ShipAddress2 { get; set; }
        public string CompanyName { get; set; }
        public string BundleStatus { get; set; }
        public string CreationDate { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public string Attribute4 { get; set; }
        public string JobRole { get; set; }
        public string Region { get; set; }
    }
    public class LearningAssignments
    {
        public int PathID { get; set; }
        public string Name { get; set; }
        public int CoursesCount { get; set; }
        public int AssessmentsCount { get; set; }
        public int AccountID { get; set; }
        public string Description { get; set; }
        public int SubcategoryID { get; set; }
        public int TypeID { get; set; }

    }
    public class PreDefinedLearningPathsParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<PreDefinedLearningPaths> PreDefinedLearningPaths { get; set; }
    }
    public class PreDefinedLearningPaths
    {
        public int PathID { get; set; }
        public string Name { get; set; }
        public int CoursesCount { get; set; }
        public int AssessmentsCount { get; set; }
        public int AccountID { get; set; }
        public string Description { get; set; }
        public int SubcategoryID { get; set; }
        public int TypeID { get; set; }
    }

    public class CoursesForLearningPathParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<CoursesForLearningPath> CoursesForLearningPaths { get; set; }
    }
    public class CoursesForLearningPath
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Custom { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
    public class IntegrationsForLearningPathParent
    {

        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<IntegrationsForLearningPath> IntegrationsForLearningPath { get; set; }
    }
    public class IntegrationsForLearningPath
    {
        public int ID { get; set; }
        public int SourceID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public string Link { get; set; }

    }
    public class AssessmentsForLearningPath
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Custom { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
    public class AssessmentsForLearningPathParent
    {

        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<AssessmentsForLearningPath> AssessmentsForLearningPath { get; set; }
    }
    public class AssignedUsersForLearningPathParent
    {

        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<AssignedUsersForLearningPath> AssignedUsersForLearningPaths { get; set; }
    }

    public class AssignedUsersForLearningPath
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int UserTypeID { get; set; }
        public string Role { get; set; }
        public string BusinessSite { get; set; }
        public string BusinessGroup { get; set; }
        public string BusinessManager { get; set; }
        public string ADID { get; set; }
        public string ADName { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipCountry { get; set; }
        public DateTime LogInDate { get; set; }
        public int TotalLogins { get; set; }
        public float UsageTime { get; set; }
        public string ShipAddress1 { get; set; }
        public string ShipAddress2 { get; set; }
        public string CompanyName { get; set; }
        public string BundleStatus { get; set; }
        public DateTime CreationDate { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
    }
    public class GroupListByManagerParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<GroupListByManager> GroupListByManager { get; set; }

    }
    public class GroupListByManager
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int Type { get; set; }

    }
    public class ConditionalUsersParent
    {

        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<ConditionalUsers> ConditionalUsers { get; set; }
    }

    public class ConditionalUsers
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
    public class UnAssignedGroupByPathIDParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<UnAssignedGroupByPathID> UnAssignedGroupByPathID { get; set; }
    }
    public class UnAssignedGroupByPathID
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }

    }
    public class AssignedGroupByPathIDParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<AssignedGroupByPathID> AssignedGroupByPathID { get; set; }

    }
    public class AssignedGroupByPathID
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }

    }
    public class NotificationEmail
    {
        public string EmailMessage { get; set; }
        public string GroEmailSubjectupName { get; set; }
        public bool MessageWithItems { get; set; }
        public string ForceOrder { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
    public class SubcategoriesForLearningPath
    {
        public int SubCategoryID { get; set; }
        public string Name { get; set; }
        public string CateName { get; set; }
        public string Version { get; set; }

    }

    public class AssignedLearningInputs : IValidatableObject
    {
        public int UserID { get; set; }
        public int AccountID { get; set; }
        public int PathID { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (UserID.ToString().Length == 0 && PathID.ToString().Length == 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_ARGUMENTS));
            }
            return validationErrors;
        }

    }
}
