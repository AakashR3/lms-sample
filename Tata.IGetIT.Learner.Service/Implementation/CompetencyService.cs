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
    public class CompetencyService : ICompetencyService
    {
        private readonly ICompetencyRepo _competencyRepo;
        //Inject logger,config, db and other required services
        public CompetencyService(ICompetencyRepo competencyRepo)
        {
            if (competencyRepo == null)
            {
                new ArgumentNullException("Competency Repo cannot be null");
            }
            _competencyRepo = competencyRepo;
        }

        public async Task<IEnumerable<CompetencyListData>> GetCompetencyList(AdminCompetency AdminCompetency)
        {
            try
            {
                return await _competencyRepo.GetCompetencyList(AdminCompetency);
            }
            catch
            {
                throw;
            }
        }

        public async Task<CompetencyListData> GetCompetencyDetails(AdminCompetency AdminCompetency)
        {
            try
            {
                return await _competencyRepo.GetCompetencyDetails(AdminCompetency);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AddEditCompetency(Competency competency, List<string> errorsMessages)
        {
            try
            {

                var result = await _competencyRepo.AddEditCompetency(competency);

                switch (result)
                {
                    case 1:
                        break;

                    case -1:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_COMPETENCY_INSERT);
                        break;

                    case -2:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_COMPETENCYNAME_EXISTS);
                        break;

                    case -3:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_COMPETENCY_DOESNOTEXIST);
                        break;
                    
                    case 0:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_UNABLE_INSERT_COMPETENCY);
                        break;

                    default:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_COMPETENCY_INSERT);
                        break;
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeleteCompetency(DeleteCompetency competency, List<string> errorsMessages)
        {
            try
            {
                var result = await _competencyRepo.DeleteCompetency(competency);

                switch (result)
                {
                    case 1:
                        break;

                    case -1:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_COMPETENCY_DELETE);
                        break;

                    case -2:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_COMPETENCY_DOESNOTEXIST);
                        break;

                    default:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_COMPETENCY_DELETE);
                        break;
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<CompetencyLevel>> GetCompetencyLevel(AdminCompetency AdminCompetency)
        {
            try
            {
                return await _competencyRepo.GetCompetencyLevel(AdminCompetency);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<CompetencyType>> GetCompetencyType(AdminCompetency AdminCompetency)
        {
            try
            {
                return await _competencyRepo.GetCompetencyType(AdminCompetency);
            }
            catch
            {
                throw;
            }
        }
    }
}