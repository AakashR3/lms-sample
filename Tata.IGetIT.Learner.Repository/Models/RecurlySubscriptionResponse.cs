using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models
{
    /// <summary>
    ///   Recurly subcription response
    /// </summary>
    public class RecurlySubscriptionResponse
    {
        /// <summary>
        /// Plan Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Plan Id
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// Subscription Id
        /// </summary>
        public string UUID { get; set; }
        /// <summary>
        /// Currency code
        /// </summary>
        public string CurrencyCode { get; set; }
        /// <summary>
        /// Cost
        /// </summary>
        public decimal? Cost { get; set; }
        /// <summary>
        /// Fulfillment Id
        /// </summary>
        public int FulfillmentID { get; set; }
        /// <summary>
        /// Email Id
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Ecom Id
        /// </summary>
        public string EcomID { get; set; }
        /// <summary>
        /// Expiry date
        /// </summary>
        public Nullable<DateTime> ExpireDate { get; set; }
        /// <summary>
        /// Trial date
        /// </summary>
        public Nullable<DateTime> TrialDate { get; set; }
        /// <summary>
        /// Learning Paths
        /// </summary>
        ///public List<SubscriptionLearningPath> Paths { get; set; }
    }
}

