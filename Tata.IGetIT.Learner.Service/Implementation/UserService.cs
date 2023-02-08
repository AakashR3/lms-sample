using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web;
using Tata.IGetIT.Learner.Repository.Helpers;
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Service.Implementation
{
    /// <summary>
    /// Service for user controller
    /// </summary>
    public class UserService : IUserService
    {
        #region Declaration

        private readonly IUserRepo _usersRepo;
        private readonly ICommunicationService _emailService;
        private readonly JwtTokenConfig _jwtTokenConfig;
        private readonly SocialLoginConfig _linkedConfig;
        private readonly SSOConfig _ssoConfig;
        private readonly GeoLocationConfig _geoLocationConfig;
        ILogger logger = LogManager.GetCurrentClassLogger();

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authRepo"></param>
        /// <param name="communicationService"></param>
        /// <param name="jwtTokenConfig"></param>
        /// <param name="linkedConfig"></param>
        /// <param name="ssoConfig"></param>
        /// <param name="geoLocationConfig"></param>
        public UserService(IUserRepo authRepo, ICommunicationService communicationService, IOptions<JwtTokenConfig> jwtTokenConfig,
             IOptions<SocialLoginConfig> linkedConfig, IOptions<SSOConfig> ssoConfig, IOptions<GeoLocationConfig> geoLocationConfig)
        {
            if (authRepo == null)
            {
                new ArgumentNullException(LearnerAppConstants.USEREPO_NULL);
            }
            _usersRepo = authRepo;
            _emailService = communicationService;
            _jwtTokenConfig = jwtTokenConfig.Value;
            _linkedConfig = linkedConfig.Value;
            _ssoConfig = ssoConfig.Value;
            _geoLocationConfig = geoLocationConfig.Value;

        }

        /// <summary>
        /// Returns the user details
        /// </summary>
        /// <param name="login"></param>
        /// <param name="errorsMessages"></param>
        /// <returns></returns>
        public async Task<UserDetails> Authentication(Login login, string ClientIP, List<string> errorsMessages)
        {
            try
            {

                var userDetails = new UserDetails();
                var authResult = await _usersRepo.Authentication(login);

                if (authResult != null && authResult.UserStatusId != null && authResult.UserStatusId != 2)
                {
                    switch (authResult.UserStatusId)
                    {
                        case 1:
                            errorsMessages.Add(LearnerAppConstants.REGISTERED_USER_UNABLE_TO_ACCESSS);
                            break;

                        case 3:
                            errorsMessages.Add(LearnerAppConstants.ACCOUNT_EXPIRED);
                            break;

                        case 4:
                            errorsMessages.Add(LearnerAppConstants.ACCOUNT_DISABLED);
                            break;

                        default:
                            errorsMessages.Add(LearnerAppConstants.INVALID_CREDENTIALS);
                            break;
                    }
                }
                else if (authResult.UserStatusId == 2)
                {
                }
                else
                {
                    errorsMessages.Add(LearnerAppConstants.INVALID_CREDENTIALS);
                }


                if (errorsMessages.Any())
                {
                    return userDetails;
                }

                string CurrencyCode = await GetCurrencyCode(ClientIP);

                userDetails = GenerateJwtToken(authResult, CurrencyCode);
                return userDetails;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Check whether the user is exist or not in the database
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public async Task<bool> CheckUser(string UserName)
        {
            try
            {
                return await _usersRepo.CheckUser(UserName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Update the Password
        /// </summary>
        /// <param name="passwordRecoveryReset"></param>
        /// <returns></returns>
        public async Task<int> UpdatePassword(PasswordRecoveryReset passwordRecoveryReset, List<string> ErrorsMessages)
        {
            var result = await _usersRepo.UpdatePassword(passwordRecoveryReset);
            if (result == 1)
            { }
            else if (result == 2)
                ErrorsMessages.Add(LearnerAppConstants.RESET_LINK_EXPIRED);
            else if (result == 3)
                ErrorsMessages.Add(LearnerAppConstants.INVALID_REQUEST);
            else
                ErrorsMessages.Add(LearnerAppConstants.INVALID_USER);
            return result;
        }




        /// <summary>
        /// Update the Password
        /// </summary>
        /// <param name="resetUser"></param>
        /// <returns></returns>
        public async Task<int> ResetUserPassword(ResetUserPassword resetUser, List<string> ErrorsMessages)
        {
            int result = 0;

            if (resetUser.UserName.IsNullOrEmptyOrWhiteSpace())
            {
                ErrorsMessages.Add(LearnerAppConstants.USERNAME_REQUIRED);
            }
            else
            {
                if (resetUser.CurrentPassword.IsNullOrEmptyOrWhiteSpace())
                {
                    ErrorsMessages.Add(LearnerAppConstants.CURRENT_PASSWORD_REQUIRED);
                }
                else
                {
                    if (resetUser.NewPassword.IsNullOrEmptyOrWhiteSpace())
                    {
                        ErrorsMessages.Add(LearnerAppConstants.NEW_PASSWORD_REQUIRED);
                    }
                    else
                    {
                        result = await _usersRepo.ResetUserPassword(resetUser.UserName, resetUser.UserId, resetUser.CurrentPassword, resetUser.NewPassword);

                        if (result == 1)
                        {
                        }
                        else if (result == 2)
                        {
                            ErrorsMessages.Add(LearnerAppConstants.CURRENT_PASSWORD_NOT_MATCH);
                        }
                        else if (result == 3)
                        {
                            ErrorsMessages.Add(LearnerAppConstants.CURRENT_AND_NEW_PASSWORD_MATCH);
                        }
                        else
                        {
                            ErrorsMessages.Add(LearnerAppConstants.INVALID_USER);
                        }
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// Verify the email link
        /// </summary>
        /// <param name="EmailSessionId"></param>
        /// <param name="ErrorsMessages"></param>
        /// <returns></returns>
        public async Task<int> VerifyEmailLink(string EmailSessionId, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _usersRepo.VerifyEmailLink(EmailSessionId);

                if (result == 0)
                {
                    ErrorsMessages.Add(LearnerAppConstants.INVALID_REQUEST);
                }
                else if (result != 1)
                {
                    ErrorsMessages.Add(LearnerAppConstants.RESET_LINK_EXPIRED);
                }
                return result;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Forgot password option to send an reset email link to the user
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="ErrorsMessages"></param>
        /// <returns></returns>
        public async Task<ResetPassword> ForgotPasswordAsync(string UserName, List<string> ErrorsMessages)
        {
            try
            {
                var response = await _usersRepo.ForgotPassword(UserName);
                if (response != null && !response.UserName.IsNullOrEmptyOrWhiteSpace())
                {
                    if (response.UserStatusId == 2)
                    {
                        if (!response.Email.IsNullOrEmptyOrWhiteSpace())
                        {
                            await _emailService.SendForgotPasswordEmailAsync(response);
                        }
                        else
                        {
                            ErrorsMessages.Add(LearnerAppConstants.INVALID_EmailID);
                        }
                    }
                    else
                    {
                        ErrorsMessages.Add(LearnerAppConstants.PASSWORD_INACTIVE_USER);
                    }
                }
                else
                {
                    ErrorsMessages.Add(LearnerAppConstants.INVALID_EmailID);
                }

                return response;

            }
            catch
            {
                throw;
            }
        }

        //To be removed
        public async Task<Users> GetUserByUserName(string UserName)
        {
            return await _usersRepo.GetUserByUserName(UserName);
        }

        /// <summary>
        /// Returns UserDetails with Token
        /// </summary>
        /// <param name="linkedinValidation"></param>
        /// <param name="ErrorsMessages">Populated the error messages if there is an error</param>
        /// <returns></returns>
        public async Task<UserDetails> LinkedinValidation(LinkedinValidation linkedinValidation, string ClientIP, List<string> ErrorsMessages)
        {
            try
            {
                string AuthHeader = string.Empty;
                UserDetails _userDetails = new UserDetails();
                SocialRegisteration register = new()
                {
                    Password = string.Empty,
                    PreferredSoftwareID = 0,
                    FavouriteSoftware = 0,
                    AccountTypeID = 2,
                    ManagerID = 11,
                    Platform = "Website",
                    SocialType = "LinkedinID",
                    EmailPref = 1,
                    MarketingEmail = "Yes"
                };

                string profileAPIurl = _linkedConfig.ProfileAPIurl;
                string emailAPIurl = _linkedConfig.EmailAPIurl;
                string apiKey = _linkedConfig.ApiKey;
                string apiSecret = _linkedConfig.ApiSecret;
                string scope = _linkedConfig.Scope;
                string redirectUrl = _linkedConfig.RedirectUrl;
                string grant_type = _linkedConfig.Granttype;
                string state = _linkedConfig.State;
                var sign = "grant_type=" + grant_type + "&code=" + HttpUtility.UrlEncode(linkedinValidation.Code) + "&redirect_uri=" + HttpUtility.HtmlEncode(redirectUrl) + "&client_id=" + apiKey + "&client_secret=" + apiSecret + "&scope=" + scope + "&state=" + state;

                string Token = await UtilityHelper.httpPostAsync(_linkedConfig.AuthUrl + "?" + sign, new List<KeyValuePair<string, string>>());

                if (Token.IsNullOrEmptyOrWhiteSpace())
                {
                    ErrorsMessages.Add(LearnerAppConstants.UNABLE_TO_GENERATE_TOKEN);
                }
                else
                {
                    LinkedinAccessToken linkedinToken = JsonConvert.DeserializeObject<LinkedinAccessToken>(Token);
                    AuthHeader = "Bearer " + linkedinToken.access_token;

                    var kvpAuthHeader = new List<KeyValuePair<string, string>>()
                    {
                         new KeyValuePair<string, string>("Authorization",AuthHeader)
                    };

                    if (string.IsNullOrEmpty(_linkedConfig.ProfileAPIurl))
                    {
                        ErrorsMessages.Add(LearnerAppConstants.INVALID_CONFIGURATION);
                    }
                    else
                    {
                        var resSocial = await UtilityHelper.httpGetAsync(_linkedConfig.ProfileAPIurl, kvpAuthHeader);
                        JObject Obj = JObject.Parse(resSocial);
                        register.LastName = (string)Obj["localizedLastName"];
                        register.FirstName = (string)Obj["localizedFirstName"];
                        register.SocialID = (string)Obj["id"];
                    }

                    if (string.IsNullOrEmpty(emailAPIurl))
                    {
                        ErrorsMessages.Add(LearnerAppConstants.INVALID_CONFIGURATION);
                    }
                    else
                    {
                        string resEmail = await UtilityHelper.httpGetAsync(emailAPIurl, kvpAuthHeader);
                        LinkedinElement myDeserializedClass = JsonConvert.DeserializeObject<LinkedinElement>(resEmail);

                        var emailAddress = myDeserializedClass.elements.FirstOrDefault()?.Handle?.emailAddress;
                        register.Email = emailAddress;
                    }


                    if (register.Email.IsNullOrEmptyOrWhiteSpace() || register.SocialID.IsNullOrEmptyOrWhiteSpace())
                    {
                        ErrorsMessages.Add(LearnerAppConstants.UNABLE_TO_RETRIEVE_EMAILID_OR_SOCIALID);
                    }
                    else
                    {
                        var response = await _usersRepo.SocialValidation(register);
                        if (response != null)
                        {

                            string CurrencyCode = await GetCurrencyCode(ClientIP);
                            _userDetails = GenerateJwtToken(response, CurrencyCode);
                        }
                        else
                        {
                            ErrorsMessages.Add(LearnerAppConstants.UNABLE_TO_REGISTER);
                        }
                    }
                }
                return _userDetails;
            }
            catch
            {
                throw;
            }
        }


        public async Task<string> GetCurrencyCode(string ClientIP)
        {
            try
            {
                string Currency = string.Empty;
                string CountryCode = string.Empty;

                if (ClientIP != "::1")
                {

                    CountryCode = await UtilityHelper.GetGeoLocation(ClientIP, _geoLocationConfig.GeoLocationApi, _geoLocationConfig.AzureMapKey, _geoLocationConfig.PublicIPApi);

                    if (CountryCode == "IN")
                    {
                        Currency = "INR";
                    }
                    else
                    {
                        Currency = "USD";
                    }
                }
                else
                {
                    Currency = "INR";
                }

                return Currency;
            }
            catch
            {

                return "INR";
            }
        }


        /// <summary>
        /// Generate JWT Token
        /// </summary>
        /// <param name="response"></param>
        /// <param name="CurrencyCode"></param>
        /// <returns></returns>
        private UserDetails GenerateJwtToken(Login response, string CurrencyCode)
        {

            var userDetails = new UserDetails();
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _jwtTokenConfig.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserName", response.UserName.ToString()),
                        new Claim("Email", response.Email),
                        new Claim("UserId", response.UserId.ToString()),
                        new Claim("AccountId", response.AccountId.ToString()),
                        new Claim("FirstName", response.FirstName.ToString()),
                        new Claim("LastName", response.LastName.ToString()),
                        new Claim("SessionId", response.SessionId.ToString()),
                        new Claim("AccountTypeID", response.AccountTypeID.ToString()),
                        new Claim("Currency", CurrencyCode),
                        new Claim("UserTypeID", response.UserTypeID)
                        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfig.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _jwtTokenConfig.Issuer,
                _jwtTokenConfig.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtTokenConfig.ExpiryInMinutes),
                signingCredentials: signIn);

            if (response.UserName != null)
            {
                userDetails.TokenId = new JwtSecurityTokenHandler().WriteToken(token);
            }
            if (response.AccountId != 0)
            {
                userDetails.AccountId = response.AccountId.ToString();
            }
            if (response.UserId != 0)
            {
                userDetails.UserId = response.UserId.ToString();
            }
            if (response.SessionId != null)
            {
                userDetails.SessionId = response.SessionId;
            }

            return userDetails;

        }
        public async Task<int> UserLogout(Logout logout, List<string> ErrorsMessages)
        {
            try
            {
                var response = await _usersRepo.UserLogout(logout);
                if (response != 0)
                    ErrorsMessages.Add(LearnerAppConstants.UNABLE_TO_LOGOUT);
                return response;
            }
            catch
            {

                throw;
            }
        }

        public async Task<int> UserRegistration(UserRegisteration userRegisteration, List<string> ErrorsMessages)
        {
            try
            {
                var response = await _usersRepo.UserRegistration(userRegisteration);
                if (response == 0 || response == -1)
                {
                    ErrorsMessages.Add(LearnerAppConstants.UNABLE_TO_REGISTER);
                }
                return response;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Verify OTP
        /// </summary>
        /// <param name="registrationOTP"></param>
        /// <param name="ErrorsMessages"></param>
        /// <returns></returns>
        public async Task<int> VerifyOTP(RegistrationOTP registrationOTP, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _usersRepo.VerifyOTP(registrationOTP);
                if (result == 0)
                    ErrorsMessages.Add(LearnerAppConstants.INVALID_OTP);
                if (result == 1)
                { }
                else if (result == 2)
                    ErrorsMessages.Add(LearnerAppConstants.OTP_EXPIRED);
                else
                    ErrorsMessages.Add(LearnerAppConstants.INVALID_OTP);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> SendOTP(SendOTP sendOTP, List<string> ErrorsMessages)
        {
            try
            {
                Random rnd = new();
                int OTP = rnd.Next(1000, 9999);
                RegistrationOTP registrationOTP = new()
                {
                    Email = sendOTP.EmailID,
                    Otp = OTP,
                    OtpstatusId = 0,
                    SentDateTime = DateTime.Now
                };

                // Insert OTP details in DB
                var response = await _usersRepo.SendOTP(registrationOTP);
                if (response == 0)
                {
                    ErrorsMessages.Add(LearnerAppConstants.OTP_LIMIT_EXCEEDED);
                }
                else if (response.ToString().Length == 4) //Valid OTP
                {
                    registrationOTP.Otp = response; //overridding OTP
                    // Send an email to the user's registered email ID
                    await _emailService.SendOTPEmailAsync(registrationOTP);
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserDetails> SocialValidation(SocialRegisteration register, string ClientIP, List<string> ErrorsMessages)
        {
            UserDetails _userDetails = new UserDetails();

            var response = await _usersRepo.SocialValidation(register);
            if (response != null)
            {
                string CurrencyCode = await GetCurrencyCode(ClientIP);
                _userDetails = GenerateJwtToken(response, CurrencyCode);
            }
            else
            {
                ErrorsMessages.Add(LearnerAppConstants.UNABLE_TO_REGISTER);
            }
            return _userDetails;
        }

        public async Task<int> GetAccountIDByDomainName(string DomainName, List<string> errorsMessages)
        {
            try
            {
                int response = await _usersRepo.GetAccountIDByDomainName(DomainName);
                if (response > 0)
                {
                    return response;
                }
                else
                {
                    errorsMessages.Add(LearnerAppConstants.DOMAIN_NAME_DOES_NOT_EXIST);
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<SamlSettings> GetSSOInfoByAccountID(int AccountID, List<string> errorsMessages)
        {
            try
            {
                SamlSettings samlSettings = new SamlSettings();

                var response = await _usersRepo.GetSSOInfoByAccountID(AccountID);
                if (response != null)
                {
                    if (response.Ssourl.IsNullOrEmptyOrWhiteSpace() || response.Ssocert.IsNullOrEmptyOrWhiteSpace() || response.Ssoissuer.IsNullOrEmptyOrWhiteSpace())
                    {
                        errorsMessages.Add(LearnerAppConstants.UNABLE_TO_RETRIEVE_ACCOUNT);
                    }
                    else
                    {
                        samlSettings.ssoUrl = response.Ssourl;
                        samlSettings.certificate = response.Ssocert;
                        samlSettings.issuer = response.Ssoissuer;
                    }
                }
                else
                {
                    errorsMessages.Add(LearnerAppConstants.UNABLE_TO_RETRIEVE_ACCOUNT);
                }
                return samlSettings;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<app_SSOLogging> SSOLogging(string reqid, int AccountID, int ItemID, int ItemTypeID, List<string> errorsMessages)
        {
            try
            {
                var response = await _usersRepo.SSOLogging(reqid, AccountID, ItemID, ItemTypeID);
                if (response == null)
                {
                    errorsMessages.Add(LearnerAppConstants.UNABLE_TO_ADD_SSO_LOG);
                }
                else
                {
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Task<string> GetSamlRedirectURL(string message)
        {
            try
            {
                switch (message)
                {
                    case LearnerAppConstants.INVALID_ACCOUNTID:
                        return Task.FromResult(string.Format("{0}{1}{2}", _ssoConfig.RedirectUrl, LearnerAppConstants.SSO_REDIRECT_URL_QUERY_STRING_FAILURE, LearnerAppConstants.INVALID_ACCOUNTID));

                    case LearnerAppConstants.SSO_INVALID_RESPONSE:
                        return Task.FromResult(string.Format("{0}{1}{2}", _ssoConfig.RedirectUrl, LearnerAppConstants.SSO_REDIRECT_URL_QUERY_STRING_FAILURE, LearnerAppConstants.SSO_INVALID_RESPONSE));

                    case LearnerAppConstants.UNABLE_TO_RETRIEVE_ACCOUNT:
                        return Task.FromResult(string.Format("{0}{1}{2}", _ssoConfig.RedirectUrl, LearnerAppConstants.SSO_REDIRECT_URL_QUERY_STRING_FAILURE, LearnerAppConstants.UNABLE_TO_RETRIEVE_ACCOUNT));

                    case LearnerAppConstants.UNABLE_TO_REGISTER_SSO_DETAILS:
                        return Task.FromResult(string.Format("{0}{1}{2}", _ssoConfig.RedirectUrl, LearnerAppConstants.SSO_REDIRECT_URL_QUERY_STRING_FAILURE, LearnerAppConstants.UNABLE_TO_REGISTER_SSO_DETAILS));

                    case LearnerAppConstants.INVALID_DOMAIN_NAME:
                        return Task.FromResult(string.Format("{0}{1}{2}", _ssoConfig.RedirectUrl, LearnerAppConstants.SSO_REDIRECT_URL_QUERY_STRING_FAILURE, LearnerAppConstants.INVALID_DOMAIN_NAME));

                    case LearnerAppConstants.UNABLE_TO_UPDATE_USER_DETAILS:
                        return Task.FromResult(string.Format("{0}{1}{2}", _ssoConfig.RedirectUrl, LearnerAppConstants.SSO_REDIRECT_URL_QUERY_STRING_FAILURE, LearnerAppConstants.UNABLE_TO_UPDATE_USER_DETAILS));

                    case LearnerAppConstants.USER_DOES_NOT_EXISTS:
                        return Task.FromResult(string.Format("{0}{1}{2}", _ssoConfig.RedirectUrl, LearnerAppConstants.SSO_REDIRECT_URL_QUERY_STRING_FAILURE, LearnerAppConstants.USER_DOES_NOT_EXISTS));

                    case LearnerAppConstants.UNABLE_TO_ADD_SSO_LOG:
                        return Task.FromResult(string.Format("{0}{1}{2}", _ssoConfig.RedirectUrl, LearnerAppConstants.SSO_REDIRECT_URL_QUERY_STRING_FAILURE, LearnerAppConstants.UNABLE_TO_ADD_SSO_LOG));

                    case LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE:
                        return Task.FromResult(string.Format("{0}{1}{2}", _ssoConfig.RedirectUrl, LearnerAppConstants.SSO_REDIRECT_URL_QUERY_STRING_FAILURE, LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE));
                    default:
                        return Task.FromResult(string.Format("{0}{1}{2}", _ssoConfig.RedirectUrl, LearnerAppConstants.SSO_REDIRECT_URL_QUERY_STRING_FAILURE, LearnerAppConstants.INVALID_REQUEST));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<login_CheckADLogin> LoginCheckADLogin(string DomainUserID, int DomainID, int AccountID, List<string> errorsMessages)
        {
            try
            {
                login_CheckADLogin login_CheckADLogin = new();

                var response = await _usersRepo.login_CheckADLogin(DomainUserID, DomainID, AccountID);
                if (response != null)
                {
                    login_CheckADLogin = response;
                }
                return login_CheckADLogin;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> SSORegister(int AccountID, string ssoNameID, string ssoUserID, string Password, string FirstName, string LastName, List<string> errorsMessages)
        {
            try
            {
                int response = await _usersRepo.SSORegister(AccountID, ssoNameID, ssoUserID, Password, FirstName, LastName);
                if (response > 0)
                {
                    return response;
                }
                else
                {
                    errorsMessages.Add(LearnerAppConstants.UNABLE_TO_REGISTER_SSO_DETAILS);
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> SSOUpdateUserDetails(int UserID, string ssoFirstName, string ssoLastName, string ssoCompany, string ssoShipCountry, string ssoBusinessSite, string ssoBusinessGroup, string ssoAttribute1, string ssoAttribute2, string ssoAttribute3, string ssoAttribute4, string ssoJobRole, string ssoRegion, List<string> errorsMessages)
        {
            try
            {
                int response = await _usersRepo.SSOUpdateUserDetails(UserID, ssoFirstName, ssoLastName, ssoCompany, ssoShipCountry, ssoBusinessSite, ssoBusinessGroup, ssoAttribute1, ssoAttribute2, ssoAttribute3, ssoAttribute4, ssoJobRole, ssoRegion);
                return response;
            }
            catch
            {
                throw;
            }
        }
        public async Task<string> SSOLoginValidation(string UserName, string ClientIP, List<string> errorsMessages)
        {
            try
            {
                Login login = new Login();
                UserDetails userDetails = new();

                login = await _usersRepo.SSOLoginValidation(UserName);
                if (login.UserId > 0)
                {

                    if (login.UserStatusId != null && login.UserStatusId != 2)
                    {
                        switch (login.UserStatusId)
                        {
                            case 1:
                                errorsMessages.Add(LearnerAppConstants.REGISTERED_USER_UNABLE_TO_ACCESSS);
                                break;

                            case 3:
                                errorsMessages.Add(LearnerAppConstants.ACCOUNT_EXPIRED);
                                break;

                            case 4:
                                errorsMessages.Add(LearnerAppConstants.ACCOUNT_DISABLED);
                                break;

                            default:
                                errorsMessages.Add(LearnerAppConstants.INVALID_CREDENTIALS);
                                break;
                        }
                    }
                    else if (login.UserStatusId == 2)
                    {
                        string CurrencyCode = await GetCurrencyCode(ClientIP);

                        userDetails = GenerateJwtToken(login, CurrencyCode);
                        string returnUrl = string.Format("{0}{1}&token={2}", _ssoConfig.RedirectUrl, LearnerAppConstants.SSO_REDIRECT_URL_QUERY_STRING_SUCCESS, userDetails.TokenId);

                        return returnUrl;

                    }
                    else
                    {
                        return string.Format("{0}{1}{2}", _ssoConfig.RedirectUrl, LearnerAppConstants.SSO_REDIRECT_URL_QUERY_STRING_FAILURE, LearnerAppConstants.USER_DOES_NOT_EXISTS);
                    }
                }
                else
                {
                    errorsMessages.Add(LearnerAppConstants.USER_DOES_NOT_EXISTS);
                }

                return string.Format("{0}{1}{2}", _ssoConfig.RedirectUrl, LearnerAppConstants.SSO_REDIRECT_URL_QUERY_STRING_FAILURE, LearnerAppConstants.USER_DOES_NOT_EXISTS);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> SAMLResponse(string SAMLResponse, int AccountID, string ClientIP)
        {
            try
            {

                List<string> errorMessages = new();
                string returnUrl = string.Empty;

                if (AccountID == 0)
                {
                    logger.LogDebug(LearnerAppConstants.INVALID_ACCOUNTID);
                    returnUrl = await GetSamlRedirectURL(LearnerAppConstants.INVALID_ACCOUNTID);
                    if (returnUrl != null)
                    {
                        return returnUrl;
                    }
                }
                else
                {
                    if (SAMLResponse.IsNullOrEmptyOrWhiteSpace())
                    {
                        logger.LogDebug(LearnerAppConstants.SSO_INVALID_RESPONSE);
                        returnUrl = await GetSamlRedirectURL(LearnerAppConstants.SSO_INVALID_RESPONSE);
                        if (returnUrl != null)
                        {
                            return returnUrl;
                        }
                    }
                    else
                    {
                        var result = await GetSSOInfoByAccountID(AccountID, errorMessages);
                        if (errorMessages.Any())
                        {
                            logger.LogDebug(errorMessages.FirstOrDefault());
                            returnUrl = await GetSamlRedirectURL(LearnerAppConstants.UNABLE_TO_RETRIEVE_ACCOUNT);
                            if (returnUrl != null)
                            {
                                return returnUrl;
                            }
                        }
                        else
                        {
                            SamlResponse samlResponse = new SamlResponse(result);
                            samlResponse.LoadXmlFromBase64(SAMLResponse);

                            if (!samlResponse.IsValid())
                            {
                                logger.LogDebug(LearnerAppConstants.SSO_INVALID_RESPONSE);
                                returnUrl = await GetSamlRedirectURL(LearnerAppConstants.SSO_INVALID_RESPONSE);
                                if (returnUrl != null)
                                {
                                    return returnUrl;
                                }
                            }
                            else
                            {
                                var userInfo = samlResponse.GetBusinessDetails(samlResponse);
                                if (userInfo.ssoUserID.IsNullOrEmptyOrWhiteSpace())
                                {
                                    logger.LogDebug(LearnerAppConstants.UNABLE_TO_RETRIEVE_ACCOUNT);
                                    returnUrl = await GetSamlRedirectURL(LearnerAppConstants.UNABLE_TO_RETRIEVE_ACCOUNT);
                                    if (returnUrl != null)
                                    {
                                        return returnUrl;
                                    }
                                }
                                else
                                {
                                    var userDetails = await LoginCheckADLogin(userInfo.ssoUserID, 0, AccountID, errorMessages);
                                    if (userDetails.Username.IsNullOrEmptyOrWhiteSpace())
                                    {
                                        int UserID = await SSORegister(AccountID, userInfo.ssoNameID.ToString(), userInfo.ssoUserID.ToString(), "", "", "", errorMessages);

                                        if (errorMessages.Any())
                                        {
                                            logger.LogDebug(errorMessages.FirstOrDefault());
                                            returnUrl = await GetSamlRedirectURL(errorMessages.FirstOrDefault());
                                            if (returnUrl != null)
                                            {
                                                return returnUrl;
                                            }
                                        }
                                        else
                                        {
                                            userDetails = await LoginCheckADLogin(userInfo.ssoUserID, 0, AccountID, errorMessages);
                                        }
                                    }

                                    if (userDetails == null)
                                    {
                                        logger.LogDebug(LearnerAppConstants.UNABLE_TO_REGISTER_SSO_DETAILS);
                                        returnUrl = await GetSamlRedirectURL(LearnerAppConstants.UNABLE_TO_REGISTER_SSO_DETAILS);
                                        if (returnUrl != null)
                                        {
                                            return returnUrl;
                                        }
                                    }
                                    else
                                    {
                                        int updateProfile = await SSOUpdateUserDetails(userDetails.UserID, userInfo.ssoFirstName, userInfo.ssoLastName, userInfo.ssoCompany, userInfo.ssoShipCountry, userInfo.ssoBusinessSite, userInfo.ssoBusinessGroup, userInfo.ssoAttribute1, userInfo.ssoAttribute2, userInfo.ssoAttribute3, userInfo.ssoAttribute4, userInfo.ssoJobRole, userInfo.ssoRegion, errorMessages);

                                        if (errorMessages.Any())
                                        {
                                            logger.LogDebug(LearnerAppConstants.UNABLE_TO_UPDATE_USER_DETAILS);
                                            returnUrl = await GetSamlRedirectURL(LearnerAppConstants.UNABLE_TO_UPDATE_USER_DETAILS);
                                            if (returnUrl != null)
                                            {
                                                return returnUrl;
                                            }
                                        }
                                        else
                                        {
                                            var loggingResult = await SSOLogging(userInfo.ssoReqID, AccountID, 0, 0, errorMessages); //Log SSO Request  

                                            if (errorMessages.Any())
                                            {
                                                logger.LogDebug(errorMessages.FirstOrDefault());
                                                returnUrl = await GetSamlRedirectURL(errorMessages.FirstOrDefault());
                                                if (returnUrl != null)
                                                {
                                                    return returnUrl;
                                                }
                                            }
                                            else
                                            {
                                                var response = await SSOLoginValidation(userInfo.ssoNameID, ClientIP, errorMessages);

                                                if (errorMessages.Any())
                                                {
                                                    logger.LogDebug(LearnerAppConstants.USER_DOES_NOT_EXISTS);
                                                    returnUrl = await GetSamlRedirectURL(LearnerAppConstants.USER_DOES_NOT_EXISTS);
                                                    if (returnUrl != null)
                                                    {
                                                        return returnUrl;
                                                    }
                                                }
                                                else
                                                {
                                                    return response.ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return returnUrl;
            }
            catch
            {
                throw;
            }
        }


        public async Task<bool> TokenValidation(string TokenId, List<string> errorsMessages, Dictionary<string, string> claims = null)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfig.Key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var validator = new JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters();
                validationParameters.ValidIssuer = _jwtTokenConfig.Issuer;
                validationParameters.ValidAudience = _jwtTokenConfig.Audience;
                validationParameters.IssuerSigningKey = key;
                validationParameters.ValidateIssuerSigningKey = true;
                validationParameters.ValidateAudience = true;

                if (validator.CanReadToken(TokenId))
                {
                    try
                    {
                        var validationResult = await validator.ValidateTokenAsync(TokenId, validationParameters);
                        if (validationResult != null && validationResult.IsValid)
                        {
                            if (claims == null)
                            {
                                claims = new Dictionary<string, string>();
                            }
                            var jwtToken = (JwtSecurityToken)validationResult.SecurityToken;

                            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
                            var accountId = jwtToken.Claims.FirstOrDefault(x => x.Type == "AccountId").Value;
                            var sessionId = jwtToken.Claims.FirstOrDefault(x => x.Type == "SessionId").Value;
                            var accountType = jwtToken.Claims.FirstOrDefault(x => x.Type == "AccountTypeID").Value;
                            var userType = jwtToken.Claims.FirstOrDefault(x => x.Type == "UserTypeID").Value;
                            var userName = jwtToken.Claims.FirstOrDefault(x => x.Type == "UserName").Value;
                            var currency = jwtToken.Claims.FirstOrDefault(x => x.Type == "Currency").Value;

                            if (string.IsNullOrWhiteSpace(userId) ||
                                string.IsNullOrWhiteSpace(accountId) ||
                                string.IsNullOrWhiteSpace(sessionId) ||
                                string.IsNullOrWhiteSpace(accountType) ||
                                string.IsNullOrWhiteSpace(userType) ||
                                string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(currency)
                                )
                            {
                                throw new Exception($"Session validation failed");
                            }

                            if (Int32.TryParse(userType, out int userTypeID) &&
                                Int32.TryParse(accountType, out int accountTypeId))
                            {
                                var result = await _usersRepo.VerifySession(sessionId, userTypeID, accountTypeId, userName, _jwtTokenConfig.ExpiryInMinutes);

                                //TODO: Need to handle for result.ChangeStatus flag.
                                if (result.SessionStatus != 1)
                                {
                                    throw new Exception($"Session validation failed for username:{userName} sessionIs{sessionId}");
                                }
                            }

                            claims.Add("UserId", userId);
                            claims.Add("AccountId", accountId);
                            claims.Add("SessionId", sessionId);
                            claims.Add("AccountTypeID", accountType);
                            claims.Add("UserTypeID", userType);
                            claims.Add("Currency", currency);
                            return true;
                        }
                        errorsMessages.Add(validationResult.Exception.Message);
                    }
                    catch (Exception ex)
                    {
                        logger.LogDebug(ex.Message.ToString());
                        errorsMessages.Add(LearnerAppConstants.INVALID_TOKEN);
                        return false;
                    }
                }
                else
                {
                    errorsMessages.Add(LearnerAppConstants.INVALID_TOKEN);
                    return false;
                }
            }
            catch
            {
                throw;
            }
            return false;
        }

        //To be removed
        async Task<string> httpGetAsyncs(string url, List<KeyValuePair<string, string>> reqParam)
        {
            HttpClient client = new HttpClient();
            foreach (var kvp in reqParam)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation(kvp.Key, kvp.Value);
            }

            var response = await client.GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }

        //To be removed
        async Task<string> httpPostAsync(string url, List<KeyValuePair<string, string>> reqParam)
        {
            HttpClient client = new HttpClient();
            var stringContent = new FormUrlEncodedContent(reqParam);
            var response = await client.PostAsync(url, stringContent);

            return await response.Content.ReadAsStringAsync();
        }
        //To be removed
        async Task<string> httpGetAsync(string url, List<KeyValuePair<string, string>> reqParam)
        {
            HttpClient client = new HttpClient();
            foreach (var kvp in reqParam)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation(kvp.Key, kvp.Value);
            }

            var response = await client.GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<bool> UpdateAddress(UpdateAddress updateAddress, List<string> ErrorsMessages)
        {
            var result = await _usersRepo.UpdateAddress(updateAddress);
            if (!result)
                ErrorsMessages.Add(LearnerAppConstants.Failure);
            return result;
        }

        public async Task<VerifySessionData> VerifySession(string SessionID, int UserTypeID, int AccTypeID, string Username, int TimeoutMin, List<string> ErrorsMessages)
        {
            var result = await _usersRepo.VerifySession(SessionID, UserTypeID, AccTypeID, Username, TimeoutMin);
            if (result == null)
                ErrorsMessages.Add(LearnerAppConstants.Failure);
            return result;
        }


        /// <summary>
        /// Aauthentication service for validating customer from igeit1.0 to igetit2.0 
        /// </summary>
        /// <param name="igetitAuthService"></param>
        /// <param name="errorsMessages"></param>
        /// <returns></returns>
        public async Task<UserDetails> AuthenticationService(IgetitAuthService igetitAuthService, List<string> errorsMessages)
        {
            try
            {
                var userDetails = new UserDetails();
                string CurrencyCode = string.Empty;
                var authResult = await _usersRepo.AuthenticationService(igetitAuthService.SessionID);

                if (authResult == null)
                {
                    errorsMessages.Add(LearnerAppConstants.INVALID_SESSIONID_OR_EXPIRED);
                }
                else
                {
                    if (authResult != null && authResult.UserStatusId != null && authResult.UserStatusId != 2)
                    {
                        switch (authResult.UserStatusId)
                        {
                            case 1:
                                errorsMessages.Add(LearnerAppConstants.REGISTERED_USER_UNABLE_TO_ACCESSS);
                                break;

                            case 3:
                                errorsMessages.Add(LearnerAppConstants.ACCOUNT_EXPIRED);
                                break;

                            case 4:
                                errorsMessages.Add(LearnerAppConstants.ACCOUNT_DISABLED);
                                break;

                            default:
                                errorsMessages.Add(LearnerAppConstants.INVALID_CREDENTIALS);
                                break;
                        }
                    }
                    else if (authResult.UserStatusId == 2)
                    {
                        if (igetitAuthService.CountryCode.IsNullOrEmptyOrWhiteSpace() || igetitAuthService.CountryCode == "IN")
                        {
                            CurrencyCode = "INR";
                        }
                        else
                        {
                            CurrencyCode = "USD";
                        }

                        userDetails = GenerateJwtToken(authResult, CurrencyCode);
                    }
                }

                if (errorsMessages.Any())
                {
                    return userDetails;
                }

                return userDetails;

            }
            catch
            {
                throw;
            }
        }

        public async Task<Location> Location(string ClientIP, List<string> ErrorsMessages)
        {
            try
            {
                Location location = new Location();
                
                if (ClientIP != "::1")
                {
                    location.CountryCode = await UtilityHelper.GetGeoLocation(ClientIP, _geoLocationConfig.GeoLocationApi, _geoLocationConfig.AzureMapKey, _geoLocationConfig.PublicIPApi);                    
                }
                else
                {
                    location.CountryCode = "IN";
                }

                return location;
            }
            catch
            {
                throw;
            }

        }


    }
}
