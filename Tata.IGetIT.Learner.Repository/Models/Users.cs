using System.ComponentModel.DataAnnotations;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Helpers;


namespace Tata.IGetIT.Learner.Repository.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? RenewalDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string Ponumber { get; set; }
        public int? UserStatusId { get; set; }
        public int? UserTypeId { get; set; }
        public int AccountId { get; set; }
        public int PreferredSubcategoryId { get; set; }
        public bool? DemoAccount { get; set; }
        public bool? Download { get; set; }
        public bool? CourseBuilder { get; set; }
        public byte? EditUsername { get; set; }
        public byte? EditLearningPath { get; set; }
        public byte? EditPassword { get; set; }
        public string AcctMgmtRights { get; set; }
        public string AcctMgmtGod { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public bool? EmailPref { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public int? EmployeeType { get; set; }
        public string CompanyName { get; set; }
        public string Industry { get; set; }
        public string CustomerType { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Platform { get; set; }
        public string Browser { get; set; }
        public string AgreeToTerms { get; set; }
        public DateTime? SecondTermsAcceptance { get; set; }
        public string ShipAddress1 { get; set; }
        public string ShipAddress2 { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public DateTime? ConnectTime { get; set; }
        public DateTime? DisconnectTime { get; set; }
        public double? Ttime { get; set; }
        public int? TotalLogins { get; set; }
        public byte? PrefFlash { get; set; }
        public DateTime? LastModDate { get; set; }
        public bool? IgiTerms { get; set; }
        public int? LastModById { get; set; }
        public string Bolmultiloginok { get; set; }
        public int? ShowMessage { get; set; }
        public string EncPassword { get; set; }

    }

    public class Response
    {
        public string Message { get; set; }
        public UserDetails Data { get; set; } = null!;
    }

    public class LoginResponse
    {
        public int? StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public UserDetails Data { get; set; } = null!;
    }

    public class UserDetails
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SessionId { get; set; }
        public string TokenId { get; set; } = string.Empty;
    }
    public class CheckValidUser : IValidatableObject
    {
        public string UserName { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (UserName.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_USERNAME));
            }

            return validationErrors;
        }
    }
    public partial class Login : IValidatableObject
    {
        public string UserName { get; set; }
        public string EncPassword { get; set; }
        public string SocialTypeName { get; set; }
        public int? UserStatusId { get; set; }
        public int? UserId { get; set; }
        public int? AccountId { get; set; }
        public int AccountTypeID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SessionId { get; set; }
        public string LoginType { get; set; }
        public string UserTypeID { get; set; }
        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();

            if (LoginType.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_LOGINTYPE));
            }
            else if (!LoginType.IsNullOrEmptyOrWhiteSpace() && (LoginType.ToUpper().Equals("BASIC") && (UserName.IsNullOrEmptyOrWhiteSpace() || EncPassword.IsNullOrEmptyOrWhiteSpace())))
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.USER_NAME_OR_PASSWORD_REQUIRED));
            }

            return validationErrors;
        }
    }

    public partial class UpdateAddress
    {
        public int UserID { get; set; }
        public string ShipAddress1 { get; set; }
        public string ShipAddress2 { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
    }

    public partial class VerifySessionData
    {
        public int SessionStatus { get; set; }
        public int ChangeStatus { get; set; }
    }
    public partial class VerifySession : IValidatableObject
    {
        public string SessionID { get; set; }
        public int UserTypeID { get; set; }
        public int AccTypeID { get; set; }
        public string Username { get; set; }
        public int TimeoutMin { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (SessionID.Length != 36)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_SESSIONID));
            }
            if (UserTypeID <= 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_USERTYPE));
            }
            if (AccTypeID <= 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_AccountTypeId));
            }
            if (TimeoutMin < 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_TimeOutMin));
            }
            return validationErrors;
        }
    }
    public partial class ResetUserPassword : IValidatableObject
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (UserName.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.USERNAME_REQUIRED));
            }
            if (CurrentPassword.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.CURRENT_PASSWORD_REQUIRED));
            }
            if (NewPassword.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.NEW_PASSWORD_REQUIRED));
            }
            return validationErrors;
        }
    }









    public partial class Logout : IValidatableObject
    {
        public string UserName { get; set; }
        public string EncPassword { get; set; }
        public string SocialTypeName { get; set; }
        public int? UserStatusId { get; set; }
        public int? UserId { get; set; }
        public int? AccountId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SessionId { get; set; }
        public string LoginType { get; set; }
        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (SessionId.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.LOGOUT_SESSION));
            }

            return validationErrors;
        }
    }

    public class SocialRegisteration : IValidatableObject
    {
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PreferredSoftwareID { get; set; }
        public int FavouriteSoftware { get; set; }
        public int AccountTypeID { get; set; }
        public int ManagerID { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string Platform { get; set; }
        public string SocialType { get; set; }
        public string SocialID { get; set; }
        public int? EmailPref { get; set; }
        public string MarketingEmail { get; set; }
        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new();
            if (Email.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.EMAILID_REQUIRED));
            }

            return validationErrors;
        }
    }

    public class UserRegisteration : IValidatableObject
    {
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PreferredSoftwareID { get; set; }
        public int FavouriteSoftware { get; set; }
        public int AccountTypeID { get; set; }
        public int ManagerID { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string Platform { get; set; }
        public string SocialType { get; set; }
        public string SocialID { get; set; }
        public int? EmailPref { get; set; }
        public string MarketingEmail { get; set; }
        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new();
            if (Email.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.EMAILID_REQUIRED));
            }
            if (FirstName.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.FIRSTNAME_REQUIRED));
            }
            if (LastName.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.LASTNAME_REQUIRED));
            }

            if (Password.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.PASSWORD_REQUIRED));
            }

            return validationErrors;
        }
    }
    public partial class RegistrationOTP : IValidatableObject
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int Otp { get; set; }
        public DateTime? SentDateTime { get; set; }
        public int? OtpstatusId { get; set; }
        public DateTime? VerifiedDateTime { get; set; }
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
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_EmailID));
            }
            if (Otp == 0 || Otp.ToString().Length != 4)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_OTP));
            }

            return validationErrors;
        }
    }

    public partial class PasswordRecoveryReset : IValidatableObject
    {
        public string EmailSessionId { get; set; }
        public string EncPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (EmailSessionId.IsNullOrEmptyOrWhiteSpace() || EncPassword.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_EMAILSESSIONID_OR_PASSWORD));
            }

            return validationErrors;
        }
    }
    public partial class ResetPassword : IValidatableObject
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string EmailSessionId { get; set; }
        public int? UserStatusId { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (EmailSessionId.IsNullOrEmptyOrWhiteSpace() || Password.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_EMAILSESSIONID_OR_PASSWORD));
            }

            return validationErrors;
        }
    }

    public partial class LinkedinValidation : IValidatableObject
    {
        /// <summary>
        /// Valid Linked In Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (Code.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.LINKED_IN_CODE));
            }

            return validationErrors;
        }
    }

    /// <summary>
    /// SendOTP
    /// </summary>
    public partial class SendOTP : IValidatableObject
    {
        public string EmailID { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (EmailID.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_EmailID));
            }

            return validationErrors;
        }
    }
    /// <summary>
    /// ForgotPassword
    /// </summary>
    public partial class ForgotPassword : IValidatableObject
    {
        public string UserName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (UserName.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_USERNAME));
            }

            return validationErrors;
        }
    }
    /// <summary>
    /// VerifyEmailLink
    /// </summary>
    public partial class VerifyEmailLink : IValidatableObject
    {
        public string EmailSessionId { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (EmailSessionId.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_EMAILSESSIONID));
            }

            return validationErrors;
        }
    }
    /// <summary>
    /// VerifyForgotPasswordEmail
    /// </summary>
    public partial class VerifyForgotPasswordEmail : IValidatableObject
    {
        public string TokenID { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (TokenID.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_TOKENID));
            }

            return validationErrors;
        }
    }

    public class CountryRegion
    {
        public string isoCode { get; set; }
    }

    public class GeoLocation
    {
        public CountryRegion countryRegion { get; set; }
        public string ipAddress { get; set; }
    }


    public class IgetitAuthService : IValidatableObject
    {
        public string SessionID { get; set; }
        public string CountryCode { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (SessionID.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.SESSIONID_REQUIRED));
            }
            if (CountryCode.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.COUNTRYCODE_REQUIRED));
            }

            return validationErrors;
        }
    }

    public partial class Location
    {
        public string CountryCode { get; set; }
        public string IPAddress { get; set; }
    }

}