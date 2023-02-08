using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class SkillAdvisorRepo : ISkillAdvisorRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public SkillAdvisorRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public Task<IEnumerable<SkillAdvisor_Categories>> GetCategories(int RoleID)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@RoleID", RoleID }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetSkillAdvisorCategories,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<SkillAdvisor_Categories>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<SkillAdvisor_Assessments>> GetSkillAdvisorAssessments(int SID_Y)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@SID_Y", SID_Y },

                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetSkillAdvisorAssessments,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<SkillAdvisor_Assessments>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<SkillAdvisor_Courses>> GetSkillAdvisorCourses(int SID_Y)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@SID_Y", SID_Y },
                    
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetSkillAdvisorCourses,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<SkillAdvisor_Courses>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<SkillAdvisor_Softwares>> GetSoftwares()
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    //{ "@CategoryID", CategoryID },
                    //{ "@MasterCategoryID", MasterCategoryID },
                    //{ "@Mode", Mode },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetSoftwaresList,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<SkillAdvisor_Softwares>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<SkillAdvisor_PersonalPlan>> GetSubscriptions(int RoleID, int ToolID, string CountryCode)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@RoleID", RoleID },
                    { "@ToolID", ToolID },
                    { "@CountryCode", CountryCode },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetSubscriptionList,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<SkillAdvisor_PersonalPlan>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

            public Task<IEnumerable<SkillAdvisor_UserTypeRoles>> GetUserTypeRoles()
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    //{ "@UserID", UserID },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetUserTypeRoles,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<SkillAdvisor_UserTypeRoles>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
