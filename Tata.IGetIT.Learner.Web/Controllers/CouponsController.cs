using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Recurly;
using Tata.IGetIT.Learner.Repository.Models;
using Recurly.Resources;
using Tata.IGetIT.Learner.Service.Helpers;

namespace Tata.IGetIT.Learner.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : BaseController
    {

        private readonly ICouponsService _couponsService;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public CouponsController(ICouponsService couponsService)
        {
            _couponsService = couponsService ?? throw new ArgumentNullException(nameof(ICouponsService));
        }

        [HttpPost("RedeemRecurlyCoupon")]
        [SwaggerResponse(200, "Ok", typeof(ReturnResponse<CouponRedemption>))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> RedeemRecurlyCouponAsync(RedeemCouponRequest request)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError(LearnerAppConstants.MODEL_VALIDATION_FAILED, new ApiException("Redeem recurly coupon model validation failed"));
                return await PopulateModelErrorsAsync();
            }

            var result = await _couponsService.RedeemRecurlyCouponAsync(request);

            return Ok(new ReturnResponse<CouponRedemption>() { Message = LearnerAppConstants.SUBSCRIPTION_FOUND, Data = result });
        }


        [HttpGet("ValidateRecurlyCoupon")]
        [SwaggerResponse(200, "Ok", typeof(ReturnResponse<Coupon>))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> ValidateRecurlyCouponAsync(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                logger.LogError(LearnerAppConstants.COUPON_CODE_REQUIRED, new ApiException("Redeem recurly coupon model validation failed"));
                throw new Exception(LearnerAppConstants.COUPON_CODE_REQUIRED);
            }
            var result = await _couponsService.ValidateRecurlyCouponAsync(code);
            if(result.State == Recurly.Constants.CouponState.Redeemable)
            {
                return Ok(new ReturnResponse<Coupon>() { Message = LearnerAppConstants.COUPON_VALID, Data = result });
            }

            return Ok(new ReturnResponse<Coupon>() { Message = LearnerAppConstants.COUPON_IS_NOT_VALID, Data = result });
        }
    }
}
