using MailKit;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Tata.IGetIT.Learner.Service
{
    /// <summary>
    /// Email service to send the email async
    /// </summary>
    public class EmailService : ICommunicationService
    {
        private readonly SmtpConfig _smtpConfig;
        private readonly ForgotPasswordConfig _forgotPasswordConfig;

        private string _ForgotEmailTemplate = string.Empty;
        private string _VerifyEmailTemplate = string.Empty;

        private string ForgotPasswordTemplate
        {
            get
            {
                return _ForgotEmailTemplate;
            }
        }
        private string VerifyEmailTemplate
        {
            get
            {
                return _VerifyEmailTemplate;
            }
        }
        ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initialize email service
        /// </summary>
        /// <param name="smtpConfig">SMTP configuration</param>
        public EmailService(IOptions<SmtpConfig> smtpConfig, IOptions<ForgotPasswordConfig> forgotPasswordConfig)
        {
            _smtpConfig = smtpConfig.Value;
            _forgotPasswordConfig = forgotPasswordConfig.Value;
            ReadForgotPasswordEmailTemplate();
            ReadVerifyEmailTemplate();
        }

        /// <summary>
        /// Send email using the verify email html template
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<bool> SendForgotPasswordEmailAsync(ResetPassword content)
        {
            try
            {
                string ContentToSend = ForgotPasswordTemplate
                                            .Replace("{UserEmail}", content.Email)
                                            .Replace("{URL}", String.Concat(_forgotPasswordConfig.BaseURL, content.EmailSessionId))
                                            .Replace("{NAME}", content.FirstName)
                                            .Replace("{YEAR}", DateTime.Now.ToString("yyyy"));

                EmailInfo emailInfo = new()
                {
                    To = content.Email,
                    Subject = "iGET IT Password Reset",
                    Content = ContentToSend
                };

                await SendEmailAsync(emailInfo);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    AppException = ex,
                    Message = "Exception occured while send email",
                    AdditionalInfo = new List<KeyValuePair<string, object>>()
                    {
                        new KeyValuePair<string,object>("content",content)
                    }
                });

                throw;
            }


        }

        /// <summary>
        /// Send email using the verify email html template
        /// </summary>
        /// <param name="registrationOTP"></param>
        /// <returns></returns>
        public async Task<bool> SendOTPEmailAsync(RegistrationOTP registrationOTP)
        {
            try
            {
                string ContentToSend = VerifyEmailTemplate
                    .Replace("{OTP}", registrationOTP.Otp.ToString())
                    .Replace("{YEAR}", DateTime.Now.ToString("yyyy"));
                EmailInfo emailInfo = new()
                {
                    To = registrationOTP.Email,
                    Subject = "Email Verification",
                    Content = ContentToSend
                };

                await SendEmailAsync(emailInfo);
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// Trigger email asynchronously using SMTP protocol
        /// </summary>
        /// <param name="emailInfo"></param>
        /// <returns></returns>
        private async Task SendEmailAsync(EmailInfo emailInfo)
        {
            try
            {
                var mimeMsg = new MimeMessage();

                mimeMsg.From.Add(MailboxAddress.Parse(emailInfo.From ?? _smtpConfig.FromEmailId));
                mimeMsg.To.Add(MailboxAddress.Parse(emailInfo.To));
                mimeMsg.Subject = emailInfo.Subject;
                mimeMsg.Body = new TextPart(TextFormat.Html) { Text = emailInfo.Content };

                // send email
                var smtp = new SmtpClient();
                smtp.Connect(_smtpConfig.SmtpHost, _smtpConfig.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(_smtpConfig.SmtpUser, _smtpConfig.SmtpPass);
                await smtp.SendAsync(mimeMsg);
                smtp.Disconnect(true);
            }
            catch (ServiceNotConnectedException SncEx)
            {
                logger.LogError("Email Service is not connected", SncEx);
            }
            catch (AuthenticationException AuthEx)
            {
                logger.LogError("SMTP Service is not Authenticated", AuthEx);
            }
            catch (SslHandshakeException SslEx)
            {
                logger.LogError("SMTP Service SSL Handshake exception", SslEx);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Read the template once and use the same template
        /// </summary>
        private void ReadForgotPasswordEmailTemplate()
        {
            string TemplatePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), ApplicationConstants.EMAIL_TEMPLATE_PATH_FORGOT_PASSWORD);

            if (File.Exists(TemplatePath))
            {
                _ForgotEmailTemplate = File.ReadAllText(TemplatePath);
            }
            else
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = LearnerAppConstants.FORGOT_PASSWORD_TEMPLATE_FILE_MISSING,
                    AdditionalInfo = new List<KeyValuePair<string, object>>()
                    {
                        new KeyValuePair<string,object>("Path",TemplatePath)
                    }
                });
            }

        }
        /// <summary>
        /// Read the template once and use the same template
        /// </summary>
        private void ReadVerifyEmailTemplate()
        {
            string TemplatePath = Path.Combine(Directory.GetCurrentDirectory(), ApplicationConstants.EMAIL_TEMPLATE_PATH_VERIFY_EMAIL);

            if (File.Exists(TemplatePath))
            {
                _VerifyEmailTemplate = File.ReadAllText(TemplatePath);
            }
            else
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = LearnerAppConstants.VERIFY_EMAIL_TEMPLATE_FILE_MISSING,
                    AdditionalInfo = new List<KeyValuePair<string, object>>()
                    {
                        new KeyValuePair<string,object>("Path",TemplatePath)
                    }
                });
            }

        }

    }
}
