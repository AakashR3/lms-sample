using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface ISkillAdvisorRepo
    {
        public Task<IEnumerable<SkillAdvisor_UserTypeRoles>> GetUserTypeRoles();
        public Task<IEnumerable<SkillAdvisor_Categories>> GetCategories(int RoleID);
        public Task<IEnumerable<SkillAdvisor_Softwares>> GetSoftwares();
        public Task<IEnumerable<SkillAdvisor_PersonalPlan>> GetSubscriptions(int RoleID, int ToolID, string CountryCode);
        public Task<IEnumerable<SkillAdvisor_Courses>> GetSkillAdvisorCourses(int SID_Y);
        public Task<IEnumerable<SkillAdvisor_Assessments>> GetSkillAdvisorAssessments(int SID_Y);
    }
}
