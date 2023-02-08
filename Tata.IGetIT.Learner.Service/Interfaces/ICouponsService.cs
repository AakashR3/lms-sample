using Tata.IGetIT.Learner.Repository.Models;
using Recurly.Resources;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface ICouponsService
    {
        /// <summary>
        /// Redeem Coupon
        /// </summary>
        /// <param name="request">Redeem coupon request</param>
        /// <returns>Coupon redemption details</returns>
        public Task<CouponRedemption> RedeemRecurlyCouponAsync(RedeemCouponRequest request);

        /// <summary>
        /// Validate Recurly coupon code
        /// </summary>
        /// <param name="code"> coupon code</param>
        /// <returns></returns>
        public Task<Coupon> ValidateRecurlyCouponAsync(string code);
    }
}
