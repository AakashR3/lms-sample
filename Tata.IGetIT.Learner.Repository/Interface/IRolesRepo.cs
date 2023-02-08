using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface IRolesRepo
    {
        public Task<RolesListData> GetRoleDetails(AdminRole AdminRole);
        public Task<IEnumerable<RolesListData>> GetRolesList(AdminRole AdminRole);
        public Task<int> AddEditRole(MultipleRoles role);
        public Task<int> DeleteRole(DeleteRole role);
    }
}
