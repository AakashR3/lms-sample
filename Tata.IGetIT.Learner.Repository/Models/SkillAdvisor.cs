using System.Numerics;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class SkillAdvisor_UserTypeRoles
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string URL { get; set; }
        public string RoleImage { get; set; }
    }
    public class SkillAdvisor_Categories
    {
        public int ID { get; set; }
        public string MasterCategoryName { get; set; }

    }
    public class SkillAdvisor_Softwares
    {
        public int CatalogCategoryID { get; set; }
        public string CatalogCategoryName { get; set; }
        public int Sequence { get; set; }

    }

    public class SkillAdvisor_Courses_Parent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<SkillAdvisor_Courses> skillAdvisor_Courses { get; set; }
    }

    public class SkillAdvisor_Courses
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int OnlineHours { get; set; }
        public int TotalLessons { get; set; }

    }
    public class SkillAdvisor_Assessments_Parent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<SkillAdvisor_Assessments> skillAdvisor_Assessments { get; set; }
    }

    public class SkillAdvisor_Assessments
    {
        public int AssessmentID { get; set; }
        public string Title { get; set; }
        public int NumberOfQuestions { get; set; }

    }
    public class SkillAdvisor_PersonalPlan
    {
        public int PlanID { get; set; }
        public string Name { get; set; }
        public string CreationDate { get; set; }
        public string RoleID { get; set; }
        public string Category { get; set; }
        public string ToolID { get; set; }
        public int SID_M { get; set; }
        public string SubscriptionID_Monthly { get; set; }
        public int SID_Q { get; set; }
        public string SubscriptionID_Quarterly { get; set; }
        public int SID_Y { get; set; }
        public string SubscriptionID_Yearly { get; set; }
        public string MonthlyAmount { get; set; }
        public string QuarterlyAmount { get; set; }
        public string YearlyAmount { get; set; }
        public int CourseCount { get; set; }
        public int AssessmentCount { get; set; }
        public int OnlineHours { get; set; }
        public string ImageLocation { get; set; }
    }
    public class SkillAdvisor_Category
    {
        public int ID { get; set; }
        public string MasterCategoryName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int Sequence { get; set; }
        public bool Active { get; set; }
        public byte[] CategoryImage { get; set; }
        public bool IsPublic { get; set; }
        public string CategoryImageFileName { get; set; }
    }
    public class SkillAdvisor_CategoryParent
    {
        public int ID { get; set; }
        public string MasterCategoryName { get; set; }
        public IEnumerable<SkillAdvisor_Category> Children { get; set; }
    }


}
