using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface IUserRepo
    {
        public Task<Login> Authentication(Login login);
        public Task<int> UserRegistration(UserRegisteration register);
        public Task<Login> SocialValidation(SocialRegisteration register);
        public Task<bool> CheckUser(string UserName);
        public Task<int> UpdatePassword(PasswordRecoveryReset passwordRecoveryReset);

        public Task<int> ResetUserPassword(string UserName, int UserId, string CurrentPassword, string NewPassword);
        public Task<bool> UpdateAddress(UpdateAddress updateAddress);
        public Task<VerifySessionData> VerifySession(string SessionID, int UserTypeID, int AccTypeID, string Username, int TimeoutMin);
        public Task<Users> GetUserByUserName(string UserName);
        public Task<ResetPassword> ForgotPassword(string UserName);
        public Task<int> VerifyEmailLink(string EmailSessionId);
        public Task<int> SendOTP(RegistrationOTP registrationOTP);
        public Task<int> VerifyOTP(RegistrationOTP registrationOTP);
        public Task<int> UserLogout(Logout logout);

        public Task<Login> AuthenticationService(string SessionID);

        ////////SSO/////////
        public Task<int> GetAccountIDByDomainName(string DomainName);
        public Task<SsoinfoShort> GetSSOInfoByAccountID(int AccountID);
        public Task<app_SSOLogging> SSOLogging(string reqid, int AccountID, int ItemID, int ItemTypeID);

        public Task<login_CheckADLogin> login_CheckADLogin(string DomainUserID, int DomainID, int AccountID);

        public Task<int> SSORegister(int AccountID, string ssoNameID, string ssoUserID, string Password, string FirstName, string LastName);

        public Task<int> SSOUpdateUserDetails(int UserID, string ssoFirstName, string ssoLastName, string ssoCompany, string ssoShipCountry, string ssoBusinessSite, string ssoBusinessGroup, string ssoAttribute1, string ssoAttribute2, string ssoAttribute3, string ssoAttribute4, string ssoJobRole, string ssoRegion);

        public Task<Login> SSOLoginValidation(string UserName);


    }
}
