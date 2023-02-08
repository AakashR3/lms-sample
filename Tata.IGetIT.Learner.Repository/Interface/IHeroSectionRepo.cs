using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface IHeroSectionRepo
    {
        public Task<CurrentRole> CurrentRole(int UserID);
        public Task<Skillset> Skillset(int UserID);
        public Task<CurrentLevel> CurrentLevel(int UserID);
        public Task<CareerPath> CareerPath(int UserID);
        public Task<TargetRoleCareerPath> TargetRoleCareerPath(int UserID);
        public Task<TargetRoleCareerPathPercentage> TargetRoleCareerPathPercentage(int UserID);
        public Task<TargetRoles> TargetRole(int UserID);
        public Task<TargetRoleCurrentLevel> TargetRoleCurrentLevel(int UserID);
    }
}
