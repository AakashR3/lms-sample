using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NLog.Fluent;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics;
using System.Net;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.AccountManagement;
using Tata.IGetIT.Learner.Service.Helpers;
using Message = Tata.IGetIT.Learner.Service.Helpers.Message;

namespace Tata.IGetIT.Learner.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : BaseController
    {
        private readonly ICommonService _commonService;
        private readonly IEmailSender _emailSender;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public CommonController(ICommonService commonService, IEmailSender emailSender)
        {
            _commonService = commonService ?? throw new ArgumentNullException(nameof(ICommonService));
            _emailSender = emailSender;
        }

        #region GET

        /// <summary>
        /// To load all the master categories
        /// </summary>
        /// <returns></returns>
        [HttpGet("Categories/{CategoryID}")]
        [SwaggerResponse(200, "Ok", typeof(CategoryDetails))]
        [SwaggerResponse(500, "Internal Server Error", typeof(CategoryDetails))]
        public async Task<IActionResult> GetCategories(int CategoryID)
        {
            try
            {
                List<string> errorMsg = new();
                var result = await _commonService.GetCategories(CategoryID);
                if (result != null)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<CategoryDetails>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<CategoryDetails>() { Message = LearnerAppConstants.NoRecordsFound, Data = result });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<CategoryDetails>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /// <summary>
        /// To all sub categories based on a category ID
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        [HttpGet("SubCategoriesBasedOnCategoryID/{CategoryID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<GetSubCategoriesBasedOnCategoryID>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<GetSubCategoriesBasedOnCategoryID>))]
        public async Task<IActionResult> GetSubCategoriesBasedOnCategoryID(int CategoryID)
        {
            try
            {
                List<string> errorMsg = new();
                var result = await _commonService.GetSubCategoriesBasedOnCategoryID(CategoryID);
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<GetSubCategoriesBasedOnCategoryID>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<GetSubCategoriesBasedOnCategoryID>>() { Message = LearnerAppConstants.NoRecordsFound, Data = result });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<IEnumerable<GetSubCategoriesBasedOnCategoryID>>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /// <summary>
        /// To all topics based on a category ID
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        [HttpGet("TopicsBasedOnCategoryID/{CategoryID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<GetTopicsBasedOnCategoryID>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<GetTopicsBasedOnCategoryID>))]
        public async Task<IActionResult> GetTopicsBasedOnCategoryID(int CategoryID)
        {
            try
            {
                List<string> errorMsg = new();
                var result = await _commonService.GetTopicsBasedOnCategoryID(CategoryID);
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<GetTopicsBasedOnCategoryID>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<GetTopicsBasedOnCategoryID>>() { Message = LearnerAppConstants.NoRecordsFound, Data = result });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<IEnumerable<GetTopicsBasedOnCategoryID>>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /// <summary>
        /// To load plan details, pass PlanCode=-1 to retrieve all records or specify required. 
        /// </summary>
        /// <param name="PlanCode"></param>
        /// <returns></returns>
        [HttpGet("Plans/{PlanCode}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<Plans>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<Plans>))]
        public async Task<IActionResult> GetPlans(int PlanCode)
        {
            try
            {
                List<string> errorMsg = new();
                var result = await _commonService.GetAllPlans(PlanCode);
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<Plans>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<Plans>>() { Message = LearnerAppConstants.NoRecordsFound, Data = result });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<IEnumerable<Plans>>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// To load individual plan details
        /// </summary>
        /// <returns></returns>
        [HttpGet("IndividualPlans")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<IndividualPlans>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<IndividualPlans>))]
        public async Task<IActionResult> GetIndividualPlans()
        {
            try
            {
                string ClientIP = Common.ClientIPAddress(HttpContext);

                List<string> errorMsg = new();
                var result = await _commonService.GetIndividualPlans(ClientIP);

                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<IndividualPlans>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<IndividualPlans>>() { Message = LearnerAppConstants.NoRecordsFound, Data = result });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<IEnumerable<IndividualPlans>>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        /// <summary>
        /// To load individual plan details
        /// </summary>
        /// <returns></returns>
        [HttpGet("wwwIndividualPlans/{CountryCode}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<IndividualPlans>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<IndividualPlans>))]
        public async Task<IActionResult> GetwwwIndividualPlans(string CountryCode)
        {
            try
            {
                List<string> errorMsg = new();
                var result = await _commonService.GetwwwIndividualPlans(CountryCode);

                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<IndividualPlans>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<IndividualPlans>>() { Message = LearnerAppConstants.NoRecordsFound, Data = result });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<IEnumerable<IndividualPlans>>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        [HttpGet("CourseTitles/{CategoryID}/{SubCategoryID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<CourseTitles>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<CourseTitles>))]
        public async Task<IActionResult> GetCourseTitles(int CategoryID, int SubCategoryID)
        {
            try
            {
                List<string> errorMsg = new();
                var result = await _commonService.GetCourseTitles(CategoryID,SubCategoryID);

                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<CourseTitles>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<CourseTitles>>() { Message = LearnerAppConstants.NoRecordsFound, Data = result });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<IEnumerable<CourseTitles>>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }



        #endregion

        #region POST
        /*[HttpPost("SendEmail")]
        [SwaggerResponse(200, "Ok", typeof(ReturnResponse<Common_EmailInfo>))]
        [SwaggerResponse(400, "Bad Request", typeof(ReturnResponse<Common_EmailInfo>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ReturnResponse<Common_EmailInfo>))]
        public async Task<IActionResult> SendEmail([FromBody] Common_EmailInfo common_EmailInfo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _commonService.SendEmailAsync(common_EmailInfo);
                    if (result)
                    {
                        logger.LogDebug(LearnerAppConstants.Success);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<Common_EmailInfo>() { Message = LearnerAppConstants.Success });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.Failure);
                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<Common_EmailInfo>() { Message = LearnerAppConstants.Failure });
                    }
                }
                else
                {
                    logger.LogDetails(new LogDetails(LogType.Error)
                    {
                        Message = LearnerAppConstants.MODEL_VALIDATION_FAILED,
                        AppUrl = Request.GetDisplayUrl(),
                        RequestParam = common_EmailInfo
                    });

                    return await PopulateModelErrorsAsync();
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Common_EmailInfo>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }*/
        [HttpPost("SendEmail")]
        [SwaggerResponse(200, "Ok", typeof(ReturnResponse<Common_EmailInfo>))]
        [SwaggerResponse(400, "Bad Request", typeof(ReturnResponse<Common_EmailInfo>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ReturnResponse<Common_EmailInfo>))]
        public async Task<IActionResult> SendEmail()
        {
            try
            {
                Common_EmailInfo common_EmailInfo = new();
                

                if (!HttpContext.Request.Form["To"].ToString().IsNullOrEmptyOrWhiteSpace() 
                    && !HttpContext.Request.Form["Content"].ToString().IsNullOrEmptyOrWhiteSpace() 
                    && !HttpContext.Request.Form["Subject"].ToString().IsNullOrEmptyOrWhiteSpace())
                {

                    common_EmailInfo.To = HttpContext.Request.Form["To"].ToString().Split(',').ToArray();
                    common_EmailInfo.Subject = HttpContext.Request.Form["Subject"].ToString();
                    common_EmailInfo.Content = HttpContext.Request.Form["Content"].ToString();


                    var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();
                    var message = new Message( common_EmailInfo.To , common_EmailInfo.Subject,common_EmailInfo.Content, files);
                    //var message = new Message(new string[] { "sathiyamoorthy@codincity.com" }, "Test mail with Attachments from Sathiya", "This is the content from our mail with attachments.", files);
                    await _emailSender.SendEmailAsync(message);

                    logger.LogDebug(LearnerAppConstants.OK);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<Common_EmailInfo>() { Message = LearnerAppConstants.Success, Data = common_EmailInfo });

                }
                else
                {
                    logger.LogDetails(new LogDetails(LogType.Error)
                    {
                        Message = LearnerAppConstants.MODEL_VALIDATION_FAILED,
                        AppUrl = Request.GetDisplayUrl(),
                        RequestParam = common_EmailInfo
                    });

                    logger.LogDebug(LearnerAppConstants.BAD_REQUEST);
                    return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<Common_EmailInfo>() { Message = LearnerAppConstants.NoRecordsFound, Data = common_EmailInfo });

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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Common_EmailInfo>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        #endregion
    }
}
