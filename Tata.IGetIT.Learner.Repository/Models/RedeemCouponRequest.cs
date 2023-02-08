using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models
{
    /// <summary>
    /// Redeem recurly coupon request
    /// </summary>
    public class RedeemCouponRequest
    {
        /// <summary>
        /// Coupon code
        /// </summary>
        [Required]
        public string CouponCode { get; set; }

        /// <summary>
        /// Currency code
        /// </summary>
        [Required]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Account ID
        /// </summary>
        [Required]
        public string AccountId { get; set; }
    }
}
