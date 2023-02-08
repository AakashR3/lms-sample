
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface IUserService
    {
        public Task<UserDetails> Authentication(Login login, string ClientIP, List<string> ErrorsMessages);
        public Task<int> UserRegistration(UserRegisteration register, List<string> ErrorsMessages);
        public Task<UserDetails> LinkedinValidation(LinkedinValidation linkedinValidation, string ClientIP, List<string> ErrorsMessages);

        public Task<UserDetails> SocialValidation(SocialRegisteration register, string ClientIP, List<string> ErrorsMessages);
        public Task<bool> CheckUser(string UserName);
        public Task<int> UpdatePassword(PasswordRecoveryReset passwordRecoveryReset, List<string> ErrorsMessages);

        public Task<int> ResetUserPassword(ResetUserPassword resetUserPassword, List<string> ErrorsMessages);
        public Task<bool> UpdateAddress(UpdateAddress updateAddress, List<string> ErrorsMessages);
        public Task<VerifySessionData> VerifySession(string SessionID, int UserTypeID, int AccTypeID, string Username, int TimeoutMin, List<string> ErrorsMessages);
        public Task<Users> GetUserByUserName(string UserName);
        public Task<ResetPassword> ForgotPasswordAsync(string UserName, List<string> ErrorsMessages);

        
        public Task<int> VerifyEmailLink(string EmailSessionId, List<string> ErrorsMessages);
        public Task<int> SendOTP(SendOTP sendOTP, List<string> ErrorsMessages);
        public Task<int> VerifyOTP(RegistrationOTP registrationOTP, List<string> ErrorsMessages);
        public Task<int> UserLogout(Logout logout, List<string> ErrorsMessages);


        public Task<int> GetAccountIDByDomainName(string DomainName, List<string> ErrorsMessages);
        public Task<SamlSettings> GetSSOInfoByAccountID(int AccountID, List<string> ErrorsMessages);
        public Task<app_SSOLogging> SSOLogging(string reqid, int AccountID, int ItemID, int ItemTypeID, List<string> ErrorsMessages);
        public Task<string> GetSamlRedirectURL(string message);      
        public Task<string> SAMLResponse(string SAMLResponse, int AccountID,string ClientIP);
        public Task<bool> TokenValidation(string TokenId, List<string> errorsMessages, Dictionary<string, string> claims = null);


        public Task<UserDetails> AuthenticationService(IgetitAuthService igetitAuthService, List<string> ErrorsMessages);
        public Task<Location> Location(string ClientIP, List<string> ErrorsMessages);

    }
}


