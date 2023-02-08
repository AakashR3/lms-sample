using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Numerics;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Helpers;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class Cart : IValidatableObject
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public int AccountID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int PlanCode { get; set; }
        public int SubscriptionID { get; set; }
        public string CurrencyCode { get; set; }
        public int IsTrial { get; set; }
        public double Price_INR { get; set; }
        public double Price_USD { get; set; }
        public double DiscountPrice { get; set; }
        public int CourseCount { get; set; }
        public string CourseDuration { get; set; }
        public string PurchaseType { get; set; }
        public int BasePlan { get; set; }
        public int PPlan { get; set; }


        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (PlanCode.ToString().Length == 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.RAZORPAY_PLAN_CODE_REQUIRED));
            }
            return validationErrors;
        }
    }

    public class PlanInfo
    {
        public int SubscriptionID { get; set; }
        public float INR { get; set; }
        public float USD { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Duration { get; set; }
        public string CourseType { get; set; }
        public string PlanDesc { get; set; }
    }
        public class DeleteCart : IValidatableObject
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (CartID <= 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_CARTID));
            }
            if (UserID <= 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_USERID));
            }

            return validationErrors;
        }
    }
    public class ShippingInfo
    {
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

    public class Countries
    {
        public string id { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }

    }
}







