
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using ILogger = NLog.ILogger;
using Location = Tata.IGetIT.Learner.Repository.Models.Location;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Tata.IGetIT.Learner.Service.Implementation
{
    public class CommonService : ICommonService
    {
        private readonly SmtpConfig _smtpConfig;
        private readonly IEmailSender _emailSender;
        private readonly ICommonRepo _commonRepo;
        private readonly GeoLocationConfig _geoLocationConfig;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public CommonService(ICommonRepo commonRepo,  IOptions<GeoLocationConfig> geoLocationConfig, IOptions<SmtpConfig> smtpConfig, IEmailSender emailSender)
        {
            if (commonRepo == null)
            {
                new ArgumentNullException("commonRepo cannot be null");
            }
            _smtpConfig = smtpConfig.Value;
            _commonRepo = commonRepo;
            _geoLocationConfig = geoLocationConfig.Value;
            _emailSender = emailSender;
            //_smtpConfig = smtpConfig;
        }

        public async Task<IEnumerable<Plans>> GetAllPlans(int PlanCode)
        {
            return await _commonRepo.GetAllPlans(PlanCode);
        }

        public async Task<CategoryDetails> GetCategories(int CategoryID)
        {
            return await _commonRepo.GetCategories(CategoryID);
        }

        public async Task<IEnumerable<IndividualPlans>> GetIndividualPlans(string ClientIP)
        {
            var result = await GetCountryCode(ClientIP);
            return await _commonRepo.GetIndividualPlans(result.CountryCode);
        }

        public async Task<IEnumerable<IndividualPlans>> GetwwwIndividualPlans(string CountryCode)
        {
            return await _commonRepo.GetIndividualPlans(CountryCode);
        }

        public async Task<IEnumerable<GetSubCategoriesBasedOnCategoryID>> GetSubCategoriesBasedOnCategoryID(int CategoryID)
        {
            return await _commonRepo.GetSubCategoriesBasedOnCategoryID(CategoryID);
        }

        public async Task<IEnumerable<GetTopicsBasedOnCategoryID>> GetTopicsBasedOnCategoryID(int CategoryID)
        {
            return await _commonRepo.GetTopicsBasedOnCategoryID(CategoryID);
        }

        public async Task<Location> GetCountryCode(string ClientIP)
        {
            try
            {
                Location location = new();

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
        public async Task<bool> SendEmailAsync(Common_EmailInfo emailInfo)
        {
            try
            {
                /*
                var mimeMsg = new MimeMessage();
                mimeMsg.From.Add(MailboxAddress.Parse(_smtpConfig.FromEmailId));
                mimeMsg.To.Add(MailboxAddress.Parse(emailInfo.To));
                mimeMsg.Subject = emailInfo.Subject;
                mimeMsg.Body = new TextPart(TextFormat.Html) { Text = emailInfo.Content };
                
                var smtp = new SmtpClient();
                smtp.Connect(_smtpConfig.SmtpHost, _smtpConfig.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(_smtpConfig.SmtpUser, _smtpConfig.SmtpPass);
                await smtp.SendAsync(mimeMsg);
                smtp.Disconnect(true);
                */
            }
            catch
            {
                throw;
            }
            return true;
        }

        public async Task<IEnumerable<CourseTitles>> GetCourseTitles(int CategoryID, int SubCategoryID)
        {
            return await _commonRepo.GetCourseTitles(CategoryID, SubCategoryID);
        }
    }
}
