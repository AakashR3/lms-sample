using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Tata.IGetIT.Learner.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeadersAndMenuController : BaseController
    {
        #region Declarations
        private readonly IHeadersAndMenusService _headersAndMenusService;
        //private readonly IUserService _userService;
        ILogger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        public HeadersAndMenuController(IHeadersAndMenusService headersAndMenusService)
        {
            _headersAndMenusService = headersAndMenusService ?? throw new ArgumentNullException(nameof(IHeadersAndMenusService));
            //_userService = userService ?? throw new ArgumentNullException(nameof(IUserService));
        }
        #endregion

        #region GET
        /// <summary>
        /// Get all menu items list
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet("MenuItems/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<HeadersAndMenu_UserMenu>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<HeadersAndMenu_UserMenu>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<HeadersAndMenu_UserMenu>))]
        public async Task<IActionResult> GetMenuItems(int UserID)
        {
            try
            {
                List<string> errorMessages = new();
                var MenuItems = await _headersAndMenusService.MenuItems(UserID, errorMessages);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest,
                        new ReturnResponse<IEnumerable<HeadersAndMenu_UserMenu>>() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK,
                        new ReturnResponse<IEnumerable<HeadersAndMenu_UserMenu>>() { Message = LearnerAppConstants.OK, Data = MenuItems });
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
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ReturnResponse<IEnumerable<HeadersAndMenu_UserMenu>>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /// <summary>
        /// Get user's points
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet("Points/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(HeadersAndMenu_UserPoints))]
        [SwaggerResponse(400, "Bad Request", typeof(HeadersAndMenu_UserPoints))]
        [SwaggerResponse(500, "Internal Server Error", typeof(HeadersAndMenu_UserPoints))]
        public async Task<IActionResult>  GetPoints(int UserID)
        {
            try
            {
                List<string> errorMessages = new();
                //var result = await _userService.VerifySession(SessionID, UserTypeID, AccTypeID, Username, TimeoutMin, errorMessages);

                //if (errorMessages.Any())
                //{
                //    return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<VerifySessionData>() { Message = errorMessages.FirstOrDefault() });
                //}
                //else
                //{
                //    if (result.SessionStatus > 0)
                //    {
                        //return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<VerifySessionData>() { Message = LearnerAppConstants.Success, Data = result });
                        var Points = await _headersAndMenusService.Points(UserID, errorMessages);

                        if (errorMessages.Any())
                        {
                            return StatusCode((int)HttpStatusCode.BadRequest,
                                new ReturnResponse<HeadersAndMenu_UserPoints>() { Message = errorMessages.FirstOrDefault() });
                        }
                        else
                        {
                            return StatusCode((int)HttpStatusCode.OK,
                                new ReturnResponse<HeadersAndMenu_UserPoints>() { Message = LearnerAppConstants.OK, Data = Points });
                        }
                //    }
                //    else
                //    {
                //        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<VerifySessionData>() { Message = LearnerAppConstants.Success, Data = result });
                //    }
                //}
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = LearnerAppConstants.EXCEPTION_MESSAGE,
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ReturnResponse<HeadersAndMenu_UserPoints>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /// <summary>
        /// Get user's cart count
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet("UserCartCount/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(HeadersAndMenu_UserCartCount))]
        [SwaggerResponse(400, "Bad Request", typeof(HeadersAndMenu_UserCartCount))]
        [SwaggerResponse(500, "Internal Server Error", typeof(HeadersAndMenu_UserCartCount))]

        public async Task<IActionResult> GetUserCartCount(int UserID)
        {
            try
            {
                List<string> errorMessages = new();
                var result = await _headersAndMenusService.UserCartCount(UserID, errorMessages);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest,
                        new ReturnResponse<HeadersAndMenu_UserCartCount>() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK,
                        new ReturnResponse<HeadersAndMenu_UserCartCount>() { Message = LearnerAppConstants.OK, Data = new HeadersAndMenu_UserCartCount { CartCount = result } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ReturnResponse<HeadersAndMenu_UserCartCount>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /// <summary>
        /// Get user's notifications
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet("UserNotification/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(HeadersAndMenu_UserNotificationDetails))]
        [SwaggerResponse(400, "Bad Request", typeof(HeadersAndMenu_UserNotificationDetails))]
        [SwaggerResponse(500, "Internal Server Error", typeof(HeadersAndMenu_UserNotificationDetails))]
        public async Task<IActionResult> GetUserNotifications(int UserID)
        {
            try
            {
                List<string> errorMessages = new();
                var result = await _headersAndMenusService.UserNotification(UserID, errorMessages);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest,
                        new ReturnResponse<HeadersAndMenu_UserNotificationDetails>() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK,
                        new ReturnResponse<HeadersAndMenu_UserNotificationDetails>() { Message = LearnerAppConstants.OK, Data = result });
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
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ReturnResponse<HeadersAndMenu_UserNotificationDetails>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("AllUserNotifications/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(NotificationsCourseReleases))]
        [SwaggerResponse(400, "Bad Request", typeof(NotificationsCourseReleases))]
        [SwaggerResponse(500, "Internal Server Error", typeof(NotificationsCourseReleases))]
        public async Task<IActionResult> GetAllUserNotifications(int UserID)
        {
            try
            {
                List<string> errorMessages = new();
                var result = await _headersAndMenusService.AllUserNotification(UserID);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.OK,
                        new ReturnResponse<NotificationsCourseReleases>() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK,
                        new ReturnResponse<NotificationsCourseReleases>() { Message = LearnerAppConstants.OK, Data = result });
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
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ReturnResponse<NotificationsCourseReleases>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        /// <summary>
        /// Get user's favorite courses and assessments
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet("Favorites/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<HeadersAndMenu_MoreActions_Favorites>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<HeadersAndMenu_MoreActions_Favorites>))]
        public async Task<IActionResult> Favorites(int UserID)
        {
            try
            {
                List<string> errorMessages = new();
                var result = await _headersAndMenusService.GetFavorites(UserID, errorMessages);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest,
                        new ReturnResponse<IEnumerable<HeadersAndMenu_MoreActions_Favorites>>() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK,
                        new ReturnResponse<IEnumerable<HeadersAndMenu_MoreActions_Favorites>>() { Message = LearnerAppConstants.OK, Data = result });
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
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ReturnResponse<IEnumerable<HeadersAndMenu_MoreActions_Favorites>>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /// <summary>
        /// Get user's subscription details
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet("Subscriptions/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(HeadersAndMenu_MoreActions_Subscription_Parent))]
        [SwaggerResponse(500, "Internal Server Error", typeof(HeadersAndMenu_MoreActions_Subscription_Parent))]
        public async Task<IActionResult> Subscriptions(int UserID)
        {
            try
            {
                var Currency = base.GetCurrency();
                List<string> errorMessages = new();
                var result = await _headersAndMenusService.GetSubscriptions(UserID, Currency, errorMessages);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest,
                        new ReturnResponse<HeadersAndMenu_MoreActions_Subscription_Parent>() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK,
                        new ReturnResponse<HeadersAndMenu_MoreActions_Subscription_Parent>() { Message = LearnerAppConstants.OK, Data = result });
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
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ReturnResponse<HeadersAndMenu_MoreActions_Subscription_Parent>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("CheckTrialUser/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(TrialUserDetails))]
        [SwaggerResponse(500, "Internal Server Error", typeof(TrialUserDetails))]
        public async Task<IActionResult> CheckTrialUser(int UserID)
        {
            try
            {
                List<string> errorMessages = new();
                var result = await _headersAndMenusService.CheckTrialUser(UserID, errorMessages);
                if (result != null)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<TrialUserDetails>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Failure);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<TrialUserDetails>() { Message = LearnerAppConstants.Failure, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<TrialUserDetails>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        #endregion
    }
}