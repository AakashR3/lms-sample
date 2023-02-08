namespace Tata.IGetIT.Learner.Repository.Constants
{
    /// <summary>
    /// Common application constants
    /// </summary>
    public static class ApplicationConstants
    {
        /// <summary>
        /// Path of the email template
        /// </summary>
        public const string EMAIL_TEMPLATE_PATH_FORGOT_PASSWORD = @"Templates/EmailTemplates/ForgetPassword.html";
        public const string EMAIL_TEMPLATE_PATH_EMAIL_ATTACHMENT = @"Templates/EmailTemplates/EmailAttachment.html";
        public const string EMAIL_TEMPLATE_PATH_VERIFY_EMAIL = @"Templates/EmailTemplates/VerifyEmail.html";
        public const string EMAIL_TEMPLATE_PATH_MEETING_INVITE = @"Templates/EmailTemplates/MeetingInvite.html";
    }
    /// <summary>
    /// Constants used to identify the configuration -- DON'T CHANGE
    /// </summary>
    public static class ConfigurationConstants
    {
        public const string DB_CONFIG = "DBConfig";
        public const string JWT_TOKEN = "Token:Jwt";
        public const string SMTP_CONFIG = "SmtpConfig";
        public const string SOCIAL_LOGIN_LINKEDIN_CONFIG = "Social:LinkedIn";
        public const string KEY_VAULT_URL = "KeyVault:Url";
        public const string APP_TENANT_ID = "App:TenantId";
        public const string APP_CLIENT_ID = "App:ClientId";
        public const string APP_ALLOWED_ORIGINS = "App:AllowedOrigins";
        public const string APP_LOG_LEVELS = "App:LogLevels";

        public const string FORGOT_PASSWORD_CONFIG = "ForgotPasswordConfig";
        public const string SSO_CONFIG = "SSO";
        public const string RAZORPAY_CONFIG = "Payment:RazorPay";
        public const string RECURLY_CONFIG = "Payment:Recurly";
        public const string AZURE_MAP_CONFIG = "GeoLocation";
        public const string DISCUSSION_FORUM_CONFIG = "ForumConfig";
    }

    public static class KeyVaultConstants
    {
        public const string DB_PASSWORD = "DBPassword";
        public const string SMTP_PASSWORD = "SmtpPassword";
        public const string JWT_KEY = "JwtKey";
        public const string LINKEDIN_API_KEY = "LinkedInApiKey";
        public const string LINKEDIN_API_SECRET = "LinkedInApiSecret";
        /// <summary>
        /// Recurly API key
        /// </summary>
        public static string RECURLY_API_KEY = $"RecurlyApiKey";

        public const string RAZORPAY_API_KEY = "RazorPayApiKey";
        public const string RAZORPAY_API_SECRET = "RazorPayApiSecret";
        public const string RAZORPAY_WEBHOOK_SECRET = "RazorPayWebhookSecret";

        public const string AZURE_MAP_KEY = "AzureMapKey";


        public const string Recurly_Webhook_Username = "RecurlyWebhookUserName";
        public const string Recurly_Webhook_Password = "RecurlyWebhookPassword";
    }

    public static class EnvironmentConstants
    {
        public const string HOSTING_ENVIRONMENT_DEV = "dev";
        public const string HOSTING_ENVIRONMENT_QA = "qa";
        public const string HOSTING_ENVIRONMENT_PROD = "prod";


        public const string ENV_VAR_AZ_CONFIG_CS = "AZ_CONFIG_CS";
        public const string ENV_VAR_AZ_KEYVAULT_SEC = "AZ_KEY_VAULT_SEC";
        public const string ENV_VAR_AZ_AI_INS_KEY = "AZ_AI_INS_KEY";

    }
    public static class LoggingConstants
    {
        /// <summary>
        /// Target's azure app insight
        ///
        /// </summary>
        public const string TARGET_APPLICATION_INSIGHT = "appInsightTarget";

        /// <summary>
        /// Don't use local file log until for development
        /// </summary>
        public const string TARGET_LOCAL_FILE_LOG = "fileLog";

        public const string LOG_LEVEL_TRACE = "Trace";
        public const string LOG_LEVEL_DEBUG = "Debug";
        public const string LOG_LEVEL_INFO = "Info";
        public const string LOG_LEVEL_WARN = "Warn";
        public const string LOG_LEVEL_ERROR = "Error";
        public const string LOG_LEVEL_FATAL = "Fatal";

    }
}
