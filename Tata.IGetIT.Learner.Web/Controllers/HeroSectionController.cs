using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Tata.IGetIT.Learner.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroSectionController : BaseController
    {
        #region Declarations
        private readonly IHeroSectionService _heroSectionService;
        ILogger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        public HeroSectionController(IHeroSectionService heroSectionService)
        {
            _heroSectionService = heroSectionService ?? throw new ArgumentNullException(nameof(IHeroSectionService));
        }
        #endregion

        #region GET

        [HttpGet("GetCurrentRole/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(CurrentRole))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> GetCurrentRole(int UserID = 0)
        {
            try
            {
                List<string> errorMessages = new();
                var user = await _heroSectionService.CurrentRole(UserID, errorMessages);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new Response() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, string.Format(LearnerAppConstants.OK_MESSAGE, user.Message));
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(GetCurrentRole)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("GetSkillset/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(Skillset))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> GetSkillset(int UserID = 0)
        {
            try
            {                
                List<string> errorMessages = new();
                var user = await _heroSectionService.Skillset(UserID, errorMessages);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new Response() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, string.Format(LearnerAppConstants.OK_MESSAGE, user.Message));
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
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("GetCurrentLevel/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(CurrentLevel))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> GetCurrentLevel(int UserID = 0)
        {
            try
            {
                List<string> errorMessages = new();
                var user = await _heroSectionService.CurrentLevel(UserID, errorMessages);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new Response() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, string.Format(LearnerAppConstants.OK_MESSAGE, user.Message));
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(GetCurrentLevel)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("GetCareerPath/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(CareerPath))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> GetCareerPath(int UserID = 0)
        {
            try
            {
                List<string> errorMessages = new();
                var user = await _heroSectionService.CareerPath(UserID, errorMessages);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new Response() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, string.Format(LearnerAppConstants.OK_MESSAGE, user.Message));
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(GetCareerPath)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("GetTargetRoleCareerPath/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(TargetRoleCareerPath))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> GetTargetRoleCareerPath(int UserID = 0)
        {
            try
            {
                List<string> errorMessages = new();
                var user = await _heroSectionService.TargetRoleCareerPath(UserID, errorMessages);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new Response() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, string.Format(LearnerAppConstants.OK_MESSAGE, user.Message));
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(GetTargetRoleCareerPath)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("GetTargetRoleCareerPathPercentage/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(TargetRoleCareerPathPercentage))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> GetTargetRoleCareerPathPercentage(int UserID = 0)
        {
            try
            {
                List<string> errorMessages = new();
                var user = await _heroSectionService.TargetRoleCareerPathPercentage(UserID, errorMessages);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new Response() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, string.Format(LearnerAppConstants.OK_MESSAGE, user.Message));
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(GetTargetRoleCareerPathPercentage)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("GetTargetRole/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(TargetRoles))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> GetTargetRole(int UserID = 0)
        {
            try
            {
                List<string> errorMessages = new();
                var user = await _heroSectionService.TargetRole(UserID, errorMessages);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new Response() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, string.Format(LearnerAppConstants.OK_MESSAGE, user.Message));
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(GetTargetRole)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("GetTargetRoleCurrentLevel/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(TargetRoleCurrentLevel))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> GetTargetRoleCurrentLevel(int UserID = 0)
        {
            try
            {
                List<string> errorMessages = new();
                var user = await _heroSectionService.TargetRoleCurrentLevel(UserID, errorMessages);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new Response() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, string.Format(LearnerAppConstants.OK_MESSAGE, user.Message));
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(GetTargetRoleCurrentLevel)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        #endregion
    }
}
