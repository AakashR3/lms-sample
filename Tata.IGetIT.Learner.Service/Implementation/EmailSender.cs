using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Tata.IGetIT.Learner.Repository.Models.Configurations;

namespace Tata.IGetIT.Learner.Service.Implementation
{
    public class EmailSender : IEmailSender
    {

        ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly SmtpConfig _smtpConfig;

        private string _EmailAttachmentTemplate = string.Empty;

        private string EmailAttachmentTemplate
        {
            get
            {
                return _EmailAttachmentTemplate;
            }
        }
        public EmailSender(IOptions<SmtpConfig> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
            ReadEmailAttachmentTemplate();
        }

        public async Task SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message);

            await SendAsync(mailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
                        
            message.Content = EmailAttachmentTemplate.Replace("{CONTENT}", message.Content).Replace("{YEAR}", DateTime.Now.ToString("yyyy")); 

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(string.Empty, _smtpConfig.FromEmailId));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder { HtmlBody =  message.Content };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }
        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_smtpConfig.SmtpHost, _smtpConfig.SmtpPort, SecureSocketOptions.Auto);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_smtpConfig.SmtpUser, _smtpConfig.SmtpPass);

                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
        private void ReadEmailAttachmentTemplate()
        {
            string TemplatePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), ApplicationConstants.EMAIL_TEMPLATE_PATH_EMAIL_ATTACHMENT);

            if (File.Exists(TemplatePath))
            {
                _EmailAttachmentTemplate = File.ReadAllText(TemplatePath);
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
    }
}
