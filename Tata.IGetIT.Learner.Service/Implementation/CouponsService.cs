using Recurly.Resources;
using Recurly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Service.Implementation
{
    /// <summary>
    /// Coupons service
    /// </summary>
    public class CouponsService : ICouponsService
    {
        private readonly RecurlyConfig _recurlyConfig;
        private readonly Client _reculyClient;
        ILogger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Coupons service constructor initialization.
        /// </summary>
        /// <param name="recurlyConfig">Recurly payment config injected from Program.cs</param>
        public CouponsService(IOptions<RecurlyConfig> recurlyConfig)
        {
            if (recurlyConfig == null)
            {
                new ArgumentNullException(LearnerAppConstants.RECURLY_CONFIG_NOT_FOUND);
            }
            _recurlyConfig = recurlyConfig.Value;

            if(_recurlyConfig.ApiKey == null)
            {
                new KeyNotFoundException(LearnerAppConstants.RECURLY_KEY_IS_NULL_OR_EMPTY);
            }
            _reculyClient = new Recurly.Client(_recurlyConfig.ApiKey);
        }

        /// <summary>
        ///  Redeem recurly coupon
        /// </summary>
        /// <param name="request">Redeem coupon request</param>
        /// <returns>Coupon redemption details</returns>
        public async Task<CouponRedemption> RedeemRecurlyCouponAsync(RedeemCouponRequest request)
        {
            // doc: https://developers.recurly.com/guides/coupon-guide.html#step-2-coupon-redemption
            var couponRedemption = new CouponRedemptionCreate()
            {
                Currency = request.CurrencyCode,
                CouponId = "code-"+ request.CouponCode
            };

            logger.LogInfo($"Redeem recurly coupon initated for account {request.AccountId}");

            var result = await _reculyClient.CreateCouponRedemptionAsync(request.AccountId, couponRedemption);

            logger.LogInfo($"Redeem recurly coupon completed for AccountId={request.AccountId}, state={result.State}, discounted={result.Discounted}, SubscriptionId={result.SubscriptionId}");

            return result;
        }

        public async Task<Coupon> ValidateRecurlyCouponAsync(string code)
        {
            return await _reculyClient.GetCouponAsync("code-" + code);
        }
    }
}
