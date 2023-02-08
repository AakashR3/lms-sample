using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class UserProfileRepo : IUserProfileRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public UserProfileRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public Task<UserPersonalProfile> GetPersonalProfile(UserProfile userProfile)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", userProfile.UserID },
                    { "@Type", userProfile.Type }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.USERPROFILE_GETPERSONALPROFILE,
                    Parameters = param
                };

                var result = _databaseOperations.GetFirstRecordAsync<UserPersonalProfile>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<UserBusinessProfile> GetBusinessProfile(UserProfile userProfile)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", userProfile.UserID },
                    { "@Type", userProfile.Type }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.USERPROFILE_GETBUSINESSPROFILE,
                    Parameters = param
                };

                var result = _databaseOperations.GetFirstRecordAsync<UserBusinessProfile>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<string> UpdatePersonalProfile(UserPersonalProfile userPersonalProfile)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    {"@UserID", userPersonalProfile.UserID },
                    {"@FirstName", userPersonalProfile.FirstName },
                    {"@LastName", userPersonalProfile.LastName },
                    {"@UserName", userPersonalProfile.UserName },
                    {"@Email", userPersonalProfile.Email },
                    {"@Biography", userPersonalProfile.Biography },
                    {"@AvatarID", userPersonalProfile.AvatarID },
                    {"@ModifiedBy", userPersonalProfile.UserID },
                    {"@SocialLinkedIn", userPersonalProfile.SocialLinkedIn },
                    {"@SocialGmail", userPersonalProfile.SocialGmail },
                    {"@SocialFacebook", userPersonalProfile.SocialFacebook }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.USERPROFILE_UPDATEPERSONALPROFILE,
                    Parameters = param
                };

                var result = _databaseOperations.ExecuteScalarAsyncString(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<string> UpdateBusinessProfile(UserBusinessProfile userBusinessProfile)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    {"@UserID", userBusinessProfile.UserID },
                    {"@Company", userBusinessProfile.Company },
                    {"@BusinessSite", userBusinessProfile.BusinessSite },
                    {"@BusinessGroup", userBusinessProfile.BusinessGroup },
                    {"@Interests", userBusinessProfile.Interests },
                    {"@BusinessManager", userBusinessProfile.BusinessManager },
                    {"@Workemail", userBusinessProfile.Workemail },
                    {"@Phone", userBusinessProfile.Phone },
                    {"@Address1", userBusinessProfile.Address1 },
                    {"@Address2", userBusinessProfile.Address2 },
                    {"@Country", userBusinessProfile.Country },
                    {"@State", userBusinessProfile.State },
                    {"@City", userBusinessProfile.City },
                    {"@PostalCode", userBusinessProfile.PostalCode },
                    {"@WorkIndustry", userBusinessProfile.WorkIndustry },
                    {"@FavCADApplication", userBusinessProfile.FavCADApplication}
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.USERPROFILE_UPDATEBUSINESSPROFILE,
                    Parameters = param
                };

                var result = _databaseOperations.ExecuteScalarAsyncString(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<UserNotificationSettings> GetUserNotificationSettings(UserProfile userProfile)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", userProfile.UserID },
                    { "@Type", userProfile.Type }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.USERPROFILE_GETNOTIFICATIONSETTINGS,
                    Parameters = param
                };

                var result = _databaseOperations.GetFirstRecordAsync<UserNotificationSettings>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<string> UpdateUserNotificationSettings(UserNotificationSettings userNotificationSettings)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    {"@UserID", userNotificationSettings.UserID },
                    {"@Newsletter", userNotificationSettings.IsNewsletterNotification ? 1 : 0 },
                    {"@Marketing", userNotificationSettings.IsMarketingNotification ? 1 : 0 },
                    {"@WeeklyInsights", userNotificationSettings.IsWeeklyInsights ? 1 : 0 },
                    {"@PendingCourse", userNotificationSettings.PendingCourseNotification },
                    {"@WeeklyProfile", userNotificationSettings.IsWeeklyProfileCompletionNotification ? 1 : 0 }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.USERPROFILE_UPDATENOTIFICATIONSETTINGS,
                    Parameters = param
                };

                var result = _databaseOperations.ExecuteScalarAsyncString(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<UserDetails>> GetBusinessManager(UserProfile userProfile)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", userProfile.UserID }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.USERPROFILE_GETBUSINESSMANAGER,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<UserDetails>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<Country>> GetCountry()
        {
            try
            {
                var param = new Dictionary<string, object>
                {

                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.USERPROFILE_GETCOUNTRY,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<Country>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<State>> GetStateByRegion(State state)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@RegionID", state.RegionID }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.USERPROFILE_GETSTATEBYCOUNTRY,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<State>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<IndustryInfo>> GetWorkIndustry()
        {
            try
            {
                var param = new Dictionary<string, object>
                {

                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.USERPROFILE_GETINDUSTRYINFO,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<IndustryInfo>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<CadApplicationList>> GetCadApplicationList()
        {
            try
            {
                var param = new Dictionary<string, object>
                {

                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.USERPROFILE_GETCADAPPLICATIONLIST,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<CadApplicationList>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<UserGroups>> GetUserGroups(UserProfile userProfile)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", userProfile.UserID }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.USERPROFILE_GETUSERGROUPS,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<UserGroups>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}