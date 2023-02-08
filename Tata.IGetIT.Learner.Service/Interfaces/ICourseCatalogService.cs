namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface ICourseCatalogService
    {
        #region Course
        public Task<IEnumerable<MasterCourseCategories>> GetMasterCourseCategories(List<string> ErrorsMessages);
        public Task<IEnumerable<CourseCatalog>> GetCourseProperties(int UserID, int CourseID, List<string> ErrorsMessages);
        public Task<CommonCourseCatalogParent> GetCommonCourseCatalogDetails(int CourseID, List<string> ErrorsMessages);
        //public Task<IEnumerable<CatalogCourses>> FilterCourses(int TopicID, int CatagoryID, int SubCategoryID, int SkillLevelID, int Rating, string SearchText, List<string> ErrorsMessages);
        public Task<IEnumerable<CatalogCourses>> FilterCourses(CatalogCoursesInputs catalogCoursesInputs, List<string> ErrorsMessages);
        public Task<IEnumerable<CourseTableOfContent>> GetCourseTableOfContents(int UserID, int CourseID, float Percentage, List<string> ErrorsMessages);
        #endregion

        #region Assessment
        public Task<IEnumerable<Assessments>> GetAssessments(int CategoryID, int SubcategoryID, string SearchText, List<string> ErrorsMessages);
        #endregion
    }
}
