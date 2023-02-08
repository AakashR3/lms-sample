using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository
{
    public class UserRepo : IUserRepo
    {

        private readonly IDatabaseManager _databaseOperations;
        public UserRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }


        public Task<int> UserRegistration(UserRegisteration userRegisteration)
        {
            var param = new Dictionary<string, object>
            {
                { "@AccountName", userRegisteration.FirstName ?? "" + "_" + userRegisteration.LastName ?? "" },
                { "@Email", userRegisteration.Email },
                { "@UserName", userRegisteration.Email },
                { "@Password", userRegisteration.Password },
                { "@FirstName", userRegisteration.FirstName },
                { "@LastName", userRegisteration.LastName },
                { "@PreferredSoftwareID", userRegisteration.PreferredSoftwareID },
                { "@FavouriteSoftware", userRegisteration.FavouriteSoftware },
                { "@AccountTypeID", userRegisteration.AccountTypeID },
                { "@ManagerID", userRegisteration.ManagerID },
                { "@CompanyName", userRegisteration.CompanyName },
                { "@Country", userRegisteration.Country },
                { "@Platform", userRegisteration.Platform },
                { "@SocialType", userRegisteration.SocialType },
                { "@SocialID", userRegisteration.SocialID },
                { "@EmailPref", userRegisteration.EmailPref },
                { "@MarketingEmail", userRegisteration.MarketingEmail }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.AUTH_USER_REGISTRATION,
                Parameters = param
            };

            return _databaseOperations.ExecuteNonQueryAsync(queryInfo);
        }

        public Task<Login> Authentication(Login login)
        {
            var param = new Dictionary<string, object>
            {
                { "@UserName", login.UserName },
                { "@Password", login.EncPassword },
                { "@SocialTypeName", login.SocialTypeName },
                { "@LoginType", login.LoginType }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.CHECK_LOGIN,
                Parameters = param
            };

            return _databaseOperations.GetFirstRecordAsync<Login>(queryInfo);
        }

        public Task<bool> CheckUser(string Username)
        {
            var param = new Dictionary<string, object>
            {
                { "@UserName", Username }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.IS_USER_EXISTS_V2,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncBool(queryInfo);
        }
        public Task<ResetPassword> ForgotPassword(string UserName)
        {
            var param = new Dictionary<string, object>
            {
                { "@UserName", UserName }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.PASSWORD_RECOVERY_REQUEST,
                Parameters = param
            };
            return _databaseOperations.GetFirstRecordAsync<ResetPassword>(queryInfo);
        }

        public Task<Users> GetUserByUserName(string UserName)
        {
            var param = new Dictionary<string, object>
            {
                { "@UserName", UserName }
            };

            //var response = _dbContext.Users.Where(x => x.UserName == UserName).FirstOrDefault();
            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.DirectText,
                QueryText = "SELECT * FROM DBO.USERS WHERE USERNAME=@USERNAME",
                Parameters = param
            };

            return _databaseOperations.GetFirstRecordAsync<Users>(queryInfo);
        }
        /// <summary>
        /// Insert OTP details
        /// </summary>
        /// <param name="registrationOTP"></param>
        /// <returns></returns>
        public async Task<int> SendOTP(RegistrationOTP registrationOTP)
        {
            var param = new Dictionary<string, object>
            {
                { "@Email", registrationOTP.Email },
                { "@Otp", registrationOTP.Otp }
            };

            QueryInfo queryInfo = new QueryInfo()
            {

                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.REGISTRATION_OTP,
                Parameters = param
            };

            return await _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<Login> SocialValidation(SocialRegisteration register)
        {
            var param = new Dictionary<string, object>
            {
                { "@AccountName", register.FirstName ?? "" + "_" + register.LastName ?? "" },
                { "@Email", register.Email },
                { "@UserName", register.Email },
                { "@Password", register.Password },
                { "@FirstName", register.FirstName },
                { "@LastName", register.LastName },
                { "@PreferredSoftwareID", register.PreferredSoftwareID },
                { "@FavouriteSoftware", register.FavouriteSoftware },
                { "@AccountTypeID", register.AccountTypeID },
                { "@ManagerID", register.ManagerID },
                { "@CompanyName", register.CompanyName },
                { "@Country", register.Country },
                { "@Platform", register.Platform },
                { "@SocialType", register.SocialType },
                { "@SocialID", register.SocialID },
                { "@EmailPref", register.EmailPref },
                { "@MarketingEmail", register.MarketingEmail }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.SOCIAL_VALIDATION,
                Parameters = param
            };

            return _databaseOperations.GetFirstRecordAsync<Login>(queryInfo);
        }

        public Task<int> UpdatePassword(PasswordRecoveryReset passwordRecoveryReset)
        {
            var param = new Dictionary<string, object>
            {
                { "@SessionId", passwordRecoveryReset.EmailSessionId },
                { "@Password", passwordRecoveryReset.EncPassword }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.PASSWORD_RECOVERY_RESET,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<int> ResetUserPassword(string UserName, int UserId, string CurrentPassword, string NewPassword)
        {
            var param = new Dictionary<string, object>
            {
                { "@UserName", UserName },
                { "@UserId", UserId },
                { "@CurrentPassword", CurrentPassword },
                { "@NewPassword", NewPassword }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.RESET_USER_PASSWORD_BY_USERID,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<int> UserLogout(Logout logout)
        {
            var param = new Dictionary<string, object>
            {
                { "@SessionId", logout.SessionId }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.LOGIN_LOGOUT,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<int> VerifyEmailLink(string EmailSessionId)
        {
            var param = new Dictionary<string, object>
            {
                { "@SessionId", EmailSessionId }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.RESET_PASSWORD_LINK_VALIDATION_V2,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<int> VerifyOTP(RegistrationOTP registrationOTP)
        {
            var param = new Dictionary<string, object>
            {
                { "@Email", registrationOTP.Email },
                { "@Otp", registrationOTP.Otp }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.REGISTRATION_OTP_VERIFICATION_V2,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }


        public Task<int> GetAccountIDByDomainName(string DomainName)
        {
            var param = new Dictionary<string, object>
            {
                { "@DomainName", DomainName }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GET_ACCOUNTID_BY_DOMAIN_NAME,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }


        public Task<SsoinfoShort> GetSSOInfoByAccountID(int AccountID)
        {
            var param = new Dictionary<string, object>
            {
                { "@AccountID", AccountID }

            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.APP_SSO_INFO,
                Parameters = param
            };

            return _databaseOperations.GetFirstRecordAsync<SsoinfoShort>(queryInfo);
        }

        public Task<app_SSOLogging> SSOLogging(string reqid, int AccountID, int ItemID, int ItemTypeID)
        {
            var param = new Dictionary<string, object>
            {
                { "@SSOID", reqid },
                { "@AccountID", AccountID },
                { "@ItemID", ItemID },
                { "@ItemTypeID", ItemTypeID }

        };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.APP_SSO_LOGGING,
                Parameters = param
            };

            return _databaseOperations.GetFirstRecordAsync<app_SSOLogging>(queryInfo);
        }


        public Task<login_CheckADLogin> login_CheckADLogin(string DomainUserID, int DomainID, int AccountID)
        {
            var param = new Dictionary<string, object>
            {
                 { "@DomainUserID", DomainUserID },
                 { "@DomainID", DomainID },
                 { "@AccountID", AccountID },

            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.LOGIN_CHECK_AD_LOGIN,
                Parameters = param
            };

            return _databaseOperations.GetFirstRecordAsync<login_CheckADLogin>(queryInfo);
        }


        public Task<int> SSORegister(int AccountID, string ssoNameID, string ssoUserID, string Password, string FirstName, string LastName)
        {
            var param = new Dictionary<string, object>
            {
                  { "@AccountID", AccountID },
                  { "@Email", ssoNameID },
                  { "@SSOUserID", ssoUserID },
                  { "@Password", Password },
                  { "@FirstName", FirstName },
                  { "@LastName", LastName }
        };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.LOGIN_SSO_REGISTER,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<int> SSOUpdateUserDetails(int UserID, string ssoFirstName, string ssoLastName, string ssoCompany, string ssoShipCountry, string ssoBusinessSite, string ssoBusinessGroup, string ssoAttribute1, string ssoAttribute2, string ssoAttribute3, string ssoAttribute4, string ssoJobRole, string ssoRegion)
        {
            var param = new Dictionary<string, object>
            {
                  { "@UserID", UserID },
                  { "@FirstName", ssoFirstName },
                  { "@LastName", ssoLastName },
                  { "@Company", ssoCompany },
                  { "@ShipCountry", ssoShipCountry },
                  { "@BusinessSite", ssoBusinessSite },
                 { "@BusinessGroup", ssoBusinessGroup },
                  { "@Attribute1", ssoAttribute1 },
                 { "@Attribute2", ssoAttribute2 },
                  { "@Attribute3", ssoAttribute3 },
                 { "@Attribute4", ssoAttribute4 },
                  { "@JobRole", ssoJobRole },
                   { "@Region", ssoRegion },
        };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.APP_SSO_UPDATE_USERDETAILS,
                Parameters = param
            };

            return _databaseOperations.ExecuteNonQueryAsync(queryInfo);
        }

        public Task<Login> SSOLoginValidation(string UserName)
        {
            var param = new Dictionary<string, object>
            {
                 { "@UserName", UserName }

            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.SSO_LOGIN_VALIDATION,
                Parameters = param
            };

            return _databaseOperations.GetFirstRecordAsync<Login>(queryInfo);
        }

        public Task<bool> UpdateAddress(UpdateAddress updateAddress)
        {
            var param = new Dictionary<string, object>
            {
                 { "@UserID", updateAddress.UserID },
                 { "@ShipAddress1", updateAddress.ShipAddress1 },
                 { "@ShipAddress2", updateAddress.ShipAddress2 },
                 { "@ShipCity", updateAddress.ShipCity },
                 { "@ShipState", updateAddress.ShipState },
                 { "@ShipPostalCode", updateAddress.ShipPostalCode },
                 { "@ShipCountry", updateAddress.ShipCountry }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.UpdateAddress,
                Parameters = param
            };

            return _databaseOperations.GetFirstRecordAsync<bool>(queryInfo);
        }

        public Task<VerifySessionData> VerifySession(string SessionID, int UserTypeID, int AccTypeID, string Username, int TimeoutMin)
        {
            var param = new Dictionary<string, object>
            {
                 { "@SessionID", SessionID },
                 { "@UserTypeID", UserTypeID },
                 { "@AccTypeID", AccTypeID },
                 { "@Username", Username },
                 { "@TimeoutMin", TimeoutMin },
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.VerifySession,
                Parameters = param
            };

            return _databaseOperations.GetFirstRecordAsync<VerifySessionData>(queryInfo);
        }


        public Task<Login> AuthenticationService(string SessionID)
        {
            var param = new Dictionary<string, object>
            {
                 { "@SessionID", SessionID }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.AUTHENTICATION_SERVICE,
                Parameters = param
            };

            return _databaseOperations.GetFirstRecordAsync<Login>(queryInfo);
        }
    }
}
