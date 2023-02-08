using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Tata.IGetIT.Learner.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : BaseController
    {
        private readonly INotificationService _NotificationService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public NotificationController(INotificationService NotificationService)
        {
            _NotificationService = NotificationService ?? throw new ArgumentNullException(nameof(NotificationService));

        }

        /// <summary>
        /// Get Notification Data
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="AccountID"></param>
        /// <param name="NotificationTypeId"></param>
        /// <returns>NotificationGridData</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<NotificationData>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<NotificationData>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<NotificationData>))]
        [Route("NotificationData/{AccountID}/{UserID}/{NotificationTypeId}")]
        public async Task<IActionResult> GetNotification(int AccountID, int UserID=0, int NotificationTypeId=0)
        {
            try
            {
                string message = string.Empty;
                Notification Notification = new Notification();
                Notification.UserID = UserID;
                Notification.AccountID = AccountID;
                Notification.NotificationTypeId = NotificationTypeId;

                var result = await _NotificationService.GetNotification(Notification);

                if (result != null)
                {
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                }
                else
                    message = LearnerAppConstants.NOTIFICATION_DATA_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<NotificationData>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetNotification)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
    }
}