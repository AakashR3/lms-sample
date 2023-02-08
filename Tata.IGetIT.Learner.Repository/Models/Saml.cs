using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Xml;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Helpers;


namespace Tata.IGetIT.Learner.Repository.Models
{
    public class SamlSettings
    {
        public string certificate { get; set; }
        public string ssoUrl { get; set; }
        public string issuer { get; set; }
    }
    public class SamelResponse
    {
        public int StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public List<SamlDetails> Data { get; set; } = null!;
    }

    public class SamlDetails
    {
        public string ssoLoginUrl { get; set; }
        public int? accountID { get; set; }
        public string ssoReqID { get; set; }
    }

    public class SamlAuthResponse
    {
        public int? accountID { get; set; }
        public string ssoNameID { get; set; }
        public string ssoUserID { get; set; }
        public string ssoReqID { get; set; }
        public string ssoFirstName { get; set; }
        public string ssoLastName { get; set; }
        public string ssoCompany { get; set; }
        public string ssoShipCountry { get; set; }
        public string ssoBusinessSite { get; set; }
        public string ssoBusinessGroup { get; set; }
        public string ssoAttribute1 { get; set; }
        public string ssoAttribute2 { get; set; }
        public string ssoAttribute3 { get; set; }
        public string ssoAttribute4 { get; set; }
        public string ssoJobRole { get; set; }
        public string ssoRegion { get; set; }
        public string status { get; set; }
        public string certificate { get; set; }
        public string signature { get; set; }
    }

    public partial class login_CheckADLogin
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string EncPassword { get; set; }
    }
    public partial class app_SSOLogging
    {
        public int ItemID { get; set; }
        public int ItemTypeID { get; set; }
    }
    public partial class lp_IsAicc
    {
        public string AiccLP { get; set; }
    }
    public partial class login_SSORedirect
    {
        public string CustomLoginURL { get; set; }
        public string CustomAccountLogin { get; set; }
    }

    public partial class SsoinfoShort
    {
        public string Ssourl { get; set; } = null!;
        public string Ssocert { get; set; }
        public string Ssoissuer { get; set; } = null!;
    }
    public partial class Ssoinfo
    {
        public int AccountId { get; set; }
        public string Ssourl { get; set; } = null!;
        public string Ssocert { get; set; }
        public string Ssoissuer { get; set; } = null!;
        public short Enforce { get; set; }
        public string SsologinUrl { get; set; } = null!;
        public string DomainName { get; set; }
    }
}