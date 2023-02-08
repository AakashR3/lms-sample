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
    public class RolesService : IRolesService
    {
        private readonly IRolesRepo _rolesRepo;
        //Inject logger,config, db and other required services
        public RolesService(IRolesRepo rolesRepo)
        {
            if (rolesRepo == null)
            {
                new ArgumentNullException("RolesSkillCompetency Repo cannot be null");
            }
            _rolesRepo = rolesRepo;
        }

        public async Task<IEnumerable<RolesListData>> GetRolesList(AdminRole AdminRole)
        {
            try
            {
                return await _rolesRepo.GetRolesList(AdminRole);
            }
            catch
            {
                throw;
            }
        }

        public async Task<RolesListData> GetRoleDetails(AdminRole AdminRole)
        {
            try
            {
                return await _rolesRepo.GetRoleDetails(AdminRole);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AddEditRole(MultipleRoles role, List<string> errorsMessages)
        {
            try
            {

                var result = await _rolesRepo.AddEditRole(role);

                switch (result)
                {
                    case 1:
                        break;

                    case -1:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_ROLE_INSERT);
                        break;

                    case -2:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_ROLENAME_EXISTS);
                        break;

                    case -3:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_ROLE_DOESNOTEXIST);
                        break;
                    
                    case 0:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_UNABLE_INSERT_ROLE);
                        break;

                    default:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_ROLE_INSERT);
                        break;
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeleteRole(DeleteRole role, List<string> errorsMessages)
        {
            try
            {
                var result = await _rolesRepo.DeleteRole(role);

                switch (result)
                {
                    case 1:
                        break;

                    case -1:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_ROLE_DELETE);
                        break;

                    case -2:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_ROLE_DOESNOTEXIST);
                        break;

                    default:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_ROLE_DELETE);
                        break;
                }

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}