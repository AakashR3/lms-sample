using System.ComponentModel.DataAnnotations;


namespace Tata.IGetIT.Learner.Repository.Models
{
    /// <summary>
    /// User
    /// </summary>
    public class Billing
    {
        /// <summary>
        /// First name
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Address line 1
        /// </summary>
        [Required]
        public string Address1 { get; set; }

        /// <summary>
        /// Address line 2
        /// </summary>
        //[Required]
        public string Address2 { get; set; }

        /// <summary>
        /// City
        /// </summary>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// State
        /// </summary>
        [Required]
        public string State { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        [Required]
        public string Country { get; set; }

        /// <summary>
        /// Zip code
        /// </summary>
        [Required]
        public string Zip { get; set; }


    }

    /// <summary>
    /// Purchase Request
    /// </summary>
    public class Purchase
    {

        /// <summary>
        /// Plan code
        /// </summary>
        //[Required]
        public string PlanCode { get; set; }

        /// <summary>
        /// Currency code
        /// </summary>
        [Required]
        public string CurrencyCode { get; set; }


        /// <summary>
        /// Coupon code
        /// </summary>
        public string Coupon { get; set; }

        /// <summary>
        /// Last four 
        /// </summary>
        //[Required]
        public string LastFour { get; set; }

        /// <summary>
        /// Card number
        /// </summary>
        //[Required]
        public string Card { get; set; }

    }

    /// <summary>
    /// Recurly purchase request
    /// </summary>
    public class RecurlyPurchaseRequest
    {
        /// <summary>
        /// Cart ID
        /// </summary>
        //[Required]
        public int CartID { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// Account Id
        /// </summary>
        [Required]
        public string AccountId { get; set; }

        /// <summary>
        /// Recurly token
        /// </summary>
        [Required]
        public string RecurlyToken { get; set; }

        /// <summary>
        /// Purchase details
        /// </summary>
        [Required]
        public Purchase Purchase { get; set; }

        /// <summary>
        /// User details
        /// </summary>
        [Required]
        public Billing Billing { get; set; }

        /// <summary>
        /// Subscriptions
        /// </summary>
        [Required]
        public List<Subscriptions> Subscriptions { get; set; }

    }

    public class Subscriptions
    {
        public int CartID { get; set; }
        public int SubscriptionID { get; set; }
        public int IsTrial { get; set; }    
        public string Title { get; set; }

    }
}
