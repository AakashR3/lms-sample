using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface IUserProfileRepo
    {
        public Task<UserPersonalProfile> GetPersonalProfile(UserProfile userProfile);
        public Task<UserBusinessProfile> GetBusinessProfile(UserProfile userProfile);
        public Task<string> UpdatePersonalProfile(UserPersonalProfile userPersonalProfile);
        public Task<string> UpdateBusinessProfile(UserBusinessProfile userBusinessProfile);
        public Task<UserNotificationSettings> GetUserNotificationSettings(UserProfile userProfile);
        public Task<string> UpdateUserNotificationSettings(UserNotificationSettings userNotificationSettings);
        public Task<IEnumerable<UserDetails>> GetBusinessManager(UserProfile userProfile);
        public Task<IEnumerable<Country>> GetCountry();
        public Task<IEnumerable<State>> GetStateByRegion(State state);
        public Task<IEnumerable<IndustryInfo>> GetWorkIndustry();
        public Task<IEnumerable<CadApplicationList>> GetCadApplicationList();
        public Task<IEnumerable<UserGroups>> GetUserGroups(UserProfile userProfile);
    }

}
