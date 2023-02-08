namespace Tata.IGetIT.Learner.Repository.Models
{
    public class UserProfile
    {
        public int UserID { get; set; }
        public char Type { get; set; }
    }

    public class UserProfileDetails
    {
        public int UserID { get; set; }
        public bool IsPublic { get; set; }
        public UserPersonalProfile UserPersonalProfile { get; set; }
        public UserBusinessProfile UserBusinessProfile { get; set; }
    }

    public class UserPersonalProfile
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Biography { get; set; }
        public int AvatarID { get; set; }
        public string AvatarPath { get; set; }
        public string SocialLinkedIn { get; set; }
        public string SocialGmail { get; set; }
        public string SocialFacebook { get; set; }
    }

    public class UserBusinessProfile
    {
        public int UserID { get; set; }
        public string Company { get; set; }
        public string BusinessSite { get; set; }
        public string BusinessGroup { get; set; }
        public string Interests { get; set; }
        public int BusinessManager { get; set; }
        public string Workemail { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public int WorkIndustry { get; set; }
        public int FavCADApplication { get; set; }
    }

    public class UserNotificationSettings
    {
        public int UserID { get; set; }
        public bool IsNewsletterNotification { get; set; }
        public bool IsMarketingNotification { get; set; }
        public bool IsWeeklyInsights { get; set; }
        public string PendingCourseNotification { get; set; }
        public bool IsWeeklyProfileCompletionNotification { get; set; }
    }

    public class State
    {
        public int RegionID { get; set; }
        public int StateID { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }

    public class Country
    {
        public int RegionID { get; set; }
        public string RegionName { get; set; }
        public string RegionCode { get; set; }
    }

    public class IndustryInfo
    {
        public int ID { get; set; }
        public string IndustryName { get; set; }
    }

    public class CadApplicationList
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class UserGroups
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
    }
}