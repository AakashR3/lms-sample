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
    public class RolesStructureService : IRolesStructureService
    {
        private readonly IRolesStructureRepo _rolesStructureRepo;
        //Inject logger,config, db and other required services
        public RolesStructureService(IRolesStructureRepo rolesStructureRepo)
        {
            if (rolesStructureRepo == null)
            {
                new ArgumentNullException("RolesStructure Repo cannot be null");
            }
            _rolesStructureRepo = rolesStructureRepo;
        }

        public async Task<IEnumerable<RolesStructureListData>> GetRoleStructureList(AdminRoleStructure AdminRoleStructure)
        {
            try
            {
                return await _rolesStructureRepo.GetRoleStructureList(AdminRoleStructure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<RolesStructureListData> GetRoleStructureDetails(AdminRoleStructure AdminRoleStructure)
        {
            try
            {
                return await _rolesStructureRepo.GetRoleStructureDetails(AdminRoleStructure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AddEditStructure(RolesStructureParam structure, List<string> errorsMessages)
        {
            try
            {

                var result = await _rolesStructureRepo.AddEditStructure(structure);

                if (result <= 0)
                {
                    switch (result)
                    {
                        case 1:
                            break;

                        case -1:
                            errorsMessages.Add(LearnerAppConstants.ISSUE_STRUCTURE_INSERT);
                            break;

                        case -2:
                            errorsMessages.Add(LearnerAppConstants.ISSUE_STRUCTURENAME_EXISTS);
                            break;

                        case -3:
                            errorsMessages.Add(LearnerAppConstants.ISSUE_STRUCTURE_DOESNOTEXIST);
                            break;

                        case 0:
                            errorsMessages.Add(LearnerAppConstants.ISSUE_UNABLE_INSERT_STRUCTURE);
                            break;

                        default:
                            errorsMessages.Add(LearnerAppConstants.ISSUE_STRUCTURE_INSERT);
                            break;
                    }
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProcedureReturnParameters> AddStructure(RolesStructureParam structure, List<string> errorsMessages)
        {
            try
            {

                var result = await _rolesStructureRepo.AddStructure(structure);

                if (result.ReturnValue <= 0)
                {
                    switch (result.ReturnValue)
                    {
                        case 1:
                            break;

                        case -1:
                            errorsMessages.Add(LearnerAppConstants.ISSUE_STRUCTURE_INSERT);
                            break;

                        case -2:
                            errorsMessages.Add(LearnerAppConstants.ISSUE_STRUCTURENAME_EXISTS);
                            break;

                        case -3:
                            errorsMessages.Add(LearnerAppConstants.ISSUE_STRUCTURE_DOESNOTEXIST);
                            break;

                        case 0:
                            errorsMessages.Add(LearnerAppConstants.ISSUE_UNABLE_INSERT_STRUCTURE);
                            break;

                        default:
                            errorsMessages.Add(LearnerAppConstants.ISSUE_STRUCTURE_INSERT);
                            break;
                    }
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeleteStructure(DeleteStructure structure, List<string> errorsMessages)
        {
            try
            {
                var result = await _rolesStructureRepo.DeleteStructure(structure);

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

        public async Task<IEnumerable<LearningPathRoleMapping>> GetRoleStructureLearningPath(AdminRoleStructure AdminRoleStructure)
        {
            try
            {
                return await _rolesStructureRepo.GetRoleStructureLearningPath(AdminRoleStructure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserRoleCompetency> GetUserRoleCompetencyMap(AdminRoleStructure AdminRoleStructure)
        {
            try
            {
                return await _rolesStructureRepo.GetUserRoleCompetencyMap(AdminRoleStructure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AddEditUserRoleCompetencyMap(UserRoleCompetencyMapParam userRoleCompetencyMapParam, List<string> errorsMessages)
        {
            try
            {

                var result = await _rolesStructureRepo.AddEditUserRoleCompetencyMap(userRoleCompetencyMapParam);

                if (result <= 0)
                {
                    switch (result)
                    {
                        case 1:
                            break;

                        case -1:
                            errorsMessages.Add(LearnerAppConstants.ISSUE_USERROLECOMPETENCY_INSERT);
                            break;

                        default:
                            errorsMessages.Add(LearnerAppConstants.ISSUE_USERROLECOMPETENCY_INSERT);
                            break;
                    }
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