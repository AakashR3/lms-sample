using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using NLog.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tata.IGetIT.Learner.Repository.Implementation;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using ILogger = NLog.ILogger;

namespace Tata.IGetIT.Learner.Service.Implementation
{
    public class SkillAdvisorService : ISkillAdvisorService
    {
        private readonly ISkillAdvisorRepo _skillAdvisorRepo;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public SkillAdvisorService(ISkillAdvisorRepo skillAdvisorRepo)
        {
            if (skillAdvisorRepo == null)
            {
                new ArgumentNullException("skillAdvisorRepo cannot be null");
            }
            _skillAdvisorRepo = skillAdvisorRepo;
        }

        public async Task<IEnumerable<SkillAdvisor_Categories>> GetCategories(int RoleID, List<string> ErrorMessages)
        {
            try
            {
                var result = await _skillAdvisorRepo.GetCategories(RoleID);
                if (result != null)
                    ErrorMessages.Add(LearnerAppConstants.NoRecordsFound);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<SkillAdvisor_Assessments>> GetSkillAdvisorAssessments(int SID_Y, List<string> ErrorMessages)
        {

            try
            {
                var result = await _skillAdvisorRepo.GetSkillAdvisorAssessments(SID_Y);
                if (result != null)
                    ErrorMessages.Add(LearnerAppConstants.NoRecordsFound);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<SkillAdvisor_Courses>> GetSkillAdvisorCourses(int SID_Y, List<string> ErrorMessages)
        {
            try
            {
                var result = await _skillAdvisorRepo.GetSkillAdvisorCourses(SID_Y);
                if (result != null)
                    ErrorMessages.Add(LearnerAppConstants.NoRecordsFound);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<SkillAdvisor_Softwares>> GetSoftwareList(List<string> ErrorMessages)
        {
            try
            {
                var result = await _skillAdvisorRepo.GetSoftwares();
                if (result != null)
                    ErrorMessages.Add(LearnerAppConstants.NoRecordsFound);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<SkillAdvisor_PersonalPlan>> GetSubscriptions(int RoleID, int ToolID, string CountryCode, List<string> ErrorMessages)
        {
            try
            {
                var result = await _skillAdvisorRepo.GetSubscriptions(RoleID, ToolID, CountryCode);
                if (result != null)
                    ErrorMessages.Add(LearnerAppConstants.Invalid_Inputs);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<SkillAdvisor_UserTypeRoles>> GetUserTypeRoles(List<string> ErrorMessages)
        {
            try
            {
                var result = await _skillAdvisorRepo.GetUserTypeRoles();
                if (result != null)
                    ErrorMessages.Add(LearnerAppConstants.NoRecordsFound);
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
