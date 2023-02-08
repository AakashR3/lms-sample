using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface IRolesStructureRepo
    {
        public Task<RolesStructureListData> GetRoleStructureDetails(AdminRoleStructure AdminRoleStructure);
        public Task<IEnumerable<RolesStructureListData>> GetRoleStructureList(AdminRoleStructure AdminRoleStructure);
        //public Task<int> AddEditStructure(List<RolesStructureParam> structure);
        public Task<int> AddEditStructure(RolesStructureParam structure);
        public Task<int> DeleteStructure(DeleteStructure structure);
        public Task<IEnumerable<LearningPathRoleMapping>> GetRoleStructureLearningPath(AdminRoleStructure AdminRoleStructure);
        public Task<ProcedureReturnParameters> AddStructure(RolesStructureParam structure);
        public Task<int> AddEditUserRoleCompetencyMap(UserRoleCompetencyMapParam userRoleCompetencyMapParam);
        public Task<UserRoleCompetency> GetUserRoleCompetencyMap(AdminRoleStructure AdminRoleStructure);
    }
}
