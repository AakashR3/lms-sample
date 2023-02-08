using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface ISkillAdvisorService
    {
        public Task<IEnumerable<SkillAdvisor_UserTypeRoles>> GetUserTypeRoles(List<string> ErrorMessages);
        public Task<IEnumerable<SkillAdvisor_Softwares>> GetSoftwareList(List<string> ErrorMessages);
        public Task<IEnumerable<SkillAdvisor_Categories>> GetCategories(int RoleID, List<string> ErrorMessages);
        public Task<IEnumerable<SkillAdvisor_PersonalPlan>> GetSubscriptions(int RoleID, int CategoryID, string CountryCode, List<string> ErrorMessages);
        public Task<IEnumerable<SkillAdvisor_Courses>> GetSkillAdvisorCourses(int SID_Y, List<string> ErrorMessages);
        public Task<IEnumerable<SkillAdvisor_Assessments>> GetSkillAdvisorAssessments(int SID_Y, List<string> ErrorMessages);


    }
}
