using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Tata.IGetIT.Learner.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : BaseController
    {
        private readonly IUserProfileService _userprofileService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userprofileService = userProfileService ?? throw new ArgumentNullException(nameof(userProfileService));

        }

        /// <summary>
        /// Get Personal Profile
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>UserPersonalProfile</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(UserPersonalProfile))]
        [SwaggerResponse(400, "Bad Request", typeof(UserPersonalProfile))]
        [SwaggerResponse(500, "Internal Server Error", typeof(UserPersonalProfile))]
        [Route("PersonalProfile/{UserID}")]
        public async Task<IActionResult> GetPersonalProfile(int UserID)
        {
            try
            {
                string message = string.Empty;
                UserProfile userProfile = new UserProfile();

                userProfile.UserID = UserID;
                userProfile.Type = 'P';

                var result = await _userprofileService.GetPersonalProfile(userProfile);

                if (result != null)
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.USERPROFILE_PERSONALPROFILE_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<UserPersonalProfile>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetPersonalProfile)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Business Profile
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>UserBusinessProfile</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(UserBusinessProfile))]
        [SwaggerResponse(400, "Bad Request", typeof(UserBusinessProfile))]
        [SwaggerResponse(500, "Internal Server Error", typeof(UserBusinessProfile))]
        [Route("BusinessProfile/{UserID}")]
        public async Task<IActionResult> GetBusinessProfile(int UserID)
        {
            try
            {
                string message = string.Empty;
                UserProfile userProfile = new UserProfile();

                userProfile.UserID = UserID;
                userProfile.Type = 'B';

                var result = await _userprofileService.GetBusinessProfile(userProfile);

                if (result != null)
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.USERPROFILE_BUSINESSPROFILE_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<UserBusinessProfile>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetBusinessProfile)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Notification Settings
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>UserNotificationSettings</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(UserNotificationSettings))]
        [SwaggerResponse(400, "Bad Request", typeof(UserNotificationSettings))]
        [SwaggerResponse(500, "Internal Server Error", typeof(UserNotificationSettings))]
        [Route("UserNotification/{UserID}")]
        public async Task<IActionResult> GetUserNotificationSettings(int UserID)
        {
            try
            {
                string message = string.Empty;
                UserProfile userProfile = new UserProfile();

                userProfile.UserID = UserID;
                userProfile.Type = 'N';

                var result = await _userprofileService.GetUserNotificationSettings(userProfile);

                if (result != null)
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.USERPROFILE_NOTIFICATIONSETTING_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<UserNotificationSettings>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetUserNotificationSettings)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Update Personal Profile
        /// </summary>
        /// <param name="userPersonalProfile"></param>
        /// <returns>string</returns>

        //[Authorize]
        [HttpPost("PersonalProfileUpdate")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> UpdatePersonalProfile([FromBody] UserPersonalProfile userPersonalProfile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userprofileService.UpdatePersonalProfile(userPersonalProfile);

                    if (result != null && result.Split('^')[0] == "1")
                    {
                        string errMsg = result.ToString().Split('^')[1];
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new Response()
                        {
                            Message = errMsg
                        });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.USERPROFILE_PERSONALPROFILE_SUCCESSMESSAGE);
                        return StatusCode((int)HttpStatusCode.OK, new Response()
                        {
                            Message = LearnerAppConstants.USERPROFILE_PERSONALPROFILE_SUCCESSMESSAGE
                        });
                    }
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.MODEL_VALIDATION_FAILED);
                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(ex.Message.ToString(), nameof(UpdatePersonalProfile)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Update Business Profile
        /// </summary>
        /// <param name="userBusinessProfile"></param>
        /// <returns>string</returns>

        //[Authorize]
        [HttpPost("BusinessProfileUpdate")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> UpdateBusinessProfile([FromBody] UserBusinessProfile userBusinessProfile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userprofileService.UpdateBusinessProfile(userBusinessProfile);

                    if (result != null && result.Split('^')[0] == "1")
                    {
                        string errMsg = result.ToString().Split('^')[1];
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new Response()
                        {
                            Message = errMsg
                        });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.USERPROFILE_BUSINESSPROFILE_SUCCESSMESSAGE);
                        return StatusCode((int)HttpStatusCode.OK, new Response()
                        {
                            Message = LearnerAppConstants.USERPROFILE_BUSINESSPROFILE_SUCCESSMESSAGE
                        });
                    }
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.MODEL_VALIDATION_FAILED);
                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(ex.Message.ToString(), nameof(UpdateBusinessProfile)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Update Notification Settings
        /// </summary>
        /// <param name="userNotificationSettings"></param>
        /// <returns>string</returns>

        //[Authorize]
        [HttpPost("UserNotificationUpdate")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> UpdateUserNotificationSettings([FromBody] UserNotificationSettings userNotificationSettings)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userprofileService.UpdateUserNotificationSettings(userNotificationSettings);

                    if (result != null && result.Split('^')[0] == "1")
                    {
                        string errMsg = result.ToString().Split('^')[1];
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new Response()
                        {
                            Message = result
                        });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.USERPROFILE_NOTIFICATIONSETTING_SUCCESSMESSAGE);
                        return StatusCode((int)HttpStatusCode.OK, new Response()
                        {
                            Message = LearnerAppConstants.USERPROFILE_NOTIFICATIONSETTING_SUCCESSMESSAGE
                        });
                    }
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.MODEL_VALIDATION_FAILED);
                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(ex.Message.ToString(), nameof(UpdateUserNotificationSettings)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Business Manager
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>UserDetails</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<UserDetails>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<UserDetails>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<UserDetails>))]
        [Route("BusinessManager/{UserID}")]
        public async Task<IActionResult> GetBusinessManager(int UserID)
        {
            try
            {
                string message = string.Empty;
                UserProfile userProfile = new UserProfile();
                userProfile.UserID = UserID;

                var result = await _userprofileService.GetBusinessManager(userProfile);

                if (result != null)
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.USERPROFILE_BUSINESSMANAGER_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<UserDetails>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetBusinessManager)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Country
        /// </summary>       
        /// <returns>Country</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<Country>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<Country>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<Country>))]
        [Route("CountryList")]
        public async Task<IActionResult> GetCountry()
        {
            try
            {
                string message = string.Empty;
                var result = await _userprofileService.GetCountry();

                if (result != null)
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.USERPROFILE_COUNTRY_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<Country>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetCountry)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get State By Region ID
        /// </summary>
        /// <param name="RegionID"></param>
        /// <returns>State</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<State>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<State>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<State>))]
        [Route("StateList/{RegionID}")]
        public async Task<IActionResult> GetStateByRegion(int RegionID)
        {
            try
            {
                string message = string.Empty;
                State state = new State();
                state.RegionID = RegionID;
                var result = await _userprofileService.GetStateByRegion(state);

                if (result != null)
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.USERPROFILE_STATE_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<State>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetStateByRegion)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Industry Info
        /// </summary>       
        /// <returns>IndustryInfo</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<IndustryInfo>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<IndustryInfo>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<IndustryInfo>))]
        [Route("IndustryInfo")]
        public async Task<IActionResult> GetWorkIndustry()
        {
            try
            {
                string message = string.Empty;
                var result = await _userprofileService.GetWorkIndustry();

                if (result != null)
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.USERPROFILE_INDUSTRYINFO_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<IndustryInfo>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetWorkIndustry)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Cad Application List
        /// </summary>
        /// <returns>CadApplicationList</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<CadApplicationList>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<CadApplicationList>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<CadApplicationList>))]
        [Route("CadApplicationList")]
        public async Task<IActionResult> GetCadApplicationList()
        {
            try
            {
                string message = string.Empty;
                var result = await _userprofileService.GetCadApplicationList();

                if (result != null)
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.USERPROFILE_CADAPPLICATIONLIST_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<CadApplicationList>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetCadApplicationList)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get User Groups
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>UserGroups</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<UserGroups>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<UserGroups>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<UserGroups>))]
        [Route("UserGroups/{UserID}")]
        public async Task<IActionResult> GetUserGroups(int UserID)
        {
            try
            {
                string message = string.Empty;
                UserProfile userProfile = new UserProfile();
                userProfile.UserID = UserID;

                var result = await _userprofileService.GetUserGroups(userProfile);

                if (result != null)
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.USERPROFILE_USERGROUPS_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<UserGroups>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetUserGroups)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

    }
}