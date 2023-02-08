using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Helpers;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class Payment
    {
    }

    public class PaymentInitiate : IValidatableObject
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int AccountID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public int Quantity { get; set; }
        public int NoOfUsers { get; set; }
        public int PlanCode { get; set; }
        public int SubscriptionID { get; set; }
        public string PromoCode { get; set; }
        public int IsTrial { get; set; }
        public int TotalCount { get; set; }
        public string PurchaseType { get; set; }
        public int CartID { get; set; }

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();

            if (UserID == 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_USERID_REQUIRED));
            }

            if (AccountID == 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_ACCOUNTID_REQUIRED));
            }

            if (FirstName.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_FNAME_REQUIRED));
            }

            if (LastName.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_LNAME_REQUIRED));
            }

            if (Email.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_EMAIID_REQUIRED));
            }

            // if (ContactNumber.IsNullOrEmptyOrWhiteSpace())
            // {
            //     validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_CONTACTNO_REQUIRED));
            // }

            if (Address.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_ADDRESS_REQUIRED));
            }

            if (Quantity == 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_QUANTITY_REQUIRED));
            }
            if (NoOfUsers == 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_NO_OF_USER_REQUIRED));
            }

            if (PlanCode == 0 || PlanCode.ToString().Length == 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_PLAN_CODE_REQUIRED));
            }

            if (IsTrial.ToString().Length == 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_ISTRIAL_REQUIRED));
            }

            if (TotalCount == 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_PLAN_CODE_REQUIRED));
            }

            if (PurchaseType.ToUpper() != "PLAN" && PurchaseType.ToUpper() != "SUBSCRIPTION")
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_PAYMENT_TYPE_INVALID));
            }
            if (CartID == 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_CART_ID_REQUIRED));
            }


            return validationErrors;
        }
    }

    public class RazorpayResponse : IValidatableObject
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int AccountID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string PlanName { get; set; }
        public string RZP_PlanID { get; set; }
        public int Quantity { get; set; }
        public int NoOfUsers { get; set; }
        public string TransactionID { get; set; }
        public string OrderID { get; set; }
        public string Signature { get; set; }
        public string PaymentID { get; set; }
        public int SubscriptionID { get; set; }
        public string RZP_SubscriptionID { get; set; }
        public double Amount { get; set; }
        public double ChargeAt { get; set; }
        public double StartAt { get; set; }
        public double EndAt { get; set; }
        public double TrialstartAt { get; set; }
        public double TrialEndAt { get; set; }
        public int IsTrial { get; set; }
        public string PurchaseType { get; set; }
        public int CartID { get; set; }

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();

            if (Email.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_EMAIID_REQUIRED));
            }

            if (Signature.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_SIGNATURE_REQUIRED));
            }

            if (PaymentID.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_PAYMENTID_REQUIRED));
            }
            if (CartID == 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_CART_ID_REQUIRED));
            }
            return validationErrors;
        }

    }


    public class CancelSubscription : IValidatableObject
    {
        public string SubscriptionID { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();

            if (SubscriptionID.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_SUBSCRIPTION_ID_REQUIRED));
            }

            return validationErrors;
        }
    }


    public class TrialValidation : IValidatableObject
    {
        public int UserID { get; set; }
        public int CartID { get; set; }
        public int IsTrial { get; set; }
        public string PurchaseType { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();

            if (PurchaseType.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.PURCHASETYPE_REQUIRED));
            }

            return validationErrors;
        }
    }







    public class PaymentResponse
    {
        public string Message { get; set; }
        [JsonProperty("Data")]
        public RazorpayModel Data { get; set; } = null!;

    }



    public class Currency
    {
        public int CID { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public string Description { get; set; }
    }

    public class RazorpayModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int AccountID { get; set; }
        public string OrderID { get; set; }
        public string RazorpayKey { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int PlanName { get; set; }
        public string PlanDescription { get; set; }
        public string PlanID { get; set; }
        public int Quantity { get; set; }
        public int NoOfUsers { get; set; }
        public int IsTrial { get; set; }
        public string PurchaseType { get; set; }
        public string TransactionID { get; set; }
        public double DtChargeAt { get; set; }
        public double DtStartAt { get; set; }
        public double DtEndAt { get; set; }
        public double DtTrialStartAt { get; set; }
        public double DtTrialEndAt { get; set; }
        public int SubscriptionID { get; set; }
        public int IntQuantity { get; set; }
        public string RZP_SubscriptionID { get; set; }
        public int CartID { get; set; }
    }

    public class PlanDetailsModel
    {
        public int PlanID { get; set; }
        public string PlanDuration { get; set; }
        public int PlanInterval { get; set; }
    }

    public class RazorPlanObject
    {
        public string ID { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public string PlanCode { get; set; }
    }

    public class RazorSubObject
    {
        public string ID { get; set; }
        public string PlanID { get; set; }
        public string Status { get; set; }
    }

    public class SubObject
    {
        public string Name { get; set; }
        public int SubID { get; set; }
        public string UUID { get; set; }
        public string CurrencyCode { get; set; }
        public double Cost { get; set; }
        public int FulfillmentID { get; set; }
        public string Email { get; set; }
        public string EcomID { get; set; }
        public Nullable<DateTime> ExpireDate { get; set; }
        public Nullable<DateTime> Trial { get; set; }
    }



    #region Recurly Pament Model
    public class RecurlyCardCheckout : IValidatableObject
    {
        public int UserID { get; set; }
        public int AccountID { get; set; }
        public int PlanCode { get; set; }
        public string CurrencyCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();

            if (CurrencyCode.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.CURRENCY_CODE_REQUIRED));
            }

            if (FirstName.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.FIRSTNAME_REQUIRED));
            }

            if (LastName.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.LASTNAME_REQUIRED));
            }

            if (Email.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.EMAIID_REQUIRED));
            }

            if (Address1.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.ADDRESS1_REQUIRED));
            }

            if (City.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.CITY_REQUIRED));
            }

            if (State.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.STATE_REQUIRED));
            }

            if (PostalCode.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.POSTALCODE_REQUIRED));
            }

            if (Country.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.COUNTRY_REQUIRED));
            }
            return validationErrors;
        }
    }

    public class RecurlyCardCheckoutResponse
    {
        public string RecurlyID { get; set; }
        public int PlanCode { get; set; }
        public string CurrencyCode { get; set; }
        public CouponObject Coupon { get; set; }
        public int AccountID { get; set; }
        public string TokenID { get; set; }
        public string Signature { get; set; }
        public string PlanName { get; set; }
        public BillingObject BillingInfo { get; set; }
    }

    public class CouponObject
    {
        public string Code { get; set; }
        public double Amount { get; set; }
        public string Date { get; set; }
        public string Currency { get; set; }
    }

    public class PlanObject
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class APIResponse
    {
        public int StatusCode { get; set; }
        public string ResponseString { get; set; }
    }
    public class BillingObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string LastFour { get; set; }
    }


    public class RenewalObject
    {
        public string ExpiryDate { get; set; }
        public int PlanCode { get; set; }
        public int Quantity { get; set; }
        public string CentCost { get; set; }
        public int DollarCost { get; set; }       
    }

    public class TransactionObject
    {
        public double Cost { get; set; }
        public string CurrencyCode { get; set; }
        public string UUID { get; set; }
        public string Email { get; set; }
    }

    #endregion
}
