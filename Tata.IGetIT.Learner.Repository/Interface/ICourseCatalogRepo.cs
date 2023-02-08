using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface ICourseCatalogRepo
    {
        #region Course

        public Task<IEnumerable<MasterCourseCategories>> GetMasterCourseCategories();
        public Task<IEnumerable<CourseCatalog>> GetCourseProperties(int UserID, int CourseID);
        public Task<CommonCourseCatalogParent> GetCommonCourseCatalogDetails(int CourseIDs);

        public Task<IEnumerable<CourseTableOfContent>> GetCourseTableOfContents(int UserID, int CourseID, float Percentage);
        //public Task<IEnumerable<CatalogCourses>> FilterCourses(int TopicID, int CatagoryID, int SubCategoryID, int SkillLevelID, int Rating, string SearchText);
        public Task<IEnumerable<CatalogCourses>> FilterCourses(CatalogCoursesInputs catalogCoursesInputs);



        #endregion

        #region Assessment

        public Task<IEnumerable<Assessments>> GetAssessments(int CategoryID, int SubcategoryID, string SearchText);
        #endregion
    }
}
