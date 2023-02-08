using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Helpers;

namespace Tata.IGetIT.Repository.Models
{
    /// <summary>
    /// Request model to update the password against the user name
    /// </summary>
    /// 
    public class UpdatePasswordRequest : IValidatableObject
    {
        /// <summary>
        /// Base 64 encoded user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password to update
        /// </summary>
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (UserName.IsNullOrEmptyOrWhiteSpace() || Password.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.USER_NAME_OR_PASSWORD_REQUIRED));
            }

            return validationErrors;
        }
    }
}