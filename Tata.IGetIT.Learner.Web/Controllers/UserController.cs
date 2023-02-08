
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Tata.IGetIT.Learner.Repository.Models.AccountManagement;

namespace Tata.IGetIT.Learner.Web.Controllers
{
    /// <summary>
    /// Auth controller to handle all authorization requests
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {

        #region Declarations
        private readonly IUserService _userService;
        ILogger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(IUserService));
        }
        #endregion

        #region GET


        /// <summary>
        /// To Get Geo Location
        /// </summary>       
        /// <returns></returns>
        [HttpGet("Location")]
        [SwaggerResponse(200, "Ok", typeof(Location))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> Location()
        {
            try
            {
                string ClientIP = ClientIPAddress(HttpContext);
                List<string> errorMessages = new List<string>();
                var result = await _userService.Location(ClientIP, errorMessages);
                result.IPAddress = ClientIP;

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<Response>() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<Location>() { Message = LearnerAppConstants.Success,Data= result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, "Location"), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }






        [HttpGet("VerifySession/{SessionID}/{UserTypeID}/{AccTypeID}/{Username}/{TimeoutMin}")]
        [SwaggerResponse(200, "Ok", typeof(ReturnResponse<VerifySessionData>))]
        [SwaggerResponse(400, "Bad Request", typeof(ReturnResponse<VerifySessionData>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ReturnResponse<VerifySessionData>))]
        public async Task<IActionResult> VerifySession(string SessionID, int UserTypeID, int AccTypeID, string Username, int TimeoutMin)
        {
            try
            {
                List<string> errorMessages = new List<string>();
                var result = await _userService.VerifySession(SessionID, UserTypeID, AccTypeID, Username, TimeoutMin, errorMessages);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<VerifySessionData>() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<VerifySessionData>() { Message = LearnerAppConstants.Success, Data = result });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<VerifySessionData>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Validating username
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [NonAction]
        [HttpGet("IsValidUser/{UserName}")]
        public async Task<bool> IsValidUser(string UserName)
        {
            try
            {

                return await _userService.CheckUser(UserName);
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(IsValidUser)), ex);

                return false;
            }
        }

        /// <summary>
        /// Validating username
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [NonAction]
        public static string ClientIPAddress(HttpContext context)
        {
            try
            {
                string PublidIPAddress = string.Empty;
                if (!string.IsNullOrEmpty(context.Request.Headers["X-Forwarded-For"]))
                {
                    PublidIPAddress = context.Request.Headers["X-Forwarded-For"];
                }
                else
                {
                    PublidIPAddress = context.Request.HttpContext.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString();
                }

                if (PublidIPAddress.Contains(":") == true)
                {
                    PublidIPAddress = PublidIPAddress.Split(":")[0];
                }

                return PublidIPAddress;
            }
            catch
            {
                return "";
            }
        }


        /// <summary>
        /// ForgotPassword
        /// </summary>
        /// <param name="UserName">Username </param>
        /// <returns></returns>
        [HttpGet("ForgotPassword/{UserName}")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> ForgotPassword(string UserName)
        {
            try
            {

                if (UserName.IsNullOrEmptyOrWhiteSpace())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, LearnerAppConstants.INVALID_USERNAME);
                }

                List<string> errorMessages = new List<string>();
                var user = await _userService.ForgotPasswordAsync(UserName, errorMessages);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new Response() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new Response() { Message = "Password reset email is sent successfully." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, "ForgotPassword"), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// VerifyEmailLink
        /// </summary>
        /// <param name="EmailSessionId"></param>
        /// <returns></returns>
        [HttpGet("VerifyEmailLink/{EmailSessionId}")]
        [SwaggerResponse(200, "Ok", typeof(string))]
        [SwaggerResponse(400, "Bad Request", typeof(string))]
        public async Task<IActionResult> VerifyEmailLink(string EmailSessionId)
        {
            try
            {
                if (EmailSessionId.IsNullOrEmptyOrWhiteSpace())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest,
                        LearnerAppConstants.INVALID_EMAILSESSIONID);
                }

                List<string> errorMessages = new();
                var returnValue = await _userService.VerifyEmailLink(EmailSessionId, errorMessages);

                if (returnValue == 1)
                {
                    return StatusCode((int)HttpStatusCode.OK,
                        new Response() { Message = LearnerAppConstants.VALID_PASSWORD_LINK });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest,
                        new Response() { Message = errorMessages.FirstOrDefault() });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.EXCEPTION_MESSAGE,
                    nameof(VerifyEmailLink)), ex);

                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        /// <summary>
        /// VerifyForgotPasswordEmail
        /// </summary>
        /// <param name="TokenID"></param>
        /// <returns></returns>
        [HttpGet("VerifyForgotPasswordEmail/{TokenID}")]
        [SwaggerResponse(200, "Ok", typeof(string))]
        [SwaggerResponse(400, "Bad Request", typeof(string))]
        public async Task<IActionResult> VerifyForgotPasswordEmail(string TokenID)
        {
            UserDetails _userDetails = new UserDetails();
            try
            {
                if (TokenID.IsNullOrEmptyOrWhiteSpace())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest,
                        LearnerAppConstants.INVALID_EMAILSESSIONID);
                }

                string UserName = UtilityHelper.Decrypt(TokenID);
                List<string> errorMessages = new();
                var user = await _userService.ForgotPasswordAsync(UserName, errorMessages);
                if (user != null && !string.IsNullOrEmpty(user.UserName) && user.UserStatusId == 2)
                {
                    _userDetails.UserId = user.UserId.ToString();
                    _userDetails.UserName = user.UserName;
                    _userDetails.AccountId = user.AccountId.ToString();
                    _userDetails.FirstName = user.FirstName;
                    _userDetails.LastName = user.LastName;
                    _userDetails.Email = user.Email;

                    logger.LogDebug(LearnerAppConstants.PASSWORD_RESET_EMAIL_SUCCESS_MESSAGE);
                    return StatusCode((int)HttpStatusCode.OK,
                        new Response() { Message = LearnerAppConstants.PASSWORD_RESET_EMAIL_SUCCESS_MESSAGE, Data = _userDetails });
                }
                else
                {
                    logger.LogInfo(errorMessages.FirstOrDefault());
                    return StatusCode((int)HttpStatusCode.BadRequest, new Response() { Message = errorMessages.FirstOrDefault() });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.EXCEPTION_MESSAGE,
                    nameof(VerifyForgotPasswordEmail)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });

            }
        }

        /// <summary>
        /// SSORequest
        /// </summary>
        /// <param name="DomainName"></param>
        /// <param name="ItemID"></param>
        /// <param name="ItemTypeID"></param>
        /// <returns></returns>
        [HttpGet("SSORequest")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> SSORequest(string DomainName, int ItemID = 0, int ItemTypeID = 0)
        {
            try
            {
                List<string> errorMessages = new();
                int AccountID = await _userService.GetAccountIDByDomainName(DomainName, errorMessages);
                if (errorMessages.Any())
                {
                    logger.LogDebug(LearnerAppConstants.DOMAIN_NAME_DOES_NOT_EXIST);
                    return StatusCode((int)HttpStatusCode.NotFound, new Response()
                    {
                        Message = LearnerAppConstants.DOMAIN_NAME_DOES_NOT_EXIST
                    });
                }
                else
                {
                    var result = await _userService.GetSSOInfoByAccountID(AccountID, errorMessages);
                    if (errorMessages.Any())
                    {
                        logger.LogDebug(errorMessages.FirstOrDefault());
                        return StatusCode((int)HttpStatusCode.NotFound, new Response()
                        {
                            Message = errorMessages.FirstOrDefault()
                        });
                    }
                    else
                    {
                        AuthRequest req = new(result);
                        List<SamlDetails> _samlDetails = new()
                        {
                            new SamlDetails
                            {
                                ssoLoginUrl = result.ssoUrl + "?SAMLRequest=" + req.GetRequest(),
                                accountID = AccountID,
                                ssoReqID = req.id
                            }
                        };

                        var loggingResult = _userService.SSOLogging(req.id, AccountID, ItemID, ItemTypeID, errorMessages); //Log SSO Request  

                        if (errorMessages.Any())
                        {
                            throw new Exception(errorMessages.FirstOrDefault());
                        }
                        else
                        {
                            logger.LogDebug(LearnerAppConstants.SSO_URL_GENERATED);
                            return StatusCode((int)HttpStatusCode.OK, new SamelResponse()
                            {
                                Message = LearnerAppConstants.SSO_URL_GENERATED,
                                Data = _samlDetails

                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE,
                    nameof(VerifyForgotPasswordEmail)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        #endregion

        #region POST
        /// <summary>
        /// User Authentication
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("UserAuthentication")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> UserAuthentication([FromBody] Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string ClientIP = ClientIPAddress(HttpContext);

                    List<string> errorMessage = new();
                    var userDetails = await _userService.Authentication(login, ClientIP, errorMessage);

                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new Response()
                        {
                            Message = errMsg,
                            Data = userDetails
                        });
                    }
                    else
                    {

                        //TODO: Setting cookies for testing
                        //SessionID cookie
                        var sessionIdCookie = new CookieOptions();
                        sessionIdCookie.Expires = DateTime.Now.AddDays(30);
                        sessionIdCookie.Path = "/";
                        sessionIdCookie.IsEssential = true;
                        sessionIdCookie.Secure = true;
                        sessionIdCookie.HttpOnly = false;
                        sessionIdCookie.Domain = Request.Host.Value;
                        Response.Cookies.Append("SessionID", userDetails.SessionId, sessionIdCookie);

                        //UserID cookie
                        var userIdCookie = new CookieOptions();
                        userIdCookie.Expires = DateTime.Now.AddDays(30);
                        userIdCookie.Path = "/";
                        userIdCookie.Secure = true;
                        userIdCookie.HttpOnly = false;
                        userIdCookie.Domain = "." + Request.Host.Value;
                        Response.Cookies.Append("UserID", userDetails.UserId, userIdCookie);

                        //AccountID cookie
                        var accountIdCookie = new CookieOptions();
                        accountIdCookie.Expires = DateTime.Now.AddDays(30);
                        accountIdCookie.Path = "/";
                        accountIdCookie.Secure = true;
                        accountIdCookie.HttpOnly = false;
                        accountIdCookie.Domain = ".igetitv2-dev.myigetit.com";
                        Response.Cookies.Append("AccountID", userDetails.AccountId, accountIdCookie);

                        //Hash cookie
                        string plainText = userDetails.SessionId.ToUpper() + userDetails.AccountId.ToString() + userDetails.UserId.ToString();
                        string md5hash = GetMD5Hash(plainText);

                        var hashIdCookie = new CookieOptions();
                        hashIdCookie.Expires = DateTime.Now.AddDays(30);
                        hashIdCookie.Path = "/";
                        hashIdCookie.Secure = true;
                        hashIdCookie.HttpOnly = false;
                        hashIdCookie.Domain = "igetitv2-dev.myigetit.com";
                        Response.Cookies.Append("Hash", md5hash, hashIdCookie);

                        logger.LogDebug(LearnerAppConstants.LOGIN_SUCCESS_MESSAGE);
                        return StatusCode((int)HttpStatusCode.OK, new Response()
                        {
                            Message = LearnerAppConstants.LOGIN_SUCCESS_MESSAGE,
                            Data = userDetails
                        });
                    }
                }
                else
                {
                    //logger.LogDebug(LearnerAppConstants.MODEL_VALIDATION_FAILED, url: Request.GetDisplayUrl());

                    logger.LogDetails(new LogDetails(LogType.Error)
                    {
                        Message = LearnerAppConstants.MODEL_VALIDATION_FAILED,
                        AppUrl = Request.GetDisplayUrl(),
                        RequestParam = login
                    });

                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE,
                    nameof(UserAuthentication)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });

                return StatusCode((int)HttpStatusCode.InternalServerError, new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Linked-in Validation
        /// </summary>
        /// <param name="linkedinValidation"></param>
        /// <returns></returns>
        [HttpPost("LinkedinValidation")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> LinkedinValidation([FromBody] LinkedinValidation linkedinValidation)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    string ClientIP = ClientIPAddress(HttpContext);
                    List<string> errorMessages = new();
                    var userDetails = await _userService.LinkedinValidation(linkedinValidation, ClientIP, errorMessages);

                    if (errorMessages.Any())
                    {
                        var errMsg = errorMessages.FirstOrDefault();
                        logger.LogInfo(errMsg);
                        return StatusCode((int)HttpStatusCode.BadRequest, new Response()
                        {
                            Message = errMsg
                        });
                    }

                    logger.LogTrace(LearnerAppConstants.SOCIAL_LOGIN_SUCCESS_MESSAGE);
                    return StatusCode((int)HttpStatusCode.OK, new Response()
                    {
                        Message = LearnerAppConstants.SOCIAL_LOGIN_SUCCESS_MESSAGE,
                        Data = userDetails
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.MODEL_VALIDATION_FAILED);
                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, "LinkedinValidation"), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        /// <summary>
        /// Social login validations
        /// </summary>
        /// <param name="socialRegisteration"></param>
        /// <returns></returns>
        [HttpPost("SocialValidation")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> SocialValidation([FromBody] SocialRegisteration socialRegisteration)
        {
            try
            {
                List<string> errorMessages = new();
                if (ModelState.IsValid)
                {
                    string ClientIP = ClientIPAddress(HttpContext);

                    var userDetails = await _userService.SocialValidation(socialRegisteration, ClientIP, errorMessages);

                    if (errorMessages.Any())
                    {
                        var errMsg = errorMessages.FirstOrDefault();
                        logger.LogInfo(errMsg);
                        return StatusCode((int)HttpStatusCode.BadRequest, new Response()
                        {
                            Message = errMsg
                        });
                    }

                    logger.LogTrace(LearnerAppConstants.SOCIAL_LOGIN_SUCCESS_MESSAGE);
                    return StatusCode((int)HttpStatusCode.OK, new Response()
                    {
                        Message = LearnerAppConstants.SOCIAL_LOGIN_SUCCESS_MESSAGE,
                        Data = userDetails
                    });
                }
                else
                {
                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, "SocialValidation"), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// User Registration
        /// </summary>
        /// <param name="userRegisteration"></param>
        /// <returns></returns>
        [HttpPost("UserRegistration")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> UserRegistration([FromBody] UserRegisteration userRegisteration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!await IsValidUser(userRegisteration.Email))
                    {
                        List<string> errorMessages = new();
                        int result = await _userService.UserRegistration(userRegisteration, errorMessages);

                        if (errorMessages.Any())
                        {
                            var errMsg = errorMessages.FirstOrDefault();
                            logger.LogDebug(errMsg);

                            return StatusCode((int)HttpStatusCode.BadRequest, new Response()
                            {
                                Message = errMsg
                            });
                        }
                        else
                        {
                            logger.LogDebug(LearnerAppConstants.REGISTRATION_SUCCESS_MESSAGE);
                            return StatusCode((int)HttpStatusCode.OK, new Response() { Message = LearnerAppConstants.REGISTRATION_SUCCESS_MESSAGE });
                        }
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.USER_ALREADY_EXISTS);
                        return StatusCode((int)HttpStatusCode.Found, new Response()
                        {
                            Message = LearnerAppConstants.USER_ALREADY_EXISTS
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
                logger.LogError(string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(UserRegistration)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });

            }
        }

        /// <summary>
        /// User Logout
        /// </summary>
        /// <param name="logout"></param>
        /// <returns></returns>
        [HttpPost("UserLogout")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> UserLogout([FromBody] Logout logout)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> errorMessages = new();
                    int result = await _userService.UserLogout(logout, errorMessages);
                    if (errorMessages.Any())
                    {
                        var errMsg = errorMessages.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new Response()
                        {
                            Message = errMsg
                        });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.LOGOUT_SUCCESSFUL);
                        return StatusCode((int)HttpStatusCode.OK, new Response() { Message = LearnerAppConstants.LOGOUT_SUCCESSFUL });
                    }
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.INVALID_SESSIONID);
                    return StatusCode((int)HttpStatusCode.NotFound, new Response()
                    {
                        Message = LearnerAppConstants.INVALID_SESSIONID
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(UserLogout)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Send OTP
        /// </summary>
        /// <param name="sendOTP"></param>
        /// <returns></returns>
        [HttpPost("SendOTP")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> SendOTP(SendOTP sendOTP)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (await IsValidUser(sendOTP.EmailID))
                    {
                        logger.LogInfo(LearnerAppConstants.USER_ALREADY_REGISTERED);
                        return StatusCode((int)HttpStatusCode.BadRequest, new Response()
                        {
                            Message = LearnerAppConstants.USER_ALREADY_REGISTERED
                        });
                    }
                    else
                    {
                        List<string> errorMessages = new();
                        var response = await _userService.SendOTP(sendOTP, errorMessages);
                        if (errorMessages.Any())
                        {
                            var errMsg = errorMessages.FirstOrDefault();
                            logger.LogDebug(errMsg);

                            return StatusCode((int)HttpStatusCode.BadRequest, new Response()
                            {
                                Message = errMsg
                            });
                        }
                        else
                        {
                            logger.LogDebug(LearnerAppConstants.VERIFICATION_CODE_SENT);
                            return StatusCode((int)HttpStatusCode.OK, new Response() { Message = LearnerAppConstants.VERIFICATION_CODE_SENT });
                        }
                    }
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.INVALID_EmailID);
                    return StatusCode((int)HttpStatusCode.NotFound, new Response()
                    {
                        Message = LearnerAppConstants.INVALID_EmailID
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(SendOTP)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });

            }
        }

        /// <summary>
        /// Verify OTP
        /// </summary>
        /// <param name="registrationOTP"></param>
        /// <returns></returns>
        [HttpPost("VerifyOTP")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> VerifyOTP([FromBody] RegistrationOTP registrationOTP)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!await IsValidUser(registrationOTP.Email))
                    {
                        List<string> errorMessages = new();
                        int result = await _userService.VerifyOTP(registrationOTP, errorMessages);

                        if (errorMessages.Any())
                        {
                            var errMsg = errorMessages.FirstOrDefault();
                            logger.LogDebug(errMsg);

                            return StatusCode((int)HttpStatusCode.BadRequest, new Response()
                            {
                                Message = errMsg
                            });
                        }
                        else
                        {
                            logger.LogDebug(LearnerAppConstants.OTP_VERIFICATION_SUCCESS);
                            return StatusCode((int)HttpStatusCode.OK, new Response() { Message = LearnerAppConstants.OTP_VERIFICATION_SUCCESS });
                        }
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.USER_ALREADY_EXISTS);
                        return StatusCode((int)HttpStatusCode.Found, new Response()
                        {
                            Message = LearnerAppConstants.INVALID_EmailID
                        });
                    }
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.INVALID_OTP);
                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(VerifyOTP)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /// <summary>
        /// UpdatePassword
        /// </summary>
        /// <param name="passwordRecoveryReset"></param>
        /// <returns></returns>
        [HttpPost("UpdatePassword")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordRecoveryReset passwordRecoveryReset)
        {
            string encryptedText = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> errorMessages = new();
                    int result = await _userService.UpdatePassword(passwordRecoveryReset, errorMessages);
                    if (errorMessages.Any())
                    {
                        var errMsg = errorMessages.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new Response()
                        {
                            Message = errMsg
                        });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.PASSWORD_UPDATE_SUCCESS);
                        return StatusCode((int)HttpStatusCode.OK, new Response() { Message = LearnerAppConstants.PASSWORD_UPDATE_SUCCESS });
                    }

                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.USERID_PASSWORD_REQUIRED);
                    return StatusCode((int)HttpStatusCode.NotFound, new Response()
                    {
                        Message = LearnerAppConstants.USERID_PASSWORD_REQUIRED
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(UserRegistration)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// SSOResponse
        /// </summary>
        /// <param name="SAMLResponse"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("ssoresponse/{id:int}")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> SSOResponse([FromForm] string SAMLResponse, int id)
        {
            try
            {
                string ClientIP = ClientIPAddress(HttpContext);

                var result = await _userService.SAMLResponse(SAMLResponse, id, ClientIP);
                if (result.IsNullOrEmptyOrWhiteSpace())
                {
                    string returnUrl = await _userService.GetSamlRedirectURL(LearnerAppConstants.SSO_INVALID_RESPONSE);
                    logger.Debug(returnUrl);
                    return Redirect(returnUrl);
                }
                else
                {
                    logger.Debug(result);
                    return Redirect(result);

                }

            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(SSOResponse)), ex);
                string returnUrl = await _userService.GetSamlRedirectURL(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE);
                return Redirect(returnUrl);
            }
        }


        /// <summary>
        /// Validating Token
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("TokenValidation")]
        public async Task<IActionResult> TokenValidation([FromBody] UserDetails userDetails)
        {
            try
            {
                List<string> errorMessages = new();
                bool isValid = await _userService.TokenValidation(userDetails.TokenId, errorMessages);
                if (errorMessages.Any())
                {
                    var errMsg = errorMessages.FirstOrDefault();
                    logger.LogDebug(errMsg);
                    return StatusCode((int)HttpStatusCode.Unauthorized, new Response()
                    {
                        Message = errMsg
                    });
                }
                else
                {
                    logger.Info(LearnerAppConstants.VALID_TOKEN);
                    return StatusCode((int)HttpStatusCode.OK, new Response() { Message = LearnerAppConstants.VALID_TOKEN });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(TokenValidation)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        /// <summary>
        /// ResetUserPassword
        /// </summary>
        /// <param name="resetUser">Username </param>
        /// <returns></returns>
        [HttpPost("ChangePassword")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        public async Task<IActionResult> ResetUserPassword([FromBody] ResetUserPassword resetUser)
        {
            try
            {
                List<string> errorMessages = new List<string>();
                var user = await _userService.ResetUserPassword(resetUser, errorMessages);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new Response() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new Response() { Message = LearnerAppConstants.PASSWORD_CHANGED_SUCCESSFULLY });
                }

            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, "ForgotPassword"), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        [HttpPut("UpdateAddress")]
        [SwaggerResponse(200, "Ok", typeof(ReturnResponse<SubcategoriesForLearningPath>))]
        [SwaggerResponse(400, "Bad Request", typeof(ReturnResponse<SubcategoriesForLearningPath>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<SubcategoriesForLearningPath>))]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddress updateAddress)
        {
            try
            {
                List<string> errorMessages = new List<string>();
                var user = await _userService.UpdateAddress(updateAddress, errorMessages);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<SubcategoriesForLearningPath>() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<SubcategoriesForLearningPath>() { Message = LearnerAppConstants.SuccessUpdate });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<SubcategoriesForLearningPath>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        /// <summary>
        /// Igetit 1.0 Authentication Service
        /// </summary>
        /// <param name="igetitAuthService">SessionID </param>
        /// <returns></returns>
        [HttpPost("AuthIgetit")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<Response>))]
        public async Task<IActionResult> IgetitAuthentication([FromBody] IgetitAuthService igetitAuthService)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> errorMessages = new List<string>();
                    var user = await _userService.AuthenticationService(igetitAuthService, errorMessages);

                    if (errorMessages.Any())
                    {
                        return StatusCode((int)HttpStatusCode.BadRequest, new Response() { Message = errorMessages.FirstOrDefault() });
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.OK, new Response() { Message = LearnerAppConstants.USER_AUTHENTICATED, Data = user });
                    }
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.MODEL_VALIDATION_FAILED);
                    return StatusCode((int)HttpStatusCode.BadRequest, new Response() { Message = LearnerAppConstants.MODEL_VALIDATION_FAILED });
                }

            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, "AuthService"), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        #endregion
        private string GetMD5Hash(string input)
        {
            var MD5CookieSalt = "32628658f18adae1beb28a83eb6f8d9f3c0a25fcc96f184157ab0e3";
            input = input + MD5CookieSalt;
            StringBuilder sBuilder = new StringBuilder();
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash. 
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Loop through each byte of the hashed data  
                // and format each one as a hexadecimal string. 
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
            }
            return sBuilder.ToString();
        }
    }
}
