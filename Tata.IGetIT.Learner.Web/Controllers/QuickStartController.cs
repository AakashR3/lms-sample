using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Tata.IGetIT.Learner.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuickStartController : BaseController
    {
        private readonly IQuickStartService _quickstartService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public QuickStartController(IQuickStartService quickstartService)
        {
            _quickstartService = quickstartService ?? throw new ArgumentNullException(nameof(quickstartService));

        }

        /// <summary>
        /// Get Quick Start Grid Data
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="CategoryID"></param>
        /// <param name="SubCategoryID"></param>
        /// <returns>QuickStartGridData</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<QuickStartGridData>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<QuickStartGridData>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<QuickStartGridData>))]
        [Route("QuickStartGrid/{UserID}/{CategoryID}/{SubCategoryID}")]
        public async Task<IActionResult> GetQuickStartGridData(int UserID, int CategoryID, int SubCategoryID, string SearchText = "", int PageNumber = 1)
        {
            try
            {
                string message = string.Empty;
                QuickStartGrid quickStartGrid = new QuickStartGrid();
                quickStartGrid.UserID = UserID;
                quickStartGrid.CategoryID = CategoryID;
                quickStartGrid.SubCategoryID = SubCategoryID;
                quickStartGrid.SearchText = SearchText;

                var result = await _quickstartService.GetQuickStartGridData(quickStartGrid);

                int totalRecords = result.Count();
                int pageSize = LearnerAppConstants.QUICKSTART_PAGSIZE;
                var validFilter = new PaginationFilter(PageNumber, pageSize, totalRecords);

                QuickStartGridDataParent QuickStartGridDataParent = new QuickStartGridDataParent();
                QuickStartGridDataParent.PageNumber = PageNumber;
                QuickStartGridDataParent.PageSize = pageSize;
                QuickStartGridDataParent.TotalItems = totalRecords;
                QuickStartGridDataParent.TotalPages = validFilter.TotalPages;

                if (result != null && result.Count() > 0)
                {
                    result = result
                            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                            .Take(validFilter.PageSize).ToList();
                    QuickStartGridDataParent.QuickStartGridData = result;
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                }
                else
                    message = LearnerAppConstants.QUICKSTART_GRIDDATA_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<QuickStartGridDataParent>()
                {
                    Message = message,
                    //     Message = message + " PageNumber : " + QuickStartGridDataParent.PageNumber.ToString() 
                    //  + " PageSize : " + QuickStartGridDataParent.PageSize.ToString()
                    //  + " TotalItems : " + QuickStartGridDataParent.TotalItems.ToString()
                    //  + " TotalPages : " + QuickStartGridDataParent.TotalPages,
                    Data = QuickStartGridDataParent
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetQuickStartGridData)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Categories for the UserID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>Categories</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<Categories>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<Categories>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<Categories>))]
        [Route("Categories/{UserID}/{Type}")]
        public async Task<IActionResult> GetCategories(int UserID, int Type)
        {
            try
            {
                string message = string.Empty;
                QuickStartGrid quickStartGrid = new QuickStartGrid();
                quickStartGrid.UserID = UserID;
                quickStartGrid.Type = Type;

                var result = await _quickstartService.GetCategories(quickStartGrid);

                if (result != null && result.Count() > 0)
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.QUICKSTART_GRIDDATA_FAILUREMESSAGE;
                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<Categories>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetCategories)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Subcategories against UserID and/or Category ID 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="CategoryID"></param>
        /// <returns>SubCategories</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<SubCategories>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<SubCategories>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<SubCategories>))]
        [Route("SubCategories/{UserID}/{CategoryID}/{Type}")]
        public async Task<IActionResult> GetSubCategories(int UserID, int CategoryID, int Type=1)
        {
            try
            {
                string message = string.Empty;
                QuickStartGrid quickStartGrid = new QuickStartGrid();
                quickStartGrid.UserID = UserID;
                quickStartGrid.CategoryID = CategoryID;
                quickStartGrid.Type = Type;

                var result = await _quickstartService.GetSubCategories(quickStartGrid);

                if (result != null && result.Count() > 0)
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.QUICKSTART_GETSUBCATEGORY_FAILUREMESSAGE;
                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<SubCategories>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetSubCategories)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Notification Flag for Quick Start New Release
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Flag"></param>
        /// <returns>int</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(int))]
        [SwaggerResponse(400, "Bad Request", typeof(int))]
        [SwaggerResponse(500, "Internal Server Error", typeof(int))]
        [Route("GetNotificationFlag/{UserID}")]
        public async Task<IActionResult> GetNotificationFlag(int UserID, string Flag=LearnerAppConstants.QUICKSTART_NEWRELEASEFLAG)
        {
            try
            {
                string message = string.Empty;
                QuickStartNotification quickStartNotification = new QuickStartNotification();
                quickStartNotification.UserID = UserID;
                quickStartNotification.Flag = Flag;

                var result = await _quickstartService.GetNotificationFlag(quickStartNotification);

                if (result != null)
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.QUICKSTART_GETQUICKSTARTNOTIFICATION_FAILUREMESSAGE;
                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<int>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetNotificationFlag)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Notification Flag for Quick Start New Release
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Flag"></param>
        /// <returns>int</returns>
        ///[Authorize]
        [HttpPut("UpdateNotificationFlag")]
        [SwaggerResponse(200, "Ok", typeof(int))]
        [SwaggerResponse(400, "Bad Request", typeof(int))]
        [SwaggerResponse(500, "Internal Server Error", typeof(int))]
        
        public async Task<IActionResult> UpdateNotificationFlag(QuickStartNotification quickStartNotification)
        {
            try
            {
                string message = string.Empty;
                var result = await _quickstartService.UpdateNotificationFlag(quickStartNotification);

                if (result != null)
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.QUICKSTART_GETQUICKSTARTNOTIFICATION_FAILUREMESSAGE;
                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<int>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(UpdateNotificationFlag)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


    }
}