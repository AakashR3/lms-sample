using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : BaseController
    {
        private readonly ISubscriptionsService _subscriptionService;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public SubscriptionsController(ISubscriptionsService subscriptionsService)
        {

            _subscriptionService = subscriptionsService ?? throw new ArgumentNullException(nameof(ISubscriptionsService));
        }

        #region GET
        /// <summary>
        /// To get the user's subscriptions detail
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet("Subscriptions/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<UserSubscription>))]
        [SwaggerResponse(400, "Bad Request", typeof(UserSubscription))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<UserSubscription>))]
        public async Task<IActionResult> Subscription(int UserID)
        {
            try
            {
                List<string> errorMessages = new();
                var result = await _subscriptionService.GetUserSubscriptionsAsync(UserID, errorMessages);
                if (errorMessages.Any())
                {
                    logger.LogDebug(LearnerAppConstants.SUBSCRIPTION_FOUND);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<UserSubscription>() { Message = LearnerAppConstants.NO_SUBSCRIPTION });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NO_SUBSCRIPTION);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<UserSubscription>>() { Message = LearnerAppConstants.SUBSCRIPTION_FOUND, Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<UserSubscription>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        /// <summary>
        /// To get the user's Purchased History detail
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet("PurchasedHistory/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<UsersPurchasedHistory>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<UsersPurchasedHistory>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<UsersPurchasedHistory>))]
        public async Task<IActionResult> PurchasedHistory(int UserID, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                List<string> errorMessages = new();
                var result = await _subscriptionService.GetUserPurchasedHistoryAsync(UserID, errorMessages);

                if (result.Any())
                {
                    string message = string.Empty;
                    int totalRecords = result.Count();
                    var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);

                    UsersPurchasedHistoryParent usersPurchasedHistoryParent = new()
                    {
                        PageNumber = PageNumber,
                        PageSize = PageSize,
                        TotalItems = totalRecords,
                        TotalPages = validFilter.TotalPages
                    };

                    if (result != null && result.Any())
                    {
                        result = result
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();
                        usersPurchasedHistoryParent.usersPurchasedHistories = result;
                        message = LearnerAppConstants.SUCCESSMESSAGE;
                    }
                    else
                        message = LearnerAppConstants.PURCHASE_HISTORY_NOT_FOUND;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<UsersPurchasedHistoryParent>()
                    {
                        Message = message,
                        Data = usersPurchasedHistoryParent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.PURCHASE_HISTORY_NOT_FOUND);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<UsersPurchasedHistoryParent>() { Message = LearnerAppConstants.PURCHASE_HISTORY_NOT_FOUND, Data = { } });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<UserSubscription>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("AvailableSubscription/{CountryCode}/{CategoryID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<AvailableSubscription>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<AvailableSubscription>))]
        public async Task<IActionResult> AvailableSubscription(string CountryCode, int CategoryID)
        {
            try
            {
                List<string> errorMessages = new();
                var result = await _subscriptionService.GetAvailableSubscription(CountryCode, CategoryID, errorMessages);
                if (errorMessages.Any())
                {
                    logger.LogDebug(LearnerAppConstants.NO_Available_SUBSCRIPTION);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<AvailableSubscription>>() { Message = LearnerAppConstants.NO_Available_SUBSCRIPTION });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<AvailableSubscription>>() { Message = LearnerAppConstants.Success, Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<IEnumerable<AvailableSubscription>>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("ProfessionalBundle")]
        [SwaggerResponse(200, "Ok", typeof(ProfessionalBundle))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ProfessionalBundle))]
        public async Task<IActionResult> ProfessionalBundle()
        {
            try
            {
                string ClientIP = Common.ClientIPAddress(HttpContext);
                logger.LogDebug("ProfessionalBundle Controller: " + ClientIP);

                List<string> errorMessages = new();
                var result = await _subscriptionService.GetProfessionalBundle(ClientIP, errorMessages);
                if (errorMessages.Any())
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<ProfessionalBundle>() { Message = LearnerAppConstants.NoRecordsFound });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<ProfessionalBundle>() { Message = LearnerAppConstants.Success, Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<ProfessionalBundle>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        [HttpGet("wwwProfessionalBundle/{CountryCode}")]
        [SwaggerResponse(200, "Ok", typeof(ProfessionalBundle))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ProfessionalBundle))]
        public async Task<IActionResult> wwwProfessionalBundle(string CountryCode)
        {
            try
            {
                List<string> errorMessages = new();
                var result = await _subscriptionService.GetwwwProfessionalBundle(CountryCode, errorMessages);
                if (errorMessages.Any())
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<ProfessionalBundle>() { Message = LearnerAppConstants.NoRecordsFound });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<ProfessionalBundle>() { Message = LearnerAppConstants.Success, Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<ProfessionalBundle>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /*[HttpGet("SubscriptionDetail/{SubscriptionID}")]
        [SwaggerResponse(200, "Ok", typeof(SubscriptionDetail))]
        [SwaggerResponse(500, "Internal Server Error", typeof(SubscriptionDetail))]
        public async Task<IActionResult> GetSubscriptionDetail(string SubscriptionID)
        {
            try
            {
                List<string> errorMessages = new();
                var result = await _subscriptionService.GetSubscriptionDetail(SubscriptionID, errorMessages);
                if (errorMessages.Any())
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<SubscriptionDetail>() { Message = LearnerAppConstants.NoRecordsFound, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<SubscriptionDetail>() { Message = LearnerAppConstants.Success, Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<SubscriptionDetail>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }*/
        [HttpGet("SubscriptionDetail/{SubscriptionID}/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(SubscriptionDetailParent))]
        [SwaggerResponse(500, "Internal Server Error", typeof(SubscriptionDetailParent))]
        public async Task<IActionResult> GetSubscriptionDetail(string SubscriptionID, int UserID, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                List<string> errorMessages = new();
                var result = await _subscriptionService.GetSubscriptionDetail(SubscriptionID, UserID, errorMessages);
                string message = string.Empty;
                int Course_totalRecords = result.skillAdvisor_Courses.Count();
                int Assessment_totalRecords = result.SkillAdvisor_Assessments.Count();
                var validFilter = new SubsriptionPaginationFilter(PageNumber, PageSize, Course_totalRecords, Assessment_totalRecords);

                SubscriptionDetailParent subscriptionDetailParent = new()
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    CourseTotalItems = Course_totalRecords,
                    CourseTotalPages = validFilter.CourseTotalPages,
                    AssessmentTotalItems=Assessment_totalRecords,
                    AssessmentTotalPages=validFilter.AssessmentTotalPages
                };

                if (result!=null)
                {

                    if (result.skillAdvisor_Courses.Count() > 0)
                    {
                        var Course_result = result.skillAdvisor_Courses
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();

                        var Assess_result = result.SkillAdvisor_Assessments
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();

                        subscriptionDetailParent.skillAdvisor_Courses = Course_result;
                        subscriptionDetailParent.SkillAdvisor_Assessments = Assess_result;
                        subscriptionDetailParent.skillAdvisor_Subscription = result.skillAdvisor_Subscription;
                        message = LearnerAppConstants.Success;
                    }
                    else
                        message = LearnerAppConstants.Failure;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<SubscriptionDetailParent>()
                    {
                        Message = message,
                        Data = subscriptionDetailParent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Failure);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<SubscriptionDetailParent>() { Message = LearnerAppConstants.Failure, Data = subscriptionDetailParent });
                }

                /*if (errorMessages.Any())
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<SubscriptionDetailParent>() { Message = LearnerAppConstants.NoRecordsFound, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<SubscriptionDetailParent>() { Message = LearnerAppConstants.Success, Data = result });
                }*/
            }
            catch (Exception ex)
            {
                logger.LogError(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<SubscriptionDetailParent>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        [NonAction]
        public string ClientIPAddress(HttpContext context)
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

                if (PublidIPAddress.Contains(':') == true)
                {
                    PublidIPAddress = PublidIPAddress.Split(':')[0];
                }

                return PublidIPAddress;
            }
            catch
            {
                return "";
            }
        }


        #endregion


        #region POST

        [HttpPost("recurly")]
        [SwaggerResponse(200, "Ok", typeof(ReturnResponse<RecurlySubscriptionResponse>))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> SubscribeAsync(RecurlyPurchaseRequest request)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    logger.LogError(LearnerAppConstants.MODEL_VALIDATION_FAILED, new ApiException("Recurly purchase model validation failed"));
                    return await PopulateModelErrorsAsync();
                }
                List<string> errorMessage = new();
                var response = await _subscriptionService.SubscribeWithRecurlyAsync(request, errorMessage);

                return Ok(new ReturnResponse<RecurlySubscriptionResponse>() { Message = LearnerAppConstants.SUBSCRIPTION_FOUND, Data = response });

            }
            catch (Exception ex)
            {
                if (ex.Source.ToUpper().Equals("RECURLY"))
                {
                    if (LearnerAppConstants.RECURLY_INVALID_COUPON_CODE.Contains(ex.Message.ToString()))
                    {
                        logger.LogError(string.Format(LearnerAppConstants.INVALID_COUPON_CODE, nameof(RecurlyCardCheckout)), ex);
                        return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Response>() { Message = LearnerAppConstants.INVALID_COUPON_CODE });
                    }
                    else if (LearnerAppConstants.RECURLY_GENERAL_EXCEPTION.Contains(ex.Message.ToString()))
                    {
                        logger.LogError(string.Format(LearnerAppConstants.GENERAL_EXCEPTION, nameof(RecurlyCardCheckout)), ex);
                        return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Response>() { Message = LearnerAppConstants.GENERAL_EXCEPTION });
                    }
                    else if (LearnerAppConstants.RECURLY_ADDRESS_CITY_COUNTRY_EMPTY.Contains(ex.Message.ToString()))
                    {
                        logger.LogError(string.Format(LearnerAppConstants.ADDRESS_CITY_COUNTRY_EMPTY, nameof(RecurlyCardCheckout)), ex);
                        return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Response>() { Message = LearnerAppConstants.ADDRESS_CITY_COUNTRY_EMPTY });
                    }
                    else
                    {
                        logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(RecurlyCardCheckout)), ex);
                        return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Response>() { Message = ex.Message.ToString() });
                    }
                }
                else
                {
                    logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(RecurlyCardCheckout)), ex);
                    return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Response>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });

                }
            }
        }


        /// <summary>
        ///Recurly Multiple Subscription
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost("MultiSubscriptionRecurly")]
        [SwaggerResponse(200, "Ok", typeof(ReturnResponse<RecurlySubscriptionResponse>))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> MultiSubscriptionWithRecurlyAsync([FromBody] RecurlyPurchaseRequest request)
        {
            try
            {
                if (request == null)
                {
                    logger.LogDebug(LearnerAppConstants.MODEL_VALIDATION_FAILED);
                    return StatusCode((int)HttpStatusCode.BadRequest, new Response()
                    {
                        Message = LearnerAppConstants.MODEL_VALIDATION_FAILED
                    });
                }
                else
                {
                    List<string> errorMessages = new();
                    var response = await _subscriptionService.MultiSubscriptionWithRecurlyAsync(request, errorMessages);
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
                        return Ok(new ReturnResponse<RecurlySubscriptionResponse>() { Message = LearnerAppConstants.SUBSCRIPTION_FOUND, Data = response });

                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Source.ToUpper().Equals("RECURLY"))
                {
                    logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(RecurlyCardCheckout)), ex);
                    return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Response>() { Message = ex.Message.ToString() });

                }
                else
                {
                    logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(RecurlyCardCheckout)), ex);
                    return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Response>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });

                }
            }
        }


        /// <summary>
        ///Download Invoice
        /// </summary>
        /// <param name="SubscriptionID"></param>
        /// <returns></returns>

        [HttpPost("DownloadInvoice")]
        [SwaggerResponse(200, "Ok", typeof(ReturnResponse<InvoiceResponse>))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> DownloadInvoice(string SubscriptionID)
        {
            try
            {
                List<string> errorMessages = new();
                var response = await _subscriptionService.DownloadInvoice(SubscriptionID, errorMessages);
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
                    if (SubscriptionID.ToLower().StartsWith("sub_") == true)
                    {
                        return Ok(new ReturnResponse<InvoiceResponse>() { Message = LearnerAppConstants.INVOICE_FOUND, Data = response });
                    }
                    else
                    {
                        //if (System.IO.File.Exists(response.FilePath))
                        //{
                        //    return File(System.IO.File.OpenRead(response.FilePath), "application/octet-stream", Path.GetFileName(response.FilePath));
                        //}

                        return Ok(new ReturnResponse<InvoiceResponse>() { Message = LearnerAppConstants.INVOICE_FOUND, Data = response });
                    }

                }
            }
            catch (Exception ex)
            {
                if (ex.Source.ToUpper().Equals("RECURLY"))
                {
                    logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(RecurlyCardCheckout)), ex);
                    return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Response>() { Message = ex.Message.ToString() });

                }
                else
                {
                    logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(RecurlyCardCheckout)), ex);
                    return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Response>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });

                }
            }
        }


        #endregion
    }
}
