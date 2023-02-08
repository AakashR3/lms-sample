using MailKit.Search;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NLog.Filters;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CourseCatalogController : BaseController
    {
        private readonly ICourseCatalogService _courseCatalogService;
        private readonly ITranscriptService _transcriptService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public CourseCatalogController(ICourseCatalogService courseCatalogService, ITranscriptService transcriptService)
        {
            _transcriptService = transcriptService ?? throw new ArgumentNullException(nameof(ITranscriptService));
            _courseCatalogService = courseCatalogService ?? throw new ArgumentNullException(nameof(ICourseCatalogService));
        }
        #region GET

        #region Course
        /// <summary>
        /// To retrieve Master Course Categories
        /// </summary>
        /// <returns></returns>
        [HttpGet("MasterCourseCategories")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<MasterCourseCategories>))]
        [SwaggerResponse(400, "Bad Request", typeof(MasterCourseCategories))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<MasterCourseCategories>))]
        public async Task<IActionResult> MasterCourseCategories()
        {
            try
            {
                List<string> errorMessages = new();
                var result = await _courseCatalogService.GetMasterCourseCategories(errorMessages);
                if (errorMessages.Any())
                {
                    logger.LogDebug(errorMessages.FirstOrDefault());
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<MasterCourseCategories>() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<MasterCourseCategories>>() { Message = LearnerAppConstants.Success, Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<MasterCourseCategories>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /// <summary>
        /// To filter course categories
        /// </summary>
        /// <param name="TopicID"></param>
        /// <param name="CatagoryID"></param>
        /// <param name="SubCategoryID"></param>
        /// <param name="SkillLevelID"></param>
        /// <param name="Rating"></param>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        [HttpGet("FilterCourses/{TopicID}/{CatagoryID}/{SubCategoryID}/{SkillLevelID}/{Rating}/{SearchText}")]
        [SwaggerResponse(200, "Ok", typeof(CatalogCoursesParent))]
        [SwaggerResponse(400, "Bad Request", typeof(CatalogCoursesParent))]
        [SwaggerResponse(500, "Internal Server Error", typeof(CatalogCoursesParent))]
        public async Task<IActionResult> FilterCourses(int TopicID, int CatagoryID, int SubCategoryID, int SkillLevelID, int Rating, string SearchText = "-1", int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                CatalogCoursesInputs catalogCoursesInputs = new()
                {
                    TopicID=TopicID,
                    CatagoryID = CatagoryID,
                    SubCategoryID = SubCategoryID,
                    SkillLevelID = SkillLevelID,
                    Rating = Rating,
                    SearchText = SearchText

                };
                List<string> errorMessages = new();
                var result = await _courseCatalogService.FilterCourses(catalogCoursesInputs, errorMessages);
                //var result = await _courseCatalogService.FilterCourses(TopicID, CatagoryID, SubCategoryID, SkillLevelID, Rating, SearchText, errorMessages);

                string message = string.Empty;
                int totalRecords = result.Count();
                var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);

                CatalogCoursesParent catalogCoursesParent = new()
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    TotalItems = totalRecords,
                    TotalPages = validFilter.TotalPages
                };

                if (result.Any())
                {
                    if (result != null && result.Count() > 0)
                    {
                        result = result
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();
                        catalogCoursesParent.catalogCourses = result;
                        message = LearnerAppConstants.Success;
                    }
                    else
                        message = LearnerAppConstants.NoRecordsFound;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<CatalogCoursesParent>()
                    {
                        Message = message,
                        Data = catalogCoursesParent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<CatalogCoursesParent>() { Message = LearnerAppConstants.NoRecordsFound, Data = catalogCoursesParent });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ReturnResponse<CatalogCoursesParent>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// To get the user's subscriptions detail
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet("CourseProperties/{UserID}/{CourseID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<CourseCatalog>))]
        [SwaggerResponse(400, "Bad Request", typeof(CourseCatalog))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<CourseCatalog>))]
        public async Task<IActionResult> CourseProperties(int UserID, int CourseID)
        {
            try
            {
                List<string> errorMessages = new();
                var result = await _courseCatalogService.GetCourseProperties(UserID, CourseID, errorMessages);
                if (errorMessages.Any())
                {
                    logger.LogDebug(errorMessages.FirstOrDefault());
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<CourseCatalog>() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<CourseCatalog>>() { Message = LearnerAppConstants.Success, Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<CourseCatalog>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("AssessmentProperties/{UserID}/{AssessmentID}")]
        [SwaggerResponse(200, "Ok", typeof(AssessmentProperties))]
        [SwaggerResponse(500, "Internal Server Error", typeof(AssessmentProperties))]
        public async Task<IActionResult> GetAssessmentProperties(int UserID, int AssessmentID)
        {
            try
            {
                List<string> errorMsg = new();
                var result = await _transcriptService.GetAssessmentProperties(UserID, AssessmentID, errorMsg);
                if (result != null)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<AssessmentProperties>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Failure);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<AssessmentProperties>() { Message = LearnerAppConstants.Failure, Data = { } });
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = LearnerAppConstants.EXCEPTION_MESSAGE,
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<AssessmentProperties>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /// <summary>
        /// Get Course Table of contents
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="CourseID"></param>
        /// <returns></returns>
        [HttpGet("CourseTableOfContent/{UserID}/{CourseID}/{Percentage}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<CourseTableOfContent>))]
        [SwaggerResponse(400, "Bad Request", typeof(CourseTableOfContent))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<CourseTableOfContent>))]
        public async Task<IActionResult> CourseTableOfContent(int UserID, int CourseID, float Percentage)
        {
            try
            {
                List<string> errorMessages = new();
                var result = await _courseCatalogService.GetCourseTableOfContents(UserID, CourseID, Percentage, errorMessages);
                if (errorMessages.Any())
                {
                    logger.LogDebug(errorMessages.FirstOrDefault());
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<CourseTableOfContent>() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<CourseTableOfContent>>() { Message = LearnerAppConstants.Success, Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<CourseTableOfContent>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        [HttpGet("CommonCourseCatalogDetails/{CourseID}")]
        [SwaggerResponse(200, "Ok", typeof(CommonCourseCatalogParent))]
        [SwaggerResponse(400, "Bad Request", typeof(CommonCourseCatalogParent))]
        [SwaggerResponse(500, "Internal Server Error", typeof(CommonCourseCatalogParent))]
        public async Task<IActionResult> GetCommonCourseCatalogDetails(int CourseID)
        {
            try
            {
                List<string> errorMessages = new();
                var result = await _courseCatalogService.GetCommonCourseCatalogDetails(CourseID, errorMessages);
                if (errorMessages.Any())
                {
                    logger.LogDebug(errorMessages.FirstOrDefault());
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<CommonCourseCatalogParent>() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<CommonCourseCatalogParent>() { Message = LearnerAppConstants.Success, Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ReturnResponse<CommonCourseCatalogParent>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        #endregion

        #region Assesssments
        /// <summary>
        /// To get all assessments related to the selected sub course category
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="SubcategoryID"></param>
        /// <returns></returns>
        [HttpGet("Assessments/{CategoryID}/{SubcategoryID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<AssessmentsParent>))]
        [SwaggerResponse(400, "Bad Request", typeof(AssessmentsParent))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<AssessmentsParent>))]
        public async Task<IActionResult> Assessments(int CategoryID, int SubcategoryID, string SearchText = "", int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                List<string> errorMessages = new();

                var result = await _courseCatalogService.GetAssessments(CategoryID, SubcategoryID, SearchText, errorMessages);

                if (result.Any())
                {
                    string message = string.Empty;
                    int totalRecords = result.Count();
                    var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);

                    AssessmentsParent assessmentsParent = new()
                    {
                        PageNumber = PageNumber,
                        PageSize = PageSize,
                        TotalItems = totalRecords,
                        TotalPages = validFilter.TotalPages
                    };

                    if (result != null && result.Count() > 0)
                    {
                        result = result
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();
                        assessmentsParent.Assessments = result;
                        message = LearnerAppConstants.SUCCESSMESSAGE;
                    }
                    else
                        message = LearnerAppConstants.Failure;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<AssessmentsParent>()
                    {
                        Message = message,
                        Data = assessmentsParent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Failure);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<AssessmentsParent>() { Message = LearnerAppConstants.Failure, Data = { } });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ReturnResponse<AssessmentsParent>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        #endregion

        #endregion

    }
}
