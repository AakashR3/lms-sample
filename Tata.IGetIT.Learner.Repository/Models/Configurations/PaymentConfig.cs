using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models.Configurations
{
     /// <summary>
     /// Configuration model for Razorpay
     /// </summary>
    public class PaymentConfig
    {
        /// <summary>
        /// PaymentConfig
        /// </summary>
        public string rzp_apiKey { get; set; }
        public string rzp_apiSecret { get; set; }
        public string rzp_apiPlanList { get; set; }
        public string rzp_apiPlanDetails { get; set; }
        public string rzp_apiSubscriptionDetail { get; set; }
        public string rzp_apiCancelSubscription { get; set; }
        public string rzp_webHookSecret { get; set; }

        public string recurly_domainName { get; set; }
        public string recurly_apiKey { get; set; }
        public string recurly_apiSecret { get; set; }
        public string recurly_getActiveCoupon { get; set; }
        public string recurly_getBillingInfo { get; set; }
        public string recurly_loginTokenApi { get; set; }
        
        public string recurly_Webhook_Username { get; set; }
        public string recurly_Webhook_Password { get; set; }


    }
}
