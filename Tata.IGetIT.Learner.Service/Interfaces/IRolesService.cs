using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface IRolesService
    {
        public Task<RolesListData> GetRoleDetails(AdminRole AdminRole);
        public Task<IEnumerable<RolesListData>> GetRolesList(AdminRole AdminRole);
        public Task<int> AddEditRole(MultipleRoles role, List<string> errorsMessages);
        public Task<int> DeleteRole(DeleteRole role, List<string> errorsMessages);
    }
}