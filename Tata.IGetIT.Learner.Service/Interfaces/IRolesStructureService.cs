using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface IRolesStructureService
    {
        public Task<RolesStructureListData> GetRoleStructureDetails(AdminRoleStructure AdminRoleStructure);
        public Task<IEnumerable<RolesStructureListData>> GetRoleStructureList(AdminRoleStructure AdminRoleStructure);
        public Task<int> AddEditStructure(RolesStructureParam structure, List<string> errorsMessages);
        public Task<ProcedureReturnParameters> AddStructure(RolesStructureParam structure, List<string> errorsMessages);
        public Task<int> DeleteStructure(DeleteStructure structure, List<string> errorsMessages);
        public Task<IEnumerable<LearningPathRoleMapping>> GetRoleStructureLearningPath(AdminRoleStructure AdminRoleStructure);
        public Task<int> AddEditUserRoleCompetencyMap(UserRoleCompetencyMapParam userRoleCompetencyMapParam, List<string> ErrorsMessages);
        public Task<UserRoleCompetency> GetUserRoleCompetencyMap(AdminRoleStructure AdminRoleStructure);
    }
}