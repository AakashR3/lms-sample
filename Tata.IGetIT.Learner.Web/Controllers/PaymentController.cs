using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Service.Interfaces;
using Tata.IGetIT.Learner.Repository.Models;
using Microsoft.AspNetCore.Authorization;

namespace Tata.IGetIT.Learner.Web.Controllers
{
    /// <summary>
    /// Payment controller to handle all payment process for Razorpay and Recurly
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(IPaymentService));
        }

        /// <summary>
        /// Get Currency List
        /// </summary>
        /// <param></param>
        /// <returns>Currency List</returns>

        //[Authorize]
        [HttpGet("Currency")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<Currency>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<Currency>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<Currency>))]
        public async Task<IActionResult> GetCurrency()
        {
            try
            {
                var result = await _paymentService.CurrencyList();
                if (result.Count() > 0)
                {
                    logger.LogDebug(LearnerAppConstants.CURRENCY_FOUND);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<Currency>>() { Message = LearnerAppConstants.CURRENCY_FOUND, Data = result });

                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.CURRENCY_NOT_FOUND);
                    return StatusCode((int)HttpStatusCode.NotFound, new ReturnResponse<Currency>() { Message = LearnerAppConstants.CURRENCY_NOT_FOUND });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetCurrency)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Currency>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Razorpay Checkout
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>

        //[Authorize]
        [HttpPost("RazorpayCheckout")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> RazorpayCheckout([FromBody] PaymentInitiate payment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RazorpayModel model = new();
                    List<string> errorMessage = new();
                    var result = await _paymentService.RazorpayCheckout(payment, errorMessage);

                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new PaymentResponse()
                        {
                            Message = errMsg
                        });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.RAZORPAY_PAYMENT_INITIATED);
                        return StatusCode((int)HttpStatusCode.OK, new PaymentResponse()
                        {
                            Message = LearnerAppConstants.RAZORPAY_PAYMENT_INITIATED,
                            Data = result
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
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(RazorpayCheckout)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Currency>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        /// <summary>
        /// Razorpay Checkout Response
        /// </summary>
        /// <param name="razorpayResponse"></param>
        /// <returns></returns>

        //[Authorize]
        [HttpPost("RazorpayResponse")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> RazorpayResponse([FromBody] RazorpayResponse razorpayResponse)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RazorpayModel order = new();
                    List<string> errorMessage = new();
                    var result = await _paymentService.RazorpayResponse(razorpayResponse, errorMessage);

                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new PaymentResponse()
                        {
                            Message = errMsg
                        });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.RAZORPAY_PAYMENT_SUCCESS);
                        return StatusCode((int)HttpStatusCode.OK, new PaymentResponse()
                        {
                            Message = LearnerAppConstants.RAZORPAY_PAYMENT_SUCCESS
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
                logger.LogError(string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(RazorpayResponse)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response() { Message = ex.Message.ToString() });
            }
        }


        /// <summary>
        /// Cancel subscription
        /// </summary>
        /// <param name="cancelSubscription"></param>
        /// <returns></returns>

        //[Authorize]
        [HttpPost("CancelSubscription")]
        [SwaggerResponse(200, "Ok", typeof(PaymentResponse))]
        [SwaggerResponse(400, "Bad Request", typeof(PaymentResponse))]
        [SwaggerResponse(500, "Internal Server Error", typeof(PaymentResponse))]
        public async Task<IActionResult> CancelSubscription([FromBody] CancelSubscription cancelSubscription)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> errorMessage = new();
                    var result = await _paymentService.CancelSubscription(cancelSubscription, errorMessage);

                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new PaymentResponse()
                        {
                            Message = errMsg,
                            Data = { }
                        });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.RAZORPAY_SUBSCRIPTION_CANCELLED);
                        return StatusCode((int)HttpStatusCode.OK, new PaymentResponse()
                        {
                            Message = LearnerAppConstants.RAZORPAY_SUBSCRIPTION_CANCELLED,
                            Data = { }
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
                logger.LogError(string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(CancelSubscription)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        #region Recurly Payment Process

        /// <summary>
        /// Recurly Checkout
        /// </summary>
        /// <param name="recurlyCardCheckout"></param>
        /// <returns></returns>

        //[Authorize]
        [HttpPost("RecurlyCardCheckout")]
        [SwaggerResponse(200, "Ok", typeof(RecurlyCardCheckoutResponse))]
        [SwaggerResponse(400, "Bad Request", typeof(PaymentResponse))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> RecurlyCardCheckout([FromBody] RecurlyCardCheckout recurlyCardCheckout)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    List<string> errorMessage = new();
                    var result = await _paymentService.RecurlyCardCheckout(recurlyCardCheckout, errorMessage);

                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new PaymentResponse()
                        {
                            Message = errMsg
                        });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.PAYMENT_INITIATED);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<RecurlyCardCheckoutResponse>()
                        {
                            Message = LearnerAppConstants.PAYMENT_INITIATED,
                            Data = result
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
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(RecurlyCardCheckout)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Response>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        #endregion


    }
}
