using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TechTipsController : BaseController
    {
        private readonly ITechTipsService _techTipsService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public TechTipsController(ITechTipsService techTipsService)
        {
            _techTipsService = techTipsService ?? throw new ArgumentNullException(nameof(ITechTipsService));
        }
        #region GET

        /// <summary>
        /// To search related articles
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="CategoryID"></param>
        /// <param name="SubCategoryID"></param>
        /// <param name="TopicID"></param>
        /// <param name="Filter"></param>
        /// <param name="SearchTag"></param>
        /// <param name="SearchInID"></param>
        /// <param name="SearchInTag"></param>
        /// <param name="SearchInTitle"></param>
        /// <param name="SearchInContent"></param>
        /// <returns></returns>
        [HttpGet("TechTips/{UserID}/{CategoryID}/{SubCategoryID}/{TopicID}/{Filter}/{SearchTag}/{SearchInID}/{SearchInTag}/{SearchInTitle}/{SearchInContent}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<TechTipsGridDataParent>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<TechTipsGridDataParent>))]
        public async Task<IActionResult> TechTips(int UserID, int CategoryID = -1, int SubCategoryID = -1, int TopicID = -1,
            int Filter = -1, string SearchTag = "", int SearchInID = -1, int SearchInTag = -1, int SearchInTitle = -1, int SearchInContent = -1, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                var result = await _techTipsService.GetTechTips(UserID, CategoryID, SubCategoryID, TopicID,
                    Filter, SearchTag, SearchInID, SearchInTag, SearchInTitle, SearchInContent);

                if (result.Any())
                {
                    string message = string.Empty;
                    int totalRecords = result.Count();
                    var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);

                    TechTipsGridDataParent techTipsGridDataParent = new()
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
                        techTipsGridDataParent.TechTipsData = result;
                        message = LearnerAppConstants.SUCCESSMESSAGE;
                    }
                    else
                        message = LearnerAppConstants.LEARNING_GETHISTORY_FAILUREMESSAGE;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<TechTipsGridDataParent>()
                    {
                        Message = message,
                        Data = techTipsGridDataParent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.DASHBOARD_FAILUREMESSAGE);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<TechTipsGridDataParent>() { Message = LearnerAppConstants.DASHBOARD_FAILUREMESSAGE, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<TechTipsGridDataParent>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /// <summary>
        /// Get topics from Subcategory ID
        /// </summary>
        /// <param name="SubCategoryID"></param>
        /// <returns></returns>
        [HttpGet("Topic/{SubCategoryID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<Topics>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<Topics>))]
        public async Task<IActionResult> TopicsBySubCategory(int SubCategoryID)
        {
            try
            {
                var result = await _techTipsService.GetTopicsSubCategoryID(SubCategoryID);
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<Topics>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Failure);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<Topics>>() { Message = LearnerAppConstants.Failure, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Topics>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /// <summary>
        /// Get Topics from Category ID
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        [HttpGet("TopicsByCategory/{CategoryID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<TopicsByCategory>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<TopicsByCategory>))]
        public async Task<IActionResult> TopicsByCategory(int CategoryID)
        {
            try
            {
                var result = await _techTipsService.GetTopicsCategoryID(CategoryID);
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<TopicsByCategory>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Failure);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<TopicsByCategory>>() { Message = LearnerAppConstants.Failure, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<TopicsByCategory>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        #endregion
    }
}
