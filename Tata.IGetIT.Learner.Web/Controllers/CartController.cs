using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Tata.IGetIT.Learner.Web.Controllers
{
    /// <summary>
    /// Cart controller hold cart information
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public CartController(ICartService cartService)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(ICartService));
        }
        #region GET

        /// <summary>
        /// Get Cart Items
        /// </summary>
        /// <param>UserID</param>
        /// <returns>Currency List</returns>

        //[Authorize]
        [HttpGet("CartList/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<Cart>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<Cart>))]
        public async Task<IActionResult> CartList(int UserID)
        {
            try
            {
                var result = await _cartService.CartList(UserID);
                if (result.Count() > 0)
                {
                    logger.LogDebug(LearnerAppConstants.CART_ITEMS_FOUND);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<Cart>>() { Message = LearnerAppConstants.CART_ITEMS_FOUND, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NO_CART_ITEMS_FOUND);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<Cart>() { Message = LearnerAppConstants.NO_CART_ITEMS_FOUND, Data = { } });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(CartList)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Cart>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// To get subscription plan details
        /// </summary>
        /// <param name="SubscriptionID"></param>
        /// <param name="CourseType"></param>
        /// <returns></returns>
        [HttpGet("Plans/{SubscriptionID}/{CourseType}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<PlanInfo>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<PlanInfo>))]
        public async Task<IActionResult> Plans(int SubscriptionID, String CourseType)
        {
            try
            {
                var result = await _cartService.GetPlanSubscriptionInfo(SubscriptionID, CourseType);
                if (result.Count() > 0)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<PlanInfo>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Failure);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<PlanInfo>() { Message = LearnerAppConstants.Failure, Data = { } });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(CartList)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<PlanInfo>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        /// <summary>
        /// Get Country List
        /// </summary>
        /// <param></param>
        /// <returns>Country List</returns>

        //[Authorize]
        [HttpGet("Countries")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<Countries>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<Countries>))]
        public async Task<IActionResult> CountryList()
        {
            try
            {
                var result = await _cartService.CountryList();
                if (result.Count() > 0)
                {
                    logger.LogDebug(LearnerAppConstants.COUNTRIES_FOUND);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<Countries>>() { Message = LearnerAppConstants.COUNTRIES_FOUND, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.COUNTRY_NOT_FOUND);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<Countries>() { Message = LearnerAppConstants.COUNTRY_NOT_FOUND });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(CountryList)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Countries>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        /// <summary>
        /// Fetch User Shipping Info
        /// </summary>
        /// <param>UserID</param>
        /// <returns>Currency List</returns>

        //[Authorize]
        [HttpGet("ShippingInfo/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(ShippingInfo))]
        [SwaggerResponse(400, "Bad Request", typeof(ShippingInfo))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ShippingInfo))]
        public async Task<IActionResult> ShippingInfo(int UserID)
        {
            try
            {
                List<string> errorMessages = new();
                var result = await _cartService.ShippingInfo(UserID, errorMessages);

                if (errorMessages.Any())
                {
                    logger.LogDebug(errorMessages.FirstOrDefault());
                    return StatusCode((int)HttpStatusCode.NotFound, new ReturnResponse<ShippingInfo>() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.SHIPPING_INFORMATION_FOUND);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<ShippingInfo>() { Message = LearnerAppConstants.SHIPPING_INFORMATION_FOUND, Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(CartList)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<ShippingInfo>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        #endregion

        #region POST
        /// <summary>
        /// To add a cart item
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        [HttpPost("AddToCart")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> AddToCart([FromBody] Cart cart)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> errorMessage = new();
                    var result = await _cartService.AddToCart(cart, errorMessage);

                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<Cart>() { Message = errMsg, Data = cart });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.CART_ITEM_ADDED);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<Cart>()
                        {
                            Message = LearnerAppConstants.CART_ITEM_ADDED
                        });
                    }
                }
                else
                {
                    logger.LogDetails(new LogDetails(LogType.Error)
                    {
                        Message = LearnerAppConstants.MODEL_VALIDATION_FAILED,
                        AppUrl = Request.GetDisplayUrl(),
                        RequestParam = cart
                    });

                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(AddToCart)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });

                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Cart>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, Data = cart });
            }
        }



        /// <summary>
        /// Trail Account Validation
        /// </summary>
        /// <param name="trial"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpPost("TrialValidation")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> TrialValidation([FromBody] TrialValidation trial)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> errorMessage = new();
                    var result = await _cartService.TrialValidation(trial, errorMessage);

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
                        if (result == 0)
                        {
                            logger.LogDebug(LearnerAppConstants.SEVEN_DAYS_TRIAL_ADDED);
                            return StatusCode((int)HttpStatusCode.OK, new PaymentResponse()
                            {
                                Message = LearnerAppConstants.SEVEN_DAYS_TRIAL_ADDED
                            });
                        }
                        else
                        {
                            logger.LogDebug(LearnerAppConstants.SEVEN_DAYS_MODIFIED_SUCCESSFULLY);
                            return StatusCode((int)HttpStatusCode.OK, new PaymentResponse()
                            {
                                Message = LearnerAppConstants.SEVEN_DAYS_MODIFIED_SUCCESSFULLY
                            });
                        }
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






        #endregion


        #region DELETE
        /// <summary>
        /// To delete a cart item
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        [HttpDelete("DeleteCartItem")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> CartItem([FromBody] DeleteCart cart)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> errorMessage = new();
                    var result = await _cartService.DeleteCartItem(cart, errorMessage);

                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<DeleteCart>() { Message = errMsg, Data = cart });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.CART_ITEM_DELETED);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<DeleteCart>()
                        {
                            Message = LearnerAppConstants.CART_ITEM_DELETED
                        });
                    }
                }
                else
                {
                    logger.LogDetails(new LogDetails(LogType.Error)
                    {
                        Message = LearnerAppConstants.MODEL_VALIDATION_FAILED,
                        AppUrl = Request.GetDisplayUrl(),
                        RequestParam = cart
                    });

                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(CartItem)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });

                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<DeleteCart>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, Data = cart });
            }
        }
        #endregion
    }
}
