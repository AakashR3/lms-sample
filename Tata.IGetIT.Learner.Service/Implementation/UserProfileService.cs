using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web;
using Tata.IGetIT.Learner.Repository.Helpers;
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Service.Implementation
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepo _userProfileRepo;
        //Inject logger,config, db and other required services
        public UserProfileService(IUserProfileRepo userProfileRepo)
        {
            if (userProfileRepo == null)
            {
                new ArgumentNullException("UserProfileRepo cannot be null");
            }
            _userProfileRepo = userProfileRepo;
        }

        public async Task<UserPersonalProfile> GetPersonalProfile(UserProfile userProfile)
        {
            try
            {
                return await _userProfileRepo.GetPersonalProfile(userProfile);
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserBusinessProfile> GetBusinessProfile(UserProfile userProfile)
        {
            try
            {
                return await _userProfileRepo.GetBusinessProfile(userProfile);
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> UpdatePersonalProfile(UserPersonalProfile userPersonalProfile)
        {
            try
            {
                return await _userProfileRepo.UpdatePersonalProfile(userPersonalProfile);
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> UpdateBusinessProfile(UserBusinessProfile userBusinessProfile)
        {
            try
            {
                return await _userProfileRepo.UpdateBusinessProfile(userBusinessProfile);
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserNotificationSettings> GetUserNotificationSettings(UserProfile userProfile)
        {
            try
            {

                return await _userProfileRepo.GetUserNotificationSettings(userProfile);
                // var result = await _userProfileRepo.GetUserNotificationSettings(userProfile);

                // var nestedResult = (from resultOutput in result
                //                     select new UserNotificationSettings()
                //                     {
                //                         UserID = resultOutput.Key.UserID,
                //                         IsNewsletterNotification = resultOutput.Key.Newsletter > 0 ? true : false,
                //                         IsMarketingNotification = resultOutput.Key.Marketing > 0 ? true : false,
                //                         IsWeeklyInsights = resultOutput.Key.WeeklyInsights > 0 ? true : false,
                //                         IsWeeklyProfileCompletionNotification = resultOutput.Key.WeeklyProfile > 0 ? true : false,
                //                         PendingCourseNotification = resultOutput.Key.PendingCourse,
                //                     });

                // return nestedResult;
                
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> UpdateUserNotificationSettings(UserNotificationSettings userNotificationSettings)
        {
            try
            {
                return await _userProfileRepo.UpdateUserNotificationSettings(userNotificationSettings);
            }
            catch
            {
                throw;
            }
        }        

        public async Task<IEnumerable<UserDetails>> GetBusinessManager(UserProfile userProfile)
        {
            try
            {
                return await _userProfileRepo.GetBusinessManager(userProfile);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Country>> GetCountry()
        {
            try
            {
                return await _userProfileRepo.GetCountry();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<State>> GetStateByRegion(State state)
        {
            try
            {
                return await _userProfileRepo.GetStateByRegion(state);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<IndustryInfo>> GetWorkIndustry()
        {
            try
            {
                return await _userProfileRepo.GetWorkIndustry();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<CadApplicationList>> GetCadApplicationList()
        {
            try
            {
                return await _userProfileRepo.GetCadApplicationList();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserGroups>> GetUserGroups(UserProfile userProfile)
        {
            try
            {
                return await _userProfileRepo.GetUserGroups(userProfile);
            }
            catch
            {
                throw;
            }
        }
    }
}