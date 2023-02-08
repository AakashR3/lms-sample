using MailKit.Search;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Service.Interfaces;

namespace Tata.IGetIT.Learner.Service.Implementation
{
    public class CourseCatalogService : ICourseCatalogService
    {
        private readonly ICourseCatalogRepo _courseCatalogRepo;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public CourseCatalogService(ICourseCatalogRepo courseCatalogRepo)
        {
            if (courseCatalogRepo == null)
            {
                new ArgumentNullException("courseCatalogRepo cannot be null");
            }
            _courseCatalogRepo = courseCatalogRepo;
        }

        public async Task<IEnumerable<CourseCatalog>> GetCourseProperties(int UserID, int CourseID, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _courseCatalogRepo.GetCourseProperties(UserID, CourseID);

                if (!result.Any())
                    ErrorsMessages.Add(LearnerAppConstants.NO_COURSES_FOUND);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<MasterCourseCategories>> GetMasterCourseCategories(List<string> ErrorsMessages)
        {
            try
            {
                var result = await _courseCatalogRepo.GetMasterCourseCategories();

                if (!result.Any())
                    ErrorsMessages.Add(LearnerAppConstants.NoRecordsFound);
                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<CatalogCourses>> FilterCourses(CatalogCoursesInputs catalogCoursesInputs, List<string> ErrorsMessages)

        //public async Task<IEnumerable<CatalogCourses>> FilterCourses(int TopicID, int CatagoryID, int SubCategoryID, int SkillLevelID, int Rating, string SearchText, List<string> ErrorsMessages)
        {
            try
            {
                //var result = await _courseCatalogRepo.FilterCourses(TopicID, CatagoryID, SubCategoryID, SkillLevelID, Rating, SearchText);
                var result = await _courseCatalogRepo.FilterCourses(catalogCoursesInputs);

                if (!result.Any())
                    ErrorsMessages.Add(LearnerAppConstants.NoRecordsFound);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Assessments>> GetAssessments(int CategoryID, int SubcategoryID, string SearchText, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _courseCatalogRepo.GetAssessments(CategoryID, SubcategoryID, SearchText);

                if (!result.Any())
                    ErrorsMessages.Add(LearnerAppConstants.NoRecordsFound);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<CourseTableOfContent>> GetCourseTableOfContents(int UserID, int CourseID, float Percentage, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _courseCatalogRepo.GetCourseTableOfContents(UserID, CourseID, Percentage);

                if (!result.Any())
                    ErrorsMessages.Add(LearnerAppConstants.NoRecordsFound);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CommonCourseCatalogParent> GetCommonCourseCatalogDetails(int CourseID, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _courseCatalogRepo.GetCommonCourseCatalogDetails(CourseID);

                if (result == null)
                    ErrorsMessages.Add(LearnerAppConstants.NoRecordsFound);
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
