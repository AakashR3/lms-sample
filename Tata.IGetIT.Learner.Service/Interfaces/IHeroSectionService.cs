using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface IHeroSectionService
    {
        public Task<CurrentRole> CurrentRole(int UserID, List<string> ErrorsMessages);
        public Task<Skillset> Skillset(int UserID, List<string> ErrorsMessages);
        public Task<CurrentLevel> CurrentLevel(int UserID, List<string> ErrorsMessages);
        public Task<CareerPath> CareerPath(int UserID, List<string> ErrorsMessages);
        public Task<TargetRoleCareerPath> TargetRoleCareerPath(int UserID, List<string> ErrorsMessages);
        public Task<TargetRoleCareerPathPercentage> TargetRoleCareerPathPercentage(int UserID, List<string> ErrorsMessages);
        public Task<TargetRoles> TargetRole(int UserID, List<string> ErrorsMessages);
        public Task<TargetRoleCurrentLevel> TargetRoleCurrentLevel(int UserID, List<string> ErrorsMessages);
    }
}
