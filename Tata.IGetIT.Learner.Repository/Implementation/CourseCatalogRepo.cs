using System.Data;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class CourseCatalogRepo : ICourseCatalogRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public CourseCatalogRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        //public Task<IEnumerable<CatalogCourses>> FilterCourses(int TopicID, int CatagoryID, int SubCategoryID, int SkillLevelID, int Rating, string SearchText)
        public Task<IEnumerable<CatalogCourses>> FilterCourses(CatalogCoursesInputs catalogCoursesInputs)
        
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@TopicID", catalogCoursesInputs.TopicID },
                    { "@CatagoryID", catalogCoursesInputs.CatagoryID },
                    { "@SubCategoryID", catalogCoursesInputs.SubCategoryID },
                    { "@SkillLevelID", catalogCoursesInputs.SkillLevelID },
                    { "@Rating", catalogCoursesInputs.Rating },
                    { "@SearchText", catalogCoursesInputs.SearchText }
                };
                QueryInfo queryInfo = new()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GET_CATALOG_COURSES,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<CatalogCourses>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<Assessments>> GetAssessments(int CategoryID, int SubcategoryID, string SearchText)
        {
            try
            {

                var param = new Dictionary<string, object>
                {
                    { "@CategoryID", CategoryID },
                    { "@SubcategoryID", SubcategoryID },
                    { "@SearchText", SearchText },
                };
                QueryInfo queryInfo = new()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GET_ASSESSMENTS,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<Assessments>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CommonCourseCatalogParent> GetCommonCourseCatalogDetails(int CourseID)
        {
            var conn = _databaseOperations.CreateConnection();
            try
            {
                var queryParameters = new Dictionary<string, object>
                {
                    { "@CourseID", CourseID }
                };

                QueryInfo queryInfo = new()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GET_COMMON_COURSE_PROPERTIES,
                    Parameters = queryParameters
                };
                var results = await _databaseOperations.GetMultipleResultAsync(conn, queryInfo);
                
                if (results != null)
                {
                    try
                    {
                        var commonCourseCatalogResult = results.Read<CommonCourseCatalog>().FirstOrDefault();
                        var CommonCourseCatalogTOCResult = results.Read<CommonCourseCatalogTOC>().ToList();
                        CommonCourseCatalogParent commonCourseCatalogParent = new()
                        {
                            CommonCourseCatalog = (CommonCourseCatalog)commonCourseCatalogResult,
                            CommonCourseCatalogTOC = CommonCourseCatalogTOCResult,
                        };

                        return commonCourseCatalogParent;
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        public Task<IEnumerable<CourseCatalog>> GetCourseProperties(int UserID, int CourseID)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", UserID },
                    { "@CourseID", CourseID }
                };
                QueryInfo queryInfo = new()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GET_ALL_COURSE_PROPERTIES,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<CourseCatalog>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<CourseTableOfContent>> GetCourseTableOfContents(int UserID, int CourseID, float Percentage)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", UserID },
                    { "@CourseID", CourseID },
                    { "@Percentage", Percentage }
                };
                QueryInfo queryInfo = new()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GET_COURSE_Table_OF_CONTENTS,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<CourseTableOfContent>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<MasterCourseCategories>> GetMasterCourseCategories()
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                };
                QueryInfo queryInfo = new()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DASHBOARD_GETCATALOG,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<MasterCourseCategories>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }


    }
}
